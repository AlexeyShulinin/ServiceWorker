import React from 'react';
import {
    Paper,
    TableBody,
    TableContainer,
    TableHead,
    Table,
} from '@mui/material';
import { StyledTableRow } from './StyledTableRow.js';
import { StyledTableCell } from './StyledTableCell.js';

const BaseTable = ({ columns, items }) => {
    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <StyledTableRow>
                        {columns?.map((column, index) => (
                            <StyledTableCell
                                key={`${column.header}_${index}`}
                                align={column.align || 'left'}>
                                {column.header}
                            </StyledTableCell>
                        ))}
                    </StyledTableRow>
                </TableHead>
                <TableBody>
                    {items?.map((item, index) => (
                        <StyledTableRow
                            key={`${item.id}_${index}`}
                            sx={{
                                '&:last-child td, &:last-child th': {
                                    border: 0,
                                },
                            }}>
                            {columns.map((column, index) => {
                                return (
                                    <React.Fragment key={index}>
                                        {column.cellReducer(item)}
                                    </React.Fragment>
                                );
                            })}
                        </StyledTableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
};

export default BaseTable;
