const STATIC_FILES_CACHE_NAME = 'serviceWorker-v1';
const API_RESPONSES_CACHE_NAME = 'serviceWorker-api-v1';

const CACHE_NAMES = [STATIC_FILES_CACHE_NAME, API_RESPONSES_CACHE_NAME];

const requestSwitcherHandler = requestSwitcher();

self.addEventListener('install', async (event) => {
    console.log('Service worker install', event);
    const cache = await caches.open(STATIC_FILES_CACHE_NAME);
    cache.addAll(['index.html']);
});

self.addEventListener('activate', async () => {
    const cacheKeys = await caches.keys();
    await Promise.all(
        cacheKeys
            .filter((cacheName) => !CACHE_NAMES.includes(cacheName))
            .map((cacheName) => caches.delete(cacheName)),
    );
});

self.addEventListener('fetch', async (event) => {
    console.log('Service worker fetch', event);

    const { request } = event;
    const url = new URL(request.url);

    if (url.origin !== location.origin) {
        // For API prefer network response
        event.respondWith(
            (async () => {
                const cache = await caches.open(API_RESPONSES_CACHE_NAME);
                try {
                    const response = await requestSwitcherHandler(request);
                    await cache.put(request, response.clone());
                    return response;
                } catch {
                    return await cache.match(request);
                }
            })(),
        );
    } else {
        // For static files prefer cached data
        event.respondWith(
            (async () => {
                const cache = await caches.open(STATIC_FILES_CACHE_NAME);
                try {
                    const cachedResponse = await cache.match(request);
                    if (!cachedResponse) {
                        const response = await fetch(request);
                        await cache.put(request, response.clone());
                        return response;
                    }

                    return cachedResponse;
                } catch {
                    console.error('Failed to fetch static values: ', event);
                }
            })(),
        );
    }
});

function requestSwitcher() {
    const SERVER_DOMAINS = ['http://localhost:5200', 'https://localhost:7235'];
    let currentDomain = SERVER_DOMAINS[0];

    const MAX_ALLOWED_REQUESTS_DISTANCE = 5;

    const requestUrlMap = new Map();

    function* differentServerDomain(currentDomain) {
        for (const domain of SERVER_DOMAINS.filter(
            (domain) => domain !== currentDomain,
        )) {
            yield domain;
        }
    }

    return (request) => {
        const url = new URL(request.url);
        if (requestUrlMap.has(currentDomain)) {
            const countOfRequests = requestUrlMap.get(currentDomain);
            for (const domain of differentServerDomain(currentDomain)) {
                if (requestUrlMap.has(domain)) {
                    const differentDomainCountOfRequests =
                        requestUrlMap.get(domain);
                    if (
                        differentDomainCountOfRequests +
                            MAX_ALLOWED_REQUESTS_DISTANCE <
                        countOfRequests
                    ) {
                        currentDomain = domain;
                        break;
                    }
                } else {
                    if (countOfRequests > MAX_ALLOWED_REQUESTS_DISTANCE) {
                        currentDomain = domain;
                        requestUrlMap.set(domain, 1);
                        break;
                    }

                    requestUrlMap.set(domain, 0);
                }
            }
        } else {
            requestUrlMap.set(currentDomain, 0);
        }

        requestUrlMap.set(currentDomain, requestUrlMap.get(currentDomain) + 1);
        return fetch(
            new Request(currentDomain + url.pathname, request.clone()),
        );
    };
}
