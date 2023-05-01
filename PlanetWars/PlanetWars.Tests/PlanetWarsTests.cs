using NUnit.Framework;
using System;
using System.Linq;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            [Test]
            public void TestConstructor()
            {
                Planet planet = new Planet("Earth", 100);

                Assert.That("Earth", Is.EqualTo(planet.Name));
                Assert.That(100, Is.EqualTo(planet.Budget));
                Assert.That(0, Is.EqualTo(planet.Weapons.Count));
                Assert.That(0, Is.EqualTo(planet.MilitaryPowerRatio));
            }

            [Test]
            [TestCase(null)]
            [TestCase("")]
            public void IfPlanetNameIsNullOrEmpty(string name)
            {
                ArgumentException exception = Assert.Throws<ArgumentException>(() => new Planet(name, 100));
                Assert.That(exception.Message, Is.EqualTo("Invalid planet Name"));
            }

            [Test]
            public void TestBudgetIsNegative()
            {
                ArgumentException exception = Assert.Throws<ArgumentException>(() => new Planet("Earth", -1));
                Assert.That(exception.Message, Is.EqualTo("Budget cannot drop below Zero!"));
            }

            [Test]
            public void TestingMethodProfit()
            {
                Planet planet = new Planet("Earth", 100);

                planet.Profit(10);

                Assert.That(110, Is.EqualTo(planet.Budget));

            }

            [Test]
            public void TestingMethodSpendFundsIfDecreasingBudget()
            {
                Planet planet = new Planet("Earth", 100);

                planet.SpendFunds(10);

                Assert.That(90, Is.EqualTo(planet.Budget));

            }

            [Test]
            public void TestingMethodSpendFundsWhenBudgetIsLowerThanAmount()
            {
                Planet planet = new Planet("Earth", 100);

                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => planet.SpendFunds(110));
                Assert.That(exception.Message, Is.EqualTo("Not enough funds to finalize the deal."));
            }

            [Test]
            public void AreThereDuplicatedWeaponNames()
            {
                Planet planet = new Planet("Earth", 100);
                Weapon weapon = new Weapon("Weapon1", 10, 10);

                planet.AddWeapon(weapon);

                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => planet.AddWeapon(weapon));
                Assert.That(exception.Message, Is.EqualTo($"There is already a {weapon.Name} weapon."));
            }

            [Test]
            public void IfAnWeaponIsAdded()
            {
                Planet planet = new Planet("Earth", 100);
                Weapon weapon = new Weapon("Weapon1", 10, 10);

                planet.AddWeapon(weapon);

                Assert.That(planet.Weapons.Count, Is.EqualTo(1));//!!!!!!!!!!!
            }

            [Test]
            public void IfWeaponIsRemoved()
            {
                Planet planet = new Planet("Earth", 100);
                Weapon weapon1 = new Weapon("Weapon1", 10, 10);
                Weapon weapon2 = new Weapon("Weapon2", 10, 10);
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                planet.RemoveWeapon("Weapon1");

                Assert.That(planet.Weapons.Count, Is.EqualTo(1));//!!!!!!!!!!!
                Assert.That(planet.Weapons.First().Name, Is.EqualTo("Weapon2"));
                Assert.That(planet.Weapons.First().DestructionLevel, Is.EqualTo(10));
                Assert.That(planet.Weapons.First().Price, Is.EqualTo(10));
            }

            [Test]
            public void IfWeaponNameExists()
            {
                Planet planet = new Planet("Earth", 100);
                Weapon weapon1 = new Weapon("Weapon1", 10, 10);
                planet.AddWeapon(weapon1);

                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => planet.UpgradeWeapon("hhh"));
                Assert.That(exception.Message, Is.EqualTo($"{"hhh"} does not exist in the weapon repository of {planet.Name}"));
            }

            [Test]
            public void IfWeaponDestructionLevelIsIncreased()
            {
                Planet planet = new Planet("Earth", 100);
                Weapon weapon1 = new Weapon("Weapon1", 10, 10);
                planet.AddWeapon(weapon1);

                planet.UpgradeWeapon("Weapon1");

                Assert.That(weapon1.DestructionLevel, Is.EqualTo(11));
            }

            [Test]
            public void IsOpponemtToStrongForWar()
            {
                Planet planet1 = new Planet("Earth", 100);
                Planet planet2 = new Planet("Mars", 110);
                Weapon weapon1 = new Weapon("Weapon1", 10, 10);
                Weapon weapon2 = new Weapon("Weapon2", 10, 11);
                Weapon weapon3 = new Weapon("Weapon3", 10, 12);
                planet1.AddWeapon(weapon1);
                planet2.AddWeapon(weapon2);
                planet2.AddWeapon(weapon3);

                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => planet1.DestructOpponent(planet2));
                Assert.That(exception.Message, Is.EqualTo($"{planet2.Name} is too strong to declare war to!"));
            }

            [Test]
            public void IsOpponemtDestructed()
            {
                Planet planet1 = new Planet("Earth", 100);
                Planet planet2 = new Planet("Mars", 110);
                Weapon weapon1 = new Weapon("Weapon1", 10, 10);
                Weapon weapon2 = new Weapon("Weapon2", 10, 11);
                Weapon weapon3 = new Weapon("Weapon3", 10, 12);
                planet1.AddWeapon(weapon1);
                planet2.AddWeapon(weapon2);
                planet2.AddWeapon(weapon3);

                string result = planet2.DestructOpponent(planet1);

                Assert.That(result, Is.EqualTo($"{planet1.Name} is destructed!"));
            }

            [Test]
            public void IfWeaponPriceIsNegative()
            {
                ArgumentException exception = Assert.Throws<ArgumentException>(() => new Weapon("Weapon1", -1, 10));
                Assert.That(exception.Message, Is.EqualTo("Price cannot be negative."));
            }

            [Test]
            public void IsNuclear()
            {
                Weapon weapon1 = new Weapon("Weapon1", 10, 10);

                Assert.That(weapon1.IsNuclear, Is.EqualTo(true));
            }
        }
    }
}
