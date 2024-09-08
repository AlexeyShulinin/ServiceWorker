import { StyledTableCell } from '../../Components/Table/StyledTableCell.js';
import React from 'react';

export const transactionsListTableColumns = [
    {
        header: 'Amount',
        cellReducer(transaction) {
            return (
                <StyledTableCell component="th" scope="row">
                    {`${transaction.amount} ${transaction.currency}`}
                </StyledTableCell>
            );
        },
    },
    {
        header: 'Date',
        align: 'right',
        cellReducer(transaction) {
            return (
                <StyledTableCell align="right">
                    {transaction.date}
                </StyledTableCell>
            );
        },
    },
];
