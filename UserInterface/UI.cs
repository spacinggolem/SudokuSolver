using SudokuSolver.UserInterface;
using SudokuSolverConsole.Strategies;
using SudokuSolverConsole.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace SudokuSolver
{
    public partial class UI : Form, IUI
    {
        public bool IsSolved { get; set; }

        private static Mapper mapper = new Mapper();
        private static BoardStateManager boardStateManager = new BoardStateManager();

        private static SolverEngine solverEngine = new SolverEngine(boardStateManager, mapper);


        private Game game = new Game(mapper, boardStateManager, solverEngine);


        public UI()
        { 
            InitializeComponent();
        }

        // Return the sudokuBoard filled with the value 
        public int[,] GetBoard()
        {
            // Keep track of the current row
            int c_row = 0;
            // Create empty sudokuBoard
            int[,] sudokuBoard = new int[9, 9];

            // Create a list which will store all the panels/ rows from the form
            List<Control> rows = GetRows();
            // For each row
            foreach (Control row in rows)
            {
                // Get all the collumns from the row
                List<Control> cols = GetColumns(row);
                // Keep track of the current column
                int c_col = 0;
                // For each columns
                foreach (Control c in cols)
                {
                    try
                    {
                        // Check if it's empty
                        if (!String.IsNullOrEmpty(c.Text))
                        {
                            // Parse the text to a number
                            sudokuBoard[c_row, c_col] = Int32.Parse(c.Text);
                        }
                        else
                        {
                            // If the field is empty its value should be set to 0
                            sudokuBoard[c_row, c_col] = 0;
                        }

                        // Move on to the next column
                        c_col++;
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
                // Move on to the next row
                c_row++;
            }

            // Return the sudokuBoard
            return sudokuBoard;
        }

        // Take in a sudokuBoard and display it to the form
        public void DisplayBoard(int[,] sudokuBoard)
        {
            // Create list that will store the rows
            List<Control> rows = GetRows();
            // For each row
            foreach (Control row in rows)
            {
                // Get all the columns
                List<Control> cols = GetColumns(row);
                // For every column
                foreach (Control c in cols)
                {
                    // Update the value in the form to the value from the sudokuBoard
                    c.Text = sudokuBoard[row.TabIndex, c.TabIndex].ToString();
                }

            }
        }
        private List<Control> GetColumns(Control row)
        {
            List<Control> cols = new List<Control>();
            // Get all the controls in the row
            List<Control> controls = row.Controls.Cast<Control>().ToList();
            // For each control
            foreach (Control control in controls)
            {
                // If it's a panel it is a row
                if (control.GetType() == typeof(TextBox))
                {
                    cols.Add(control);
                }
            }
            // Reverse the list to match the UI
            cols.Reverse();
            // Return the columns
            return cols;
        }

        private List<Control> GetRows()
        {
            // Create list that will store the rows
            List<Control> rows = new List<Control>();
            // Get all controls from the form and put them in a list
            List<Control> controls = this.Controls.Cast<Control>().ToList();
            // For each control
            foreach (Control control in controls)
            {
                // If it's a panel it is a row
                if (control.GetType() == typeof(Panel))
                {
                    rows.Add(control);
                }
            }
            // Reverse the list
            rows.Reverse();
            return rows;

        }

        private void Solve_Click(object sender, EventArgs e)
        {
            int[,] sudokuBoard = GetBoard();
            DisplayBoard(sudokuBoard);
            game.Solve(sudokuBoard);
            DisplayBoard(sudokuBoard);

            if (game.IsSolved)
            {
                resultaatBox.Text = "De sudoku is volledig opgelost!!";
            }
            else
            {
                resultaatBox.Text = "De sudoku is niet volledig opgelost!!";
            }

        }

        private void ValidateInput(object sender, EventArgs e)
        {
            // Get the textbox
            Control textbox = (Control)sender;
            // Get the value of the textbox
            string val = textbox.Text;

            // If the input is a number
            if (int.TryParse(val, out int number))
            {
                // Update the value of the field
                textbox.Text = number.ToString();
            }
            else
            {
                // Empty the field
                textbox.Text = "";
            }
        }

        private void ResetFields(object sender, EventArgs e)
        {
            List<Control> rows = GetRows();
            foreach(Control row in rows)
            {
                List<Control> cols = GetColumns(row);
                foreach(Control col in cols)
                {
                    col.Text = "";
                }
            }

            resultaatBox.Text = "";
        }
    }

}
