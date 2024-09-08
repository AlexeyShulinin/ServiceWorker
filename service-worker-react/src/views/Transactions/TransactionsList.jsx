import React from 'react';
import { useTransactionsList } from './useTransactions.js';
import BaseTable from '../../Components/Table/index.js';
import { transactionsListTableColumns } from './transactionsListTableColumns.jsx';
import { CircularProgress, Grid2 } from '@mui/material';

export const TransactionsList = () => {
    const { transactions, isFetching } = useTransactionsList();

    if (isFetching || !transactions) {
        return (
            <Grid2 display="flex">
                <CircularProgress />
            </Grid2>
        );
    }

    return (
        <BaseTable
            columns={transactionsListTableColumns}
            items={transactions}
        />
    );
};
