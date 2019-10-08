using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolverConsole.Strategies
{
    // Dit is een blauwdruk voor een strategie
    interface IStrategy
    {
        // Elke strategie heeft een solve functie nodig die een int[,] returned
        int[,] Solve(int[,] sudokuBoard);
    }
}
