import { useEffect, useState } from 'react';

export const useUserList = () => {
    const [users, setUsers] = useState([]);
    const [isFetching, setIsFetching] = useState(false);

    useEffect(() => {
        fetchUserList();
    }, []);

    const fetchUserList = () => {
        setIsFetching(true);
        fetch(`${import.meta.env.VITE_API_BASE_URL}/users`)
            .then((response) => response.json())
            .then((result) => setUsers(result))
            .finally(() => setIsFetching(false));
    };

    return {
        users,
        isFetching,
    };
};
