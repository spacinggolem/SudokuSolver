using SudokuSolverConsole.Workers;
using System;
using System.Linq;

namespace SudokuSolverConsole.Strategies
{
    class SimpleMarkUpStrategy : IStrategy
    {
        private readonly Mapper _mapper;

        public SimpleMarkUpStrategy(Mapper mapper)
        {
            _mapper = mapper;
        }

        public int[,] Solve(int[,] sudokuBoard)
        {
            // Voor elke rij
            for (int rij = 0; rij < sudokuBoard.GetLength(0); rij++)
            {
                // Voor elke kolom in de rij
                for (int kolom = 0; kolom < sudokuBoard.GetLength(1); kolom++)
                {
                    // Als de lengte gelijk is aan 0
                    // EN als de waarde langer is dan 1 cijfer
                    if (sudokuBoard[rij, kolom] == 0 || sudokuBoard[rij, kolom].ToString().Length > 1)
                    {
                        // Alle mogelijkheden bepalen
                        var mogelijkhedenInRijAndKolom = GetMogelijkhedenInRijEnKolom(sudokuBoard, rij, kolom);
                        var mogelijkhedenInBlok = GetMogelijkhedenInVak(sudokuBoard, rij, kolom);
                        sudokuBoard[rij, kolom] = GetMogelijkheden(mogelijkhedenInRijAndKolom, mogelijkhedenInBlok);
                    }
                }

            }
            return sudokuBoard;
        }


        private int GetMogelijkhedenInRijEnKolom(int[,] sudokuBoard, int _rij, int _kolom)
        {
            // Array met alle mogelijke waardes
            int[] mogelijkheden = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Voor elke kolom
            for (int kolom = 0; kolom < 9; kolom++)
            {
                // Als de celwaarde van de rij waar de cel zich bevindt een geldige waarde is
                if (IsValidSingle(sudokuBoard[_rij, kolom]))
                {
                    // De waarde staat op index "waarde - 1", de waarde gelijk stellen aan nul
                    mogelijkheden[sudokuBoard[_rij, kolom] - 1] = 0;
                }
            }
            // Voor elke rij
            for (int rij = 0; rij < 9; rij++)
            {
                // Als de celwaarde van de kolom waar de cel zich bevindt een geldige waarde is
                if (IsValidSingle(sudokuBoard[rij, _kolom]))
                {
                    // De waarde staat op index "waarde - 1", de waarde gelijk stellen aan nul
                    mogelijkheden[sudokuBoard[rij, _kolom] - 1] = 0;
                }
            }

            // Selecteer alle waardes in 'mogelijkheden' die niet gelijk zijn aan nul en zet deze in een string
            return Convert.ToInt32(String.Join(string.Empty, mogelijkheden.Select(p => p).Where(p => p != 0)));
        }

        private int GetMogelijkhedenInVak(int[,] sudokuBoard, int _rij, int _kolom)
        {
            int[] mogelijkheden = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Zoek in welk vak de cel zich bevindt
            var map = _mapper.Find(_rij, _kolom);

            // Loop over het hele vak waarin de cel zich bevind
            for (int kolom = map.StartKolom; kolom <= map.StartKolom + 2; kolom++)
            {
                for (int rij = map.StartRij; rij <= map.StartRij + 2; rij++)
                {
                    // Als de celwaarde een geldige waarde is
                    if (IsValidSingle(sudokuBoard[rij, kolom]))
                    {
                        // De waarde staat op index "waarde - 1", de waarde gelijk stellen aan nul
                        mogelijkheden[sudokuBoard[rij, kolom] - 1] = 0;
                    }
                }
            }

            // Selecteer alle waardes in 'mogelijkheden' die niet gelijk zijn aan nul en zet deze in een string
            return Convert.ToInt32(String.Join(string.Empty, mogelijkheden.Select(p => p).Where(p => p != 0)));
        }

        private bool IsValidSingle(int waarde)
        {
            return waarde != 0 && waarde.ToString().Length == 1;
        }

        private int GetMogelijkheden(int mogelijkhedenInRijAndKolom, int mogelijkhedenInBlok)
        {
            // Zet alle mogelijkheden uit de rij en kolom in een array
            var mogelijkhedenInRijAndKolomCharArray = mogelijkhedenInRijAndKolom.ToString().ToCharArray();
            // Zet alle mogelijkheden uit het blok in een array
            var mogelijkhedenInBlokCharArray = mogelijkhedenInBlok.ToString().ToCharArray();
            // Vergelijk de vakken en return wat ze met elkaar gemeen hebben
            var mogelijkheden = mogelijkhedenInRijAndKolomCharArray.Intersect(mogelijkhedenInBlokCharArray);
            return Convert.ToInt32(string.Join(string.Empty, mogelijkheden));
        }



    }
}
