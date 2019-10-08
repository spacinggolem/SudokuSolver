using System.Text;

namespace SudokuSolverConsole.Workers
{
    class BoardStateManager
    {
        public string GenerateState(int[,] sudokuBoard)
        {
             // De key van een bord is heel simpel, het is een string met alle waarden van elk vak
              

            var key = new StringBuilder();
            // Voor elke rij
            for (int rij = 0; rij < sudokuBoard.GetLength(0); rij++)
            {
                // Voor elke kolom
                for (int kolom = 0; kolom < sudokuBoard.GetLength(1); kolom++)
                {
                    // Waarde aan de key toevoegen
                    key.Append(sudokuBoard[rij, kolom]);
                }

            }
            // Key returnen
            return key.ToString();
        }

        // Een vakje is opgelost als de waarde niet gelijk is aan 0 (=leeg) EN bestaat uit 1 karakter. 
        // Als er meerde waardes nog mogelijk zijn dan staan alle mogelijke waardes namelijk in het vak

        public bool IsSolved(int[,] sudokuBoard)
        {

            for (int rij = 0; rij < sudokuBoard.GetLength(0); rij++)
            {
                for (int kolom = 0; kolom < sudokuBoard.GetLength(1); kolom++)
                {
                    if (sudokuBoard[rij, kolom] == 0 || sudokuBoard[rij, kolom].ToString().Length > 1)
                    {
                        return false;
                    }
                }

            }

            return true;
        }
    }
}
