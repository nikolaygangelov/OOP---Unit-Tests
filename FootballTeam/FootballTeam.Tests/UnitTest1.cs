using NUnit.Framework;
using System;

namespace FootballTeam.Tests
{
    public class Tests
    {

        [Test]
        public void IfConstructorOfFootballTeamWorksCorrectly()
        {
            FootballTeam team = new FootballTeam("Team", 21);

            Assert.AreEqual("Team", team.Name);
            Assert.That(team.Capacity, Is.EqualTo(21));
            Assert.That(team.Players.Count, Is.EqualTo(0));
        }

        [Test]
        [TestCase (null)]
        [TestCase ("")]
        public void IfTeamNameIsNullOrEmpty(string name)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new FootballTeam(name, 21));
            Assert.That(exception.Message, Is.EqualTo("Name cannot be null or empty!"));
        }

        [Test]
        public void IfCapacityIsLessThanFifteen()
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new FootballTeam("Team", 14));
            Assert.That(exception.Message, Is.EqualTo("Capacity min value = 15"));
        }

        [Test]
        public void IfCountIsGreatetThanCapacity()
        {
            FootballTeam team = new FootballTeam("Team", 15);

            for (int i = 1; i < 16; i++)
            {
                FootballPlayer player = new FootballPlayer($"{i.ToString()}", i, "Goalkeeper");
                team.AddNewPlayer(player);
            }
            FootballPlayer player16 = new FootballPlayer("Peter", 1, "Goalkeeper");
            
            var result = team.AddNewPlayer(player16);
            
            Assert.That(result, Is.EqualTo("No more positions available!"));
        }

        [Test]
        public void IfMethodAddPlayerWorksCorrectly()
        {
            FootballTeam team = new FootballTeam("Team", 15);

            FootballPlayer player1 = new FootballPlayer("Peter", 1, "Goalkeeper");

            var result = team.AddNewPlayer(player1);
            
            Assert.That(team.Players.Count, Is.EqualTo(1));
            Assert.That(result, Is.EqualTo($"Added player {player1.Name} in position {player1.Position} with number {player1.PlayerNumber}"));
        }

        [Test]
        public void IfMethodPickPlayerReturnsTheRightPlayer()
        {
            FootballTeam team = new FootballTeam("Team", 15);

            FootballPlayer player1 = new FootballPlayer("Peter", 1, "Goalkeeper");
            
            team.AddNewPlayer(player1);
            var result = team.PickPlayer("Peter");

            Assert.That(result.Name, Is.EqualTo("Peter"));
        }

        [Test]
        public void IfMethodPlayerScoreWorksCorrectly()
        {
            FootballTeam team = new FootballTeam("Team", 15);

            FootballPlayer player = new FootballPlayer("Peter", 11, "Forward");
            team.AddNewPlayer(player);

            var result = team.PlayerScore(11);

            Assert.That(result, Is.EqualTo($"{player.Name} scored and now has {player.ScoredGoals} for this season!"));
        }

        [Test]
        public void IfMethodPlayerScoreAddGoalsCorrectly()
        {
            FootballTeam team = new FootballTeam("Team", 15);
            FootballPlayer player = new FootballPlayer("Peter", 11, "Forward");
            team.AddNewPlayer(player);

            team.PlayerScore(11);

            Assert.That(player.ScoredGoals, Is.EqualTo(1));
        }
    }
}