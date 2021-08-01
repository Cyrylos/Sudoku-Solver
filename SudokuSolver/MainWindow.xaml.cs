using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuSolverWPF
{
    public partial class MainWindow : Window
    {
        private TextBox[,] sudokuFields = new TextBox[9, 9];
        private int[,] sudokuBoard = new int[9, 9];
        public MainWindow()
        {
            InitializeComponent();
            sudokuFields = new TextBox[,] { { Field00, Field01, Field02, Field03, Field04, Field05, Field06, Field07, Field08},
                { Field10, Field11, Field12, Field13, Field14, Field15, Field16, Field17, Field18},
                { Field20, Field21, Field22, Field23, Field24, Field25, Field26, Field27, Field28},
                { Field30, Field31, Field32, Field33, Field34, Field35, Field36, Field37, Field38},
                { Field40, Field41, Field42, Field43, Field44, Field45, Field46, Field47, Field48},
                { Field50, Field51, Field52, Field53, Field54, Field55, Field56, Field57, Field58},
                { Field60, Field61, Field62, Field63, Field64, Field65, Field66, Field67, Field68},
                { Field70, Field71, Field72, Field73, Field74, Field75, Field76, Field77, Field78},
                { Field80, Field81, Field82, Field83, Field84, Field85, Field86, Field87, Field88} };
        }

        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    String sudokuFieldValue = sudokuFields[i, j].Text;
                    if (sudokuFieldValue.Equals(""))
                    {
                        sudokuBoard[i, j] = 0;
                    }
                    else
                    {
                        sudokuBoard[i, j] = int.Parse(sudokuFieldValue);
                    }
                }
            }

            SudokuSolver sudokusolver = new SudokuSolver(sudokuBoard);
            if (!sudokusolver.IsConsistent())
            {
                MessageBox.Show("Sudoku board isn't consistent!");
                return;
            }
            if (!sudokusolver.Solve())
            {
                MessageBox.Show("This sudoku doesn't have a sulution!");
                return;
            }
            sudokuBoard = sudokusolver.SudokuBoard;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    String sudokuFieldValue = sudokuFields[i, j].Text;
                    if (sudokuFieldValue.Equals(""))
                    {
                        sudokuFields[i, j].Text = sudokuBoard[i, j].ToString();
                    }
                    else
                    {
                        sudokuFields[i, j].Background = new SolidColorBrush(Colors.LightGray);
                    }
                }
            }
        }

        private void ClearBoard_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudokuFields[i, j].Background = new SolidColorBrush(Colors.White);
                    sudokuFields[i, j].Clear();
                    sudokuBoard[i, j] = 0;
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void IsNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            String fieldValue = ((TextBox)sender).Text + e.Text;
            Regex reg = new Regex("^[1-9]$");
            e.Handled = !reg.IsMatch(fieldValue);
        }
    }
}
