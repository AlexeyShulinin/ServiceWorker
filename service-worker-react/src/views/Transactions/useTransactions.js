import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

export const useTransactionsList = () => {
    const { userId } = useParams();
    const [transactions, setTransactions] = useState([]);
    const [isFetching, setIsFetching] = useState(false);

    useEffect(() => {
        fetchTransactionsList();
    }, []);

    const fetchTransactionsList = () => {
        setIsFetching(true);
        fetch(
            `${import.meta.env.VITE_API_BASE_URL}/users/${userId}/transactions`,
        )
            .then((response) => response.json())
            .then((result) => setTransactions(result))
            .finally(() => setIsFetching(false));
    };

    return {
        transactions,
        isFetching,
    };
};
