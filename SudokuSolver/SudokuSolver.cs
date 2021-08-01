using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolverWPF
{
    class SudokuSolver
    {
        private int[,] sudokuBoard = new int[9, 9];

        public int[,] SudokuBoard { get => sudokuBoard; set => sudokuBoard = value; }

        public SudokuSolver(int[,] sudokuBoard)
        {
            this.SudokuBoard = sudokuBoard;
        }

        public bool IsConsistent()
        {
            int rows = SudokuBoard.GetLength(0);
            int columns = SudokuBoard.GetLength(1);
            if (rows != 9 || columns != 9)
            {
                return false;
            }
            for (int i = 0; i < 9; i++)
            {
                if (!IsColumnConsistent(i) || !IsRowConsistent(i) || !IsSubgridConsistent(i))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsRowConsistent(int row)
        {
            List<int> rowValues = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                int currentValue = SudokuBoard[row, i];
                if (currentValue != 0)
                {
                    if (rowValues.Contains(currentValue))
                    {
                        return false;
                    }
                    rowValues.Add(currentValue);
                }
            }
            return true;
        }

        private bool IsColumnConsistent(int column)
        {
            List<int> rowValues = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                int currentValue = SudokuBoard[i, column];
                if (currentValue != 0)
                {
                    if (rowValues.Contains(currentValue))
                    {
                        return false;
                    }
                    rowValues.Add(currentValue);
                }
            }
            return true;
        }

        private bool IsSubgridConsistent(int subgrid)
        {
            int firstX = (subgrid % 3) * 3;
            int firstY = (subgrid / 3) * 3;
            List<int> subgridValues = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int x = firstX + i;
                    int y = firstY + j;
                    int currentValue = SudokuBoard[y, x];
                    if (currentValue != 0)
                    {
                        if (subgridValues.Contains(currentValue))
                        {
                            return false;
                        }
                        subgridValues.Add(currentValue);
                    }
                }
            }
            return true;
        }

        private List<int> AvailableValues(int x, int y)
        {
            List<int> availableValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int firstSubgridX = (x / 3) * 3;
            int firstSubgridY = (y / 3) * 3;
            //removing values from row and column
            for (int i = 0; i < 9; i++)
            {
                availableValues.Remove(SudokuBoard[y, i]);
                availableValues.Remove(SudokuBoard[i, x]);
            }

            //removing values from current subgrid
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int currentX = firstSubgridX + i;
                    int currentY = firstSubgridY + j;
                    int currentValue = sudokuBoard[currentY, currentX];
                    if (currentValue != 0)
                    {
                        availableValues.Remove(currentValue);
                    }
                }
            }

            return availableValues;
        }

        public bool Solve()
        {
            //int x, y;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int currentValue = SudokuBoard[i, j];
                    if (currentValue == 0)
                    {
                        var availableValues = AvailableValues(j, i);
                        foreach (int value in availableValues)
                        {
                            sudokuBoard[i, j] = value;
                            if (Solve())
                            {
                                return true;
                            }
                        }
                        // no solution in this path - rollback
                        sudokuBoard[i, j] = 0;
                        return false;
                    }
                }
            }
            // no empty values
            return true;
        }



    }
}
