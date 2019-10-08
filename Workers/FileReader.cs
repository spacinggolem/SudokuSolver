using System;
using System.IO;

namespace SudokuSolverConsole.Workers
{
    class FileReader
    {
        public int[,] ReadFile(string filename)
        {
            // Init multi dementional array
            int[,] sudokuBoard = new int[9, 9];

            // Bestand lezen
            try
            {
                // Alle regels in het bestand lezen
                var sudokuBoardLines = File.ReadAllLines(filename);

                // 1 regel in het bestand is 1 rij in het spel

                int rij = 0;
                // Voor elke rij
                foreach (var sudokuBoardLine in sudokuBoardLines)
                {
                    // Alle waardes uit de regel in een array zetten
                    string[] sudokuBoardLineElements = sudokuBoardLine.Split('|', StringSplitOptions.RemoveEmptyEntries);


                    int kolom = 0;
                    // Voor elke kolom in de rij
                    foreach (var sudokuBoardLineElement in sudokuBoardLineElements)
                    {
                        // Het vakje in (rij, kolom) de waarde geven.
                        // Als de waarde een spatie is dan is de waarde van het vakje 0, anders is het de waarde als Integer
                        sudokuBoard[rij, kolom] = sudokuBoardLineElement.Equals(" ") ? 0 : Convert.ToInt16(sudokuBoardLineElement);

                        // Nieuwe kolom selecteren
                        kolom++;
                    }
                    // Nieuwe rij selecteren
                    rij++;
                }

            }
            catch (Exception ex)
            {
                // Als er iets fout gaat tijdens het lezen van het bestand een error sturen
                throw new Exception("Error tijdens het lezen van het bestand: " + ex.Message);
            }

            // Als alles goed gaat, sudokuBoard sturen
            return sudokuBoard;
        }
    }
}
