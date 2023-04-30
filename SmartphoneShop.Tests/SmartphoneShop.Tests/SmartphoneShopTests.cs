using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        [Test]
        public void TestConstructor()
        {
            Shop shop = new Shop(10);

            Assert.That(10, Is.EqualTo(shop.Capacity));
            Assert.That(0, Is.EqualTo(shop.Count));
        }

        [Test]
        public void IfCapacityIsLessThanZero()
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new Shop(-1));
            Assert.That(exception.Message, Is.EqualTo("Invalid capacity."));
        }

        [Test]
        public void IsAddPhonesCorrectly()
        {
            Shop shop = new Shop(10);
            Smartphone smartphone = new Smartphone("MiA3", 5000);
            shop.Add(smartphone);

            Assert.That(shop.Count, Is.EqualTo(1));
        }

        [Test]
        public void IfPhoneIsFound()
        {
            Shop shop = new Shop(10);
            Smartphone smartphone = new Smartphone("MiA3", 5000);
            shop.Add(smartphone);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => shop.Add(smartphone));
            Assert.That(exception.Message, Is.EqualTo($"The phone model {smartphone.ModelName} already exist."));
        }

        [Test]
        public void IfShopIsFull()
        {
            Shop shop = new Shop(1);
            Smartphone smartphone1 = new Smartphone("MiA3", 5000);
            Smartphone smartphone2 = new Smartphone("MiA4", 5000);
            shop.Add(smartphone1);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => shop.Add(smartphone2));
            Assert.That(exception.Message, Is.EqualTo("The shop is full."));
        }

        [Test]
        public void AreTherePhonessToRemove()
        {
            Shop shop = new Shop(2);
            Smartphone smartphone1 = new Smartphone("MiA3", 5000);
            Smartphone smartphone2 = new Smartphone("MiA4", 5000);
            shop.Add(smartphone1);
            shop.Add(smartphone2);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => shop.Remove("MiA5"));
            Assert.That(exception.Message, Is.EqualTo($"The phone model {"MiA5"} doesn't exist."));
        }

        [Test]
        public void IfPhonesAreRemoved()
        {
            Shop shop = new Shop(2);
            Smartphone smartphone1 = new Smartphone("MiA3", 5000);
            Smartphone smartphone2 = new Smartphone("MiA4", 5000);
            shop.Add(smartphone1);
            shop.Add(smartphone2);
            shop.Remove("MiA3");


            Assert.That(1, Is.EqualTo(shop.Count));
        }

        [Test]
        public void AreTherePhonessToTest()
        {
            Shop shop = new Shop(2);
            Smartphone smartphone1 = new Smartphone("MiA3", 5000);
            Smartphone smartphone2 = new Smartphone("MiA4", 5000);
            shop.Add(smartphone1);
            shop.Add(smartphone2);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => shop.TestPhone("MiA5", 5000));
            Assert.That(exception.Message, Is.EqualTo($"The phone model {"MiA5"} doesn't exist."));
        }

        [Test]
        public void IfCurrentBateryChargeIsLessThanBatteryUsage()
        {
            Shop shop = new Shop(2);
            Smartphone smartphone1 = new Smartphone("MiA3", 5000);
            Smartphone smartphone2 = new Smartphone("MiA4", 5000);
            shop.Add(smartphone1);
            shop.Add(smartphone2);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => shop.TestPhone("MiA3", 6000));
            Assert.That(exception.Message, Is.EqualTo($"The phone model {"MiA3"} is low on batery."));
        }

        [Test]
        public void IfCurrentBateryChargeIsGreaterThanBatteryUsage()
        {
            Shop shop = new Shop(2);
            Smartphone smartphone1 = new Smartphone("MiA3", 5000);
            Smartphone smartphone2 = new Smartphone("MiA4", 5000);
            shop.Add(smartphone1);
            shop.Add(smartphone2);

            shop.TestPhone("MiA3", 4000);

            Assert.That(1000, Is.EqualTo(smartphone1.CurrentBateryCharge));
        }

        [Test]
        public void AreTherePhonesToCharge()
        {
            Shop shop = new Shop(2);
            Smartphone smartphone1 = new Smartphone("MiA3", 5000);
            Smartphone smartphone2 = new Smartphone("MiA4", 5000);
            shop.Add(smartphone1);
            shop.Add(smartphone2);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => shop.ChargePhone("MiA5"));
            Assert.That(exception.Message, Is.EqualTo($"The phone model {"MiA5"} doesn't exist."));
        }

        [Test]
        public void IfPhoneIsCharged()
        {
            Shop shop = new Shop(2);
            Smartphone smartphone1 = new Smartphone("MiA3", 5000);
            Smartphone smartphone2 = new Smartphone("MiA4", 5000);
            shop.Add(smartphone1);
            shop.Add(smartphone2);

            shop.TestPhone("MiA3", 4000);
            shop.ChargePhone("MiA3");

            //Assert.That(5000, Is.EqualTo(smartphone1.CurrentBateryCharge));
            //Assert.That(5000, Is.EqualTo(smartphone1.MaximumBatteryCharge));
            Assert.That(smartphone1.CurrentBateryCharge, Is.EqualTo(smartphone1.MaximumBatteryCharge));
        }
    }
}