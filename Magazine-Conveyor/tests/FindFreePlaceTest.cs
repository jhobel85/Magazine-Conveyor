using Magazine_Conveyor.Model;
using Magazine_Conveyor.Service;


namespace Magazine_Conveyor.Tests
{
    public class FindFreePlaceTest
    {
        private Magazine mag;

        [SetUp]
        public void SetUp()
        {
            mag = Magazine.GetInstance(12, false);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void SingleFreePlace(bool isRotary)
        {
            bool[] places = [true, false, true, true];
            int neededPlaces = 1;
            int expected = 1;
            int result = mag.FindFreePlace(places, isRotary, neededPlaces);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void MultipleFreePlaces(bool isRotary)
        {
            bool[] places = [true, false, false, true, false, true];
            int neededPlaces = 2;
            int expected = 1;
            int result = mag.FindFreePlace(places, isRotary, neededPlaces);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void MultipleFreePlaces_TwoSuitable(bool isRotary)
        {
            bool[] places = [true, true, false, false, false, true, false, false, false];
            int neededPlaces = 3;
            int expected = 2;
            int result = mag.FindFreePlace(places, isRotary, neededPlaces);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void AllFreePlaces_AllNeeded(bool isRotary)
        {
            bool[] places = [false, false, false, false];
            int neededPlaces = 4;
            int expected = 0;
            int result = mag.FindFreePlace(places, isRotary, neededPlaces);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(0, 1, -1)]
        [TestCase(1, 2, 9)]
        [TestCase(2, 3, 8)]
        [TestCase(2, 4, 8)]
        [TestCase(3, 4, 7)]
        [TestCase(3, 5, 7)]
        [TestCase(3, 6, 7)]
        [TestCase(4, 8, 6)]
        [TestCase(4, 9, -1)]
        [TestCase(5, 10, 0)] // all free -> start on first position
        public void RotaryTest_OccupyBorders(int nrOfFreePlacesFromBeginAndFromEnd, int neededPlaces, int expected)
        {
            bool[] places = [true, true, true, true, true, true, true, true, true, true]; // 10 values

            //begin case
            for (int i = 0; i < nrOfFreePlacesFromBeginAndFromEnd; i++)
            {
                places[i] = false;
            }

            //end case
            for (int i = 0; i < nrOfFreePlacesFromBeginAndFromEnd; i++)
            {
                places[places.Length - 1 - i] = false;
            }

            int result = mag.FindFreePlace(places, true, neededPlaces);
            Assert.That(result, Is.EqualTo(expected));
        }

        /* NEGATIVE SCENARIOS*/

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Neg_PlacesAreEmpty(bool isRotary)
        {
            bool[] places = [];
            int neededPlaces = 10;
            int expected = -1;
            int result = mag.FindFreePlace(places, isRotary, neededPlaces);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Neg_NoPlacesNeeded(bool isRotary)
        {
            bool[] places = [false, false, false, false];
            int neededPlaces = 0;
            int expected = -1;
            int result = mag.FindFreePlace(places, isRotary, neededPlaces);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Neg_NoPlacesNeeded_AllBooked(bool isRotary)
        {
            bool[] places = [true, true];
            int neededPlaces = 0;
            int expected = -1;
            int result = mag.FindFreePlace(places, isRotary, neededPlaces);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Neg_NeededPlacesHigherThanMaxLength(bool isRotary)
        {
            bool[] places = [false, false, false];
            int neededPlaces = 4;
            int expected = -1;
            int result = mag.FindFreePlace(places, isRotary, neededPlaces);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Neg_NeededPlacesHigherThanMaxAvailable(bool isRotary)
        {
            bool[] places = [true, false, false];
            int neededPlaces = 3;
            int expected = -1;
            int result = mag.FindFreePlace(places, isRotary, neededPlaces);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Neg_NoFreePlaces(bool isRotary)
        {
            bool[] places = [true, true, true, true];
            int neededPlaces = 1;
            int expected = -1;
            int result = mag.FindFreePlace(places, isRotary, neededPlaces);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Neg_MultipleFreePlaces_NoSuitable(bool isRotary)
        {
            bool[] places = [true, true, false, false, true, true, false, false, true];
            int neededPlaces = 3;
            int expected = -1;
            int result = mag.FindFreePlace(places, isRotary, neededPlaces);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
