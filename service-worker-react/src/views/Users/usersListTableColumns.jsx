import { StyledTableCell } from '../../Components/Table/StyledTableCell.js';
import React from 'react';
import { Link } from 'react-router-dom';

export const usersListTableColumns = [
    {
        header: 'FullName',
        cellReducer(user) {
            return (
                <StyledTableCell component="th" scope="row">
                    <Link to={`${user.id}/transactions`}>
                        {`${user.lastName}, ${user.firstName}`}
                    </Link>
                </StyledTableCell>
            );
        },
    },
    {
        header: 'Balance',
        align: 'right',
        cellReducer(user) {
            return (
                <StyledTableCell align="right">{`${user.balance} ${user.currency}`}</StyledTableCell>
            );
        },
    },
];
