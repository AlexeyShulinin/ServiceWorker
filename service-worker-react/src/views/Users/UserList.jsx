import React from 'react';
import { useUserList } from './useUserList';
import BaseTable from '../../Components/Table/index.js';
import { usersListTableColumns } from './usersListTableColumns.jsx';
import { CircularProgress, Grid2 } from '@mui/material';

export const UserList = () => {
    const { users, isFetching } = useUserList();

    if (isFetching || !users) {
        return (
            <Grid2 display="flex">
                <CircularProgress />
            </Grid2>
        );
    }

    return <BaseTable columns={usersListTableColumns} items={users} />;
};
