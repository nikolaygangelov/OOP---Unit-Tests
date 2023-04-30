using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            [Test]
            public void TestConstructor()
            {
                Garage garage = new Garage("garage", 10);

                Assert.That("garage", Is.EqualTo(garage.Name));
                Assert.That(10, Is.EqualTo(garage.MechanicsAvailable));
                Assert.That(0, Is.EqualTo(garage.CarsInGarage));
            }

            [Test]
            [TestCase(null)]
            [TestCase("")]
            public void IfGarageNameIsNullOrEmpty(string name)
            {
                ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new Garage(name, 10));
                Assert.That(exception.ParamName, Is.EqualTo("value"));
            }

            [Test]
            [TestCase(0)]
            [TestCase(-1)]
            public void IfGarageIsLessThanOne(int number)
            {
                ArgumentException exception = Assert.Throws<ArgumentException>(() => new Garage("garage", number));
                Assert.That(exception.Message, Is.EqualTo("At least one mechanic must work in the garage."));
            }


            [Test]
            public void IsAddCarsCorrectly()
            {
                Garage garage = new Garage("garage", 1);
                garage.AddCar(new Car("Megane", 1));

                Assert.That(garage.CarsInGarage, Is.EqualTo(1));
            }

            [Test]
            public void IfCarIsFound()
            {
                Garage garage = new Garage("garage", 1);
                Car car = new Car("Megane", 1);
                garage.AddCar(car);

                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => garage.FixCar("Meganesss"));
                Assert.That(exception.Message, Is.EqualTo($"The car {"Meganesss"} doesn't exist."));
            }

            [Test]
            public void IsCarFixed()
            {
                Garage garage = new Garage("garage", 1);
                Car car = new Car("Megane", 1);
                garage.AddCar(car);

                var result = garage.FixCar("Megane");

                Assert.That(result.NumberOfIssues, Is.EqualTo(0));
            }

            [Test]
            public void AreThereCarsToRemove()
            {
                Garage garage = new Garage("garage", 1);
                Car car = new Car("Megane", 1);
                garage.AddCar(car);

                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => garage.RemoveFixedCar());
                Assert.That(exception.Message, Is.EqualTo($"No fixed cars available."));
            }

            [Test]
            public void IfRepairedCarsAreRemoved()
            {
                Garage garage = new Garage("garage", 1);
                Car car = new Car("Megane", 1);
                garage.AddCar(car);
                garage.FixCar("Megane");

                var result = garage.RemoveFixedCar();

                Assert.That(result, Is.EqualTo(1));
            }

            [Test]
            public void TestingGarageReport()
            {
                Garage garage = new Garage("garage", 10);
                Car car1 = new Car("Megane", 1);
                Car car2 = new Car("Honda", 1);
                garage.AddCar(car1);
                garage.AddCar(car2);

                var result = garage.Report();

                Assert.That(result, Is.EqualTo($"There are {2} which are not fixed: {"Megane, Honda"}."));
            }
        }
    }
}