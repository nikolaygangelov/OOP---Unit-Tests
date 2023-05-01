using NUnit.Framework;
using System.Text;

namespace RobotFactory.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConstructor()
        {
            Factory factory = new Factory("Factory", 10);

            Assert.That(factory.Name, Is.EqualTo("Factory"));
            Assert.That(10, Is.EqualTo(factory.Capacity));
            Assert.That(0, Is.EqualTo(factory.Robots.Count));
            Assert.That(0, Is.EqualTo(factory.Supplements.Count));
        }

        [Test]
        public void IfRobotsAreLessThanCapacity()
        {
            Factory factory = new Factory("Factory", 10);
            Robot robot = new Robot("p1", 100, 10);

            var result = factory.ProduceRobot(robot.Model, robot.Price, robot.InterfaceStandard);

            Assert.That(result, Is.EqualTo($"Produced --> {robot}"));
        }

        [Test]
        public void IfRobotsAreEqualToThanCapacity()
        {
            Factory factory = new Factory("Factory", 1);
            Robot robot = new Robot("p1", 100, 10);
            factory.ProduceRobot(robot.Model, robot.Price, robot.InterfaceStandard);
            
            var result = factory.ProduceRobot("p2", 101, 11);

            Assert.That(result, Is.EqualTo($"The factory is unable to produce more robots for this production day!"));
        }

        [Test]
        public void TestProduceSupplement()
        {
            Factory factory = new Factory("Factory", 1);
            
            var result = factory.ProduceSupplement("ddd", 10);            

            Assert.That(factory.Supplements.Count, Is.EqualTo(1));
            Assert.That(result, Is.EqualTo($"Supplement: {"ddd"} IS: {10}"));
            Assert.That(factory.Supplements[0].Name, Is.EqualTo("ddd"));
            Assert.That(factory.Supplements[0].InterfaceStandard, Is.EqualTo(10));
        }

        [Test]
        public void IfRobotSupplementsReturnsTrue()//ContainsSupplement
        {
            Factory factory = new Factory("Factory", 1);
            Robot robot = new Robot("p1", 100, 10);
            Supplement supplement = new Supplement("ddd", 10);
            
            var result = factory.UpgradeRobot(robot, supplement);

            Assert.That(result, Is.EqualTo(true));
            Assert.That(robot.Supplements.Count, Is.EqualTo(1));
        }

        [Test]
        public void IfRobotSupplementsContainsSupplement()
        {
            Factory factory = new Factory("Factory", 1);
            Robot robot = new Robot("p1", 100, 10);
            Supplement supplement = new Supplement("ddd", 10);
            factory.UpgradeRobot(robot, supplement);
            
            var result = factory.UpgradeRobot(robot, supplement);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IfRobotsStandardsDiffer()
        {
            Factory factory = new Factory("Factory", 1);
            Robot robot = new Robot("p1", 100, 10);
            Supplement supplement = new Supplement("ddd", 11);

            var result = factory.UpgradeRobot(robot, supplement);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        [TestCase(100)]
        [TestCase(101)]

        public void IfSellRobotReturnsRobot(double price)
        {
            Factory factory = new Factory("Factory", 10);
            Robot robot = new Robot("p1", 100, 10);

            factory.ProduceRobot(robot.Model, robot.Price, robot.InterfaceStandard);

            var result = factory.SellRobot(price);

            Assert.That(result.Model, Is.EqualTo("p1"));
            Assert.That(result.Price, Is.EqualTo(100));
            Assert.That(result.InterfaceStandard, Is.EqualTo(10));
            Assert.That(factory.Robots.Count, Is.EqualTo(1));
        }
    }
}