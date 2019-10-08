using SudokuSolverConsole.Data;

namespace SudokuSolverConsole.Workers
{
    class Mapper
    {
        public Map Find(int _rij, int _kolom)
        {
            /* 
            Een sudoku vak is 3x3 (3 rijen bij 3 kolommen) als je weet in welke rij en kolom het vakje zit kan je
            makkelijk bepalen in welk vak het zit. 

            Voorbeelden [vakje(rij, kolom)]:
        
            vakje(1,4)
            Het vakje zit in de eerste (bovenste) rij, het zit dus in een van de bovenste vakken
            Het vakje zit in de vierde kolom, het zit dus in het middelste vak
            Het vakje zit in de bovenste rij in het middelste vak

            Door de 'startCell' te selecteren is het daarna makkelijk om over het vak te loopen.
            De startCell zit altijd linksbovenin het vak
                */


            Map sudokuMap = new Map();

            // Vak zit in de meest linkse kolom
            if (_kolom >= 0 && _kolom <= 2)
            {
                sudokuMap.StartKolom = 0;
            }
            // Vak zit in de middelste kolom
            else if (_kolom >= 3 && _kolom <= 5)
            {
                sudokuMap.StartKolom = 3;
            }
            // Vak zit in de meest rechtse kolom
            else
            {
                sudokuMap.StartKolom = 6;
            }

            // Vak zit in de bovenste rij van vakken
            if ((_rij >= 0 && _rij <= 2))
                sudokuMap.StartRij = 0;
            // Vak zit in de middelste rij van vakken
            else if ((_rij >= 3 && _rij <= 5))
                sudokuMap.StartRij = 3; 
            // Vak zit in de onderste rij van vakken
            else
                sudokuMap.StartRij = 6;

            return sudokuMap;
        }
    }
}
