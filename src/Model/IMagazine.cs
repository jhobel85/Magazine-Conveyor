using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine_WPF.Model
{
    public interface IMagazine
    {
        public bool[] Places { get; }
        bool IsRotary { get; }
        int NeededPlaces { get; }

        /// <summary>
        /// Function to return the free place in the magazine level
        /// </summary>
        /// <param name="places">Array of bools of dimension "n". True means occupied position, false means available</param>
        /// <param name="isRotary">Flag whether the level is rotary (last position is neighbour of the first one)</param>
        /// <param name="neededPlaces">Number of places needed</param>
        /// <returns>Index of the first position found (zero based) or -1 if no position is found</returns>
        public int FindFreePlace(bool[] places, bool isRotary, int neededPlaces);

        public void UpdatePossitionsOccupancy(int freePlace);
    }
}
