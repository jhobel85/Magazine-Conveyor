using Magazine_WPF.Model;

namespace Magazine_WPF.Service
{
    public class FindFreePlaceService : IService
    {
        private readonly IMagazine reciever;
        private int freePlace;

        public FindFreePlaceService(IMagazine reciever) {
            this.reciever = reciever;
        }

        public int FreePlace { get => freePlace; set => freePlace = value; }

        public void Execute()
        {
            var places = reciever.Places;
            var rot = reciever.IsRotary;
            var neededPlaces = reciever.NeededPlaces;
            freePlace = reciever.FindFreePlace(places, rot, neededPlaces);
            reciever.UpdatePossitionsOccupancy(freePlace);
        }
    }
}
