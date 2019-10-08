using SudokuSolverConsole.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolverConsole.Strategies
{
    class SolverEngine
    {
        private readonly BoardStateManager _boardStateManager;
        private readonly Mapper _mapper;

        public SolverEngine(BoardStateManager boardStateManager, Mapper mapper)
        {
            _boardStateManager = boardStateManager;
            _mapper = mapper;
        }

        public bool Solve(int [,] sudokuBoard)
        {
            // Lijst met strategieën
            List<IStrategy> strategies = new List<IStrategy>()
            {
                new SimpleMarkUpStrategy(_mapper),
                new NakedPairStrategy(_mapper),

            };

            // Huidige staat van het bord bepalen
            var curState = _boardStateManager.GenerateState(sudokuBoard);

            // Staat van het bord bepalen na het toepassen van de eerste strategie
            var nextState = _boardStateManager.GenerateState(strategies.First().Solve(sudokuBoard));

            // Zolang het bord niet is opgelost ENde huidige staat niet gelijk is aan de volgende staat
            // (want dan doen de strategieën niks en kan het spel niet worden opgelost)
            while (!_boardStateManager.IsSolved(sudokuBoard) && curState != nextState)
            {
                // Huidige staat is de oude volgende staat
                curState = nextState;
                foreach(var strategy in strategies)
                {
                    // Voor elke strategie de solve functie aanroepen en kijken of dat een verandering oplevert, anders kan het spel niet worden opgelost
                    nextState = _boardStateManager.GenerateState(strategy.Solve(sudokuBoard));
                }
            }
            // if (curState != nextState): false; // geen oplossing
            // if (_boardStateManager.IsSolved(sudokuBoard)): true; spel is opgelost
            return _boardStateManager.IsSolved(sudokuBoard);
        }
    }
}
