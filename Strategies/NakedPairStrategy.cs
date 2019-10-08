using SudokuSolverConsole.Workers;
using System;

namespace SudokuSolverConsole.Strategies
{
    class NakedPairStrategy : IStrategy
    {
        private readonly Mapper _mapper;

        public NakedPairStrategy(Mapper mapper)
        {
            _mapper = mapper;
        }

        public int[,] Solve(int[,] sudokuBoard)
        {
            // Voor elke rij
            for (int rij = 0; rij < sudokuBoard.GetLength(0); rij++)
            {
                // Voor elke kolom
                for (int kolom = 0; kolom < sudokuBoard.GetLength(1); kolom++)
                {
                    // Alle 'naked pairs' verwijderen
                    ElimineerNakedPairVanAndereInRij(sudokuBoard, rij, kolom);
                    ElimineerNakedPairVanAndereInKolom(sudokuBoard, rij, kolom);
                    ElimineerNakedPairVanAndereInBlok(sudokuBoard, rij, kolom);
                }

            }
            return sudokuBoard;
        }
        // Naked pair uit de rij verwijderen
        private void ElimineerNakedPairVanAndereInRij(int[,] sudokuBoard, int _rij, int _kolom)
        {
            // Als er geen naked pair is hoeft er niks te gebeuren
            if (!NakedPairInRij(sudokuBoard, _rij, _kolom)) return;

            // Voor elke kolom in de rij 
            for (int kolom = 0; kolom < sudokuBoard.GetLength(1); kolom++)
            {
                // Als de waarde van de kolom in de geselecteerde rij niet hetzelfde is als van de geselcteerde rij en geselecteerde kolom
                // EN de lengte van de kolom is langer dan 1 teken
                if (sudokuBoard[_rij, kolom] != sudokuBoard[_rij, _kolom] && sudokuBoard[_rij, kolom].ToString().Length > 1)
                {
                    // Naked pair verwijderen met waarde van de cel op de geselecteerde rij en kolom
                    // Uit cel op de geselecteerde rij en kolom uit loop
                    ElimineerNakedPair(sudokuBoard, sudokuBoard[_rij, _kolom], _rij, kolom);
                }
            }
        }

        // Kijken of er een naked pair in de rij zit
        private bool NakedPairInRij(int[,] sudokuBoard, int _rij, int _kolom)
        {
            // Voor elke kolom in de geselecteerde rij
            for (int kolom = 0; kolom < sudokuBoard.GetLength(1); kolom++)
            {
                // Als de geselecteerde kolom niet hetzelfde is als de loop kolom EN er zit een naked pair in de rij dan zit er een naked pair in de rij
                if (_kolom != kolom && IsNakedPair(sudokuBoard[_rij, kolom], sudokuBoard[_rij, _kolom])) return true;
            }
            // Anders is er geen naked pair in de rij
            return false;
        }

        // Naked pair uit kolom verwijderen 
        private void ElimineerNakedPairVanAndereInKolom(int[,] sudokuBoard, int _rij, int _kolom)
        {
            // Als er geen naked pair in de kolom hoeft er niks te gebeuren
            if (!NakedPairInKolom(sudokuBoard, _rij, _kolom)) return;

            // Voor elke rij in de geselecteerde kolom
            for (int rij = 0; rij < sudokuBoard.GetLength(0); rij++)
            {
                // Als de rij in de geselecteerde kolom niet gelijk is aan de geselecteerde rij en kolom en de waarde is langer dan 1 cijfer
                if (sudokuBoard[rij, _kolom] != sudokuBoard[_rij, _kolom] && sudokuBoard[rij, _kolom].ToString().Length > 1)
                {
                    // Elimineer naked pair met waarde van geselecteerde rij en geselcteerde kolom uit de cel op loop rij en geselecteerde kolom
                    ElimineerNakedPair(sudokuBoard, sudokuBoard[_rij, _kolom], rij, _kolom);
                }
            }
        }

        // Kijken of er een naked pair in de kolom zit
        private bool NakedPairInKolom(int[,] sudokuBoard, int _rij, int _kolom)
        {
            // Voor elke rij in de geselecteerdee kolom
            for (int rij = 0; rij < sudokuBoard.GetLength(0); rij++)
            {
                // Als de geselecteerde rij niet gelijk is aan de loop rij EN er zit een naked pair in de rij dan is er een naked pair in de kolom
                if (_rij != rij && IsNakedPair(sudokuBoard[rij, _kolom], sudokuBoard[_rij, _kolom])) return true;
            }
            // Anders is er geen naked pair
            return false;
        }

        // Kijken of er een naked pair in het blok zit
        private void ElimineerNakedPairVanAndereInBlok(int[,] sudokuBoard, int _rij, int _kolom)
        {
            // Als er geen naked pair in de kolom hoeft er niks te gebeuren
            if (!NakedPairInBlok(sudokuBoard, _rij, _kolom)) return;

            // Bepalen in welk blok de cel zit
            var map = _mapper.Find(_rij, _kolom);

            // Over het hele blok heen loopen
            for (int rij = map.StartRij; rij <= map.StartRij + 2; rij++)
            {
                for (int kolom = map.StartKolom; kolom <= map.StartKolom + 2; kolom++)
                {
                    // Als de waarde van de cel langer is dan 2 cijfers en de loop rij en kolom is niet hetzelfde als de geselecteerde rij en kolom
                    if (sudokuBoard[rij, kolom].ToString().Length > 2 && sudokuBoard[rij, kolom] != sudokuBoard[_rij, _kolom])
                    {
                        // Verwijder naked pair met waarde van de geselecteerde rij en kolom op loop rij en kolom
                        ElimineerNakedPair(sudokuBoard, sudokuBoard[_rij, _kolom], rij, kolom);
                    }
                }
            }
        }

        // Kijken of er een naked pair in het blok zit
        private bool NakedPairInBlok(int[,] sudokuBoard, int _rij, int _kolom)
        {
            // Voor elke rij
            for (int rij = 0; rij < sudokuBoard.GetLength(0); rij++)
            {
                // Voor elke kolom in de rij
                for (int kolom = 0; kolom < sudokuBoard.GetLength(1); kolom++)
                {
                    // Kijken of het hetzelfde 'element'/cel is
                    var elementHetzelfde = _rij == rij && _kolom == kolom;
                    // Kijken of het element/cel in hetzelfde blok zit
                    var elementInZelfdeBlok = _mapper.Find(_rij, _kolom).StartRij == _mapper.Find(rij, kolom).StartRij &&
                        _mapper.Find(_rij, _kolom).StartKolom == _mapper.Find(rij, kolom).StartKolom;
                    // Als het element niet hetzelfde is, de element in hetzelfde blok zit en het een naked pair is dan is er een naked pair in het blok
                    if (!elementHetzelfde && elementInZelfdeBlok && IsNakedPair(sudokuBoard[_rij, _kolom], sudokuBoard[rij, kolom]))
                    {
                        return true;
                    }
                }

            }
            // Anders is er geen naked pair
            return false;
        }

        // Elmineer een naked pair
        private void ElimineerNakedPair(int[,] sudokuBoard, int waardesOmTeElimineren, int ElimineerUitRij, int ElimineerUitkolom)
        {
            // Array maken van alle waardes die verwijdert moeten worden
            var WaardesOmTeEliminerenArray = waardesOmTeElimineren.ToString().ToCharArray();

            // Voor elke waarde in de array
            foreach (var waardeOmTeElimineren in WaardesOmTeEliminerenArray)
            {
                // De waarde van de cel op de plaats waar de naked pair zit
                // De waarde van deze cel wordt omgezet naar een string en de waarde die moet worden verwijdert wordt
                // vervangen door een empty string/ verwijdert
                sudokuBoard[ElimineerUitRij, ElimineerUitkolom] = Convert.ToInt32(sudokuBoard[ElimineerUitRij,
                    ElimineerUitkolom].ToString().Replace(waardeOmTeElimineren.ToString(), string.Empty));
            }
        }

        // Kijken of het een naked pair is
        private bool IsNakedPair(int paar1, int paar2)
        {
            // De lengte moet 2 zijn en de paren moeten dezelfde waarde hebben
            return paar1.ToString().Length == 2 && paar1 == paar2;
        }
    }
}
