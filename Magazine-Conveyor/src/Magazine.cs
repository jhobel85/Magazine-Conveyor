using System;
using System.Collections.Generic;
using System.Linq;

namespace Magazine_Conveyor
{
    /// <summary>
    /// Model class representing the magazine data and business logic.
    /// Pure Model - no UI concerns, no INotifyPropertyChanged.
    /// </summary>
    public class Magazine : IMagazine
    {
        private bool isRotary;
        public static readonly int PLACES_AVAILABLE_DEFAULT = 50;
        public static readonly int NEEDED_PLACES_DEFAULT = 3;
        private int positionCount;
        private int neededPlaces;
        private List<Position> positions;
        private static Magazine? instance;

        public static Magazine GetInstance(int positionsCount, bool isRotary = false, bool createAlwaysNew = true)
        {
            if (instance == null || createAlwaysNew)
            {
                bool positionsVisibleByDefault = true;
                instance = new Magazine(positionsCount, isRotary)
                {
                    Positions = Enumerable.Range(0, positionsCount).Select(i => new Position(positionsVisibleByDefault)).ToList()
                };
            }
            return instance;
        }

        private Magazine(int positionsCount, bool isRotary = false)
        {
            this.positionCount = positionsCount;
            this.isRotary = isRotary;
            this.positions = [];
        }

        public List<Position> Positions { get => positions; private set => positions = value; }
        
        public bool IsRotary
        {
            get => isRotary;
            set => isRotary = value;
        }
        
        public int PositionCount
        {
            get => positionCount;
            set => positionCount = value;
        }
        
        public int NeededPlaces
        {
            get => neededPlaces;
            set => neededPlaces = value;
        }

        /// <summary>
        /// Get information about all current positions (places) whether the place is occupied or not.
        /// </summary>
        public bool[] Places
        {
            get
            {
                var visiblePos = Positions.Where(p => p.IsVisible);
                IEnumerable<bool> occupied = visiblePos.Select(i => i.IsChecked);
                return occupied.ToArray();
            }
        }

        /// <summary>
        /// Based on selected PositionCount make each Position visible or not.
        /// </summary>
        public void UpdatePositionsVisibility()
        {
            for (int i = 0; i < Positions.Count; i++)
            {
                bool select = i <= PositionCount - 1;
                Positions[i].IsVisible = select;
            }
        }

        /// <summary>
        /// Mark positions as occupied starting from freePlace index.
        /// Returns -1 if no suitable position found.
        /// Marks the Positions as occupied. Place == Index
        /// </summary>
        public void UpdatePossitionsOccupancy(int freePlace)
        {
            if (freePlace >= 0)
            {
                var lastPlace = freePlace + NeededPlaces;

                for (int i = freePlace; i < lastPlace; i++)
                {
                    int j = i;
                    var lastIndex = Positions.Count - 1;
                    if (i > lastIndex && IsRotary)
                    {
                        j = i - lastIndex - 1;
                    }

                    Positions[j].IsChecked = true;
                }
            }
        }

        /// <summary>
        /// Function to return the free place in the magazine level
        /// </summary>
        /// <param name="places">Array of bools of dimension "n". True means occupied position, false means available</param>
        /// <param name="isRotary">Flag whether the level is rotary (last position is neighbour of the first one)</param>
        /// <param name="neededPlaces">Number of places needed</param>
        /// <returns>Index of the first position found (zero based) or -1 if no position is found</returns>
        public int FindFreePlace(bool[] places, bool isRotary, int neededPlaces)
        {
            int ret = -1;
            int i = 0;
            bool previous = true;
            bool current = true;
            int placesFound = 0;
            bool endAlreadyReached = false;
            
            if (places.Length > 0 && neededPlaces > 0 && neededPlaces <= places.Length)
            {
                do
                {
                    previous = current;
                    current = places[i];

                    if (ret == -1 && !current)
                    {
                        ret = i;
                        placesFound = 1;
                    }
                    else if (!current && !previous)
                    {
                        placesFound++;
                    }
                    else
                    {
                        placesFound = 0;
                        ret = -1;
                    }
                    i++;
                    if (i == places.Length && isRotary && !endAlreadyReached)
                    {
                        i = 0;
                        endAlreadyReached = true;
                    }
                } while (placesFound < neededPlaces && i < places.Length);
            }

            // If not enough free places available
            if (placesFound != neededPlaces)
            {
                ret = -1;
            }
            return ret;
        }
    }
}
