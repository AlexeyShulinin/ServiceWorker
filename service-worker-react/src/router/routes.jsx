import React from 'react';
import { UserList } from '../views/Users';
import { createBrowserRouter } from 'react-router-dom';
import { TransactionsList } from '../views/Transactions/index.js';

export const router = createBrowserRouter([
    {
        path: '/',
        element: <UserList />,
    },
    {
        path: ':userId/transactions',
        element: <TransactionsList />,
    },
]);
