using SudokuSolverConsole.Strategies;
using SudokuSolverConsole.Workers;
using System;

namespace SudokuSolver
{
    class Game
    {

        private Mapper mapper { get; set; }
        private BoardStateManager boardStateManager { get; set; }
        private SolverEngine solverEngine { get; set; }
        public bool IsSolved { get; internal set; }

        public Game(Mapper _mapper, BoardStateManager _boardStateManager, SolverEngine _solverEngine)
        {
            mapper = _mapper;
            boardStateManager = _boardStateManager;
            solverEngine = _solverEngine;
        }

        public void Solve(int[,] sudokuBoard)
        {
            try
            {
                // Init mapper
                Mapper mapper = new Mapper();
                // Init boardStateManager
                BoardStateManager boardStateManager = new BoardStateManager();
                // Init solver Engine
                SolverEngine solverEngine = new SolverEngine(boardStateManager, mapper);
                // Init sudokuBoard
                int[,] sudokuBord = sudokuBoard;

                // Check if the sudoku is solved
                IsSolved = solverEngine.Solve(sudokuBord);

            }
            // Throw an error
            catch (Exception ex)
            {
                Console.WriteLine($"Sudoku kan niet worden opgelost omdat: {ex.Message}");
            }
        }
    }
}
