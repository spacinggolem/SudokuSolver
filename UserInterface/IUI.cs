using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.UserInterface
{
    interface IUI
    {
        int[,] GetBoard();
        void DisplayBoard(int[,] sudokuBord);
    }
}
