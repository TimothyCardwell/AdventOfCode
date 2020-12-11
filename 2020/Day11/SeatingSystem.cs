namespace Day11
{
    public class SeatingSystem
    {
        public bool IsStable { get; private set; }
        private Seat[,] _seatLayout;
        private readonly int _seatLayoutWidth; // Used for readability later
        private readonly int _seatLayoutHeight; // Used for readability later

        public SeatingSystem(string[] rows)
        {
            _seatLayout = new Seat[rows.Length, rows[0].ToCharArray().Length];
            var i = 0;
            foreach (var row in rows)
            {
                var j = 0;
                var positionsInRow = row.ToCharArray();
                foreach (var positionInRow in positionsInRow)
                {
                    _seatLayout[i, j] = new Seat(positionInRow);
                    j++;
                }

                i++;
            }

            _seatLayoutWidth = _seatLayout.GetLength(0);
            _seatLayoutHeight = _seatLayout.GetLength(1);
        }

        public void ExecuteSeatRules()
        {
            if (IsStable)
            {
                return;
            }

            var hasLayoutChanged = false;

            var newSeatLayout = CloneSeatingSystem();
            for (var i = 0; i < _seatLayoutWidth; i++)
            {
                for (var j = 0; j < _seatLayoutHeight; j++)
                {
                    var currentSeat = _seatLayout[i, j];
                    if (currentSeat.IsSeat)
                    {
                        var adjacentOccupiedSeatCount = GetAdjacentOccupiedSeatCountPart2(i, j);

                        // Seat is now occupied
                        if (!currentSeat.IsOccupied && adjacentOccupiedSeatCount == 0)
                        {
                            newSeatLayout[i, j].IsOccupied = true;
                            hasLayoutChanged = true;
                        }

                        // Seat is now unoccupied
                        if (currentSeat.IsOccupied && adjacentOccupiedSeatCount >= 5) // 4 for Part 1
                        {
                            newSeatLayout[i, j].IsOccupied = false;
                            hasLayoutChanged = true;
                        }
                    }
                }
            }

            if (!hasLayoutChanged)
            {
                IsStable = true;
            }

            _seatLayout = newSeatLayout;
        }

        public int GetOccupiedSeatCount()
        {
            var occupiedSeatCount = 0;
            for (var i = 0; i < _seatLayoutWidth; i++)
            {
                for (var j = 0; j < _seatLayoutHeight; j++)
                {
                    if (_seatLayout[i, j].IsOccupied) occupiedSeatCount++;
                }
            }

            return occupiedSeatCount;
        }

        private int GetAdjacentOccupiedSeatCountPart1(int i, int j)
        {
            var occupiedSeatCount = 0;

            // Left
            if (i > 0 && _seatLayout[i - 1, j].IsOccupied) occupiedSeatCount++;

            // Right
            if (i < _seatLayoutWidth - 1 && _seatLayout[i + 1, j].IsOccupied) occupiedSeatCount++;

            // Top
            if (j > 0 && _seatLayout[i, j - 1].IsOccupied) occupiedSeatCount++;

            // Bottom
            if (j < _seatLayoutHeight - 1 && _seatLayout[i, j + 1].IsOccupied) occupiedSeatCount++;

            // Top Left
            if (i > 0 && j > 0 && _seatLayout[i - 1, j - 1].IsOccupied) occupiedSeatCount++;

            // Top Right
            if (i < _seatLayoutWidth - 1 && j > 0 && _seatLayout[i + 1, j - 1].IsOccupied) occupiedSeatCount++;

            // Bottom Left
            if (i > 0 && j < _seatLayoutHeight - 1 && _seatLayout[i - 1, j + 1].IsOccupied) occupiedSeatCount++;

            // Bottom Right
            if (i < _seatLayoutWidth - 1 && j < _seatLayoutHeight - 1 && _seatLayout[i + 1, j + 1].IsOccupied) occupiedSeatCount++;

            return occupiedSeatCount;
        }

        private int GetAdjacentOccupiedSeatCountPart2(int i, int j)
        {
            var occupiedSeatCount = 0;
            var iIterator = i;
            var jIterator = j;

            // Left
            iIterator = i;
            while (iIterator > 0)
            {
                var currentSeat = _seatLayout[iIterator - 1, j];
                if (currentSeat.IsSeat)
                {
                    if (currentSeat.IsOccupied) occupiedSeatCount++;
                    break;
                }

                iIterator--;
            }

            // Right
            iIterator = i;
            while (iIterator < _seatLayoutWidth - 1)
            {
                var currentSeat = _seatLayout[iIterator + 1, j];
                if (currentSeat.IsSeat)
                {
                    if (currentSeat.IsOccupied) occupiedSeatCount++;
                    break;
                }

                iIterator++;
            }

            // Top
            jIterator = j;
            while (jIterator > 0)
            {
                var currentSeat = _seatLayout[i, jIterator - 1];
                if (currentSeat.IsSeat)
                {
                    if (currentSeat.IsOccupied) occupiedSeatCount++;
                    break;
                }

                jIterator--;
            }

            // Bottom
            jIterator = j;
            while (jIterator < _seatLayoutHeight - 1)
            {
                var currentSeat = _seatLayout[i, jIterator + 1];
                if (currentSeat.IsSeat)
                {
                    if (currentSeat.IsOccupied) occupiedSeatCount++;
                    break;
                }

                jIterator++;
            }

            // Top Left
            iIterator = i;
            jIterator = j;
            while (iIterator > 0 && jIterator > 0)
            {
                var currentSeat = _seatLayout[iIterator - 1, jIterator - 1];
                if (currentSeat.IsSeat)
                {
                    if (currentSeat.IsOccupied) occupiedSeatCount++;
                    break;
                }

                iIterator--;
                jIterator--;
            }

            // Top Right
            iIterator = i;
            jIterator = j;
            while (iIterator < _seatLayoutWidth - 1 && jIterator > 0)
            {
                var currentSeat = _seatLayout[iIterator + 1, jIterator - 1];
                if (currentSeat.IsSeat)
                {
                    if (currentSeat.IsOccupied) occupiedSeatCount++;
                    break;
                }

                iIterator++;
                jIterator--;
            }

            // Bottom Left
            iIterator = i;
            jIterator = j;
            while (iIterator > 0 && jIterator < _seatLayoutHeight - 1)
            {
                var currentSeat = _seatLayout[iIterator - 1, jIterator + 1];
                if (currentSeat.IsSeat)
                {
                    if (currentSeat.IsOccupied) occupiedSeatCount++;
                    break;
                }

                iIterator--;
                jIterator++;
            }

            // Bottom Right
            iIterator = i;
            jIterator = j;
            while (iIterator < _seatLayoutWidth - 1 && jIterator < _seatLayoutHeight - 1)
            {
                var currentSeat = _seatLayout[iIterator + 1, jIterator + 1];
                if (currentSeat.IsSeat)
                {
                    if (currentSeat.IsOccupied) occupiedSeatCount++;
                    break;
                }

                iIterator++;
                jIterator++;
            }

            return occupiedSeatCount;
        }

        private Seat[,] CloneSeatingSystem()
        {
            var clonedSeatingLayout = new Seat[_seatLayoutWidth, _seatLayoutHeight];
            for (var i = 0; i < _seatLayoutWidth; i++)
            {
                for (var j = 0; j < _seatLayoutHeight; j++)
                {
                    clonedSeatingLayout[i, j] = (Seat)_seatLayout[i, j].Clone();
                }
            }

            return clonedSeatingLayout;
        }
    }
}