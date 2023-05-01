using FrontDeskApp;
using NUnit.Framework;
using System;
using System.Linq;

namespace BookigApp.Tests
{
    public class Tests
    {
        
        [Test]
        public void TestHotelConstructor()
        {
            Hotel hotel = new Hotel("Sezoni", 5);
            Assert.AreEqual("Sezoni", hotel.FullName);
            Assert.AreEqual(5, hotel.Category);
            Assert.AreEqual(0, hotel.Bookings.Count);
            Assert.AreEqual(0, hotel.Rooms.Count);
        }

        [Test]
        public void IfFullNameIsIsNullOrWhiteSpace()
        {
            Assert.Throws<ArgumentNullException>(() => new Hotel(" ", 5));
            Assert.Throws<ArgumentNullException>(() => new Hotel(null, 5));
            Assert.Throws<ArgumentNullException>(() => new Hotel("", 5));
        }

        [Test]
        public void IfCataegoryIsLessThanOneOrGreaterThanFive()
        {
            Assert.Throws<ArgumentException>(() => new Hotel("Sezoni", 0));
            Assert.Throws<ArgumentException>(() => new Hotel("Sezoni", 6));
        }

        [Test]
        public void IfBaseTurnoverIsZero()
        {
            Hotel hotel = new Hotel("Sezoni", 5);
            Assert.AreEqual(0, hotel.Turnover);
        }

        [Test]
        public void IfMethodAddRoomWorksCorrectly()
        {
            Hotel hotel = new Hotel("Sezoni", 5);
            Room room = new Room(2, 80.5);
            
            hotel.AddRoom(room);

            Assert.AreEqual(1, hotel.Rooms.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void IfAdultsCountIsZeroOrNegative(int adultsCount)
        {
            Assert.Throws<ArgumentException>(() => new Hotel("Sezoni", 3).BookRoom(adultsCount, 4, 10, 100));
        }

        [Test]
        public void IfChildrenCountIsNegative()
        {
            Assert.Throws<ArgumentException>(() => new Hotel("Sezoni", 3).BookRoom(2, -1, 10, 100));
        }

        [Test]
        public void IfDurationIsLessThanOne()
        {
            Assert.Throws<ArgumentException>(() => new Hotel("Sezoni", 3).BookRoom(2, 2, 0, 100));
        }

        [Test]
        public void IfBedsNeededCountIsCorrect()
        {
            Hotel hotel = new Hotel("Sezoni", 5);
            Room room = new Room(4, 2);

            hotel.AddRoom(room);
            hotel.BookRoom(2, 2, 2, 100);
            Booking result = hotel.Bookings.First();

            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(1, result.BookingNumber);
            Assert.AreEqual(2, result.ResidenceDuration);
            Assert.GreaterOrEqual(result.Room.BedCapacity, 4);
            Assert.GreaterOrEqual(room.BedCapacity, result.Room.BedCapacity);
            Assert.AreEqual(2, room.PricePerNight);
            Assert.AreEqual(2, result.Room.PricePerNight);
            Assert.AreEqual(room.PricePerNight, result.Room.PricePerNight);
            Assert.AreEqual(4, hotel.Turnover);
            Assert.GreaterOrEqual(100, result.ResidenceDuration * result.Room.PricePerNight);
            Assert.GreaterOrEqual(100, result.ResidenceDuration * room.PricePerNight);

        }
    }
}