import React from 'react';
import { createRoot } from 'react-dom/client';
import App from './App.jsx';

createRoot(document.getElementById('root')).render(<App />);

window.addEventListener('load', async () => {
    if ('serviceWorker' in navigator) {
        try {
            const registered =
                await navigator.serviceWorker.register('./serviceWorker.js');
            console.log(registered);
        } catch (error) {
            console.error('Service worker register failed: ', error);
        }
    }
});
