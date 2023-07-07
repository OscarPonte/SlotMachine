using NUnit.Framework;
using Moq;
using SimplifiedSlotMachine.Models;
using SimplifiedSlotMachine.Repositories.Interfaces;
using SimplifiedSlotMachine.Services.Interfaces;
using SimplifiedSlotMachine.Services;
using SimplifiedSlotMachine.Enums;

namespace SimplifiedSlotMachine.Tests
{
    [TestFixture]
    public class GameEngineTests
    {
        private Mock<IGameRepository> _gameRepositoryMock;
        private Mock<IUserInterface> _userInterfaceMock;
        private Mock<IGridService> _gridServiceMock;
        private Mock<IMatchingSequenceChecker> _matchingSequenceCheckerMock;
        private GameEngine _gameEngine;

        [SetUp]
        public void Setup()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _userInterfaceMock = new Mock<IUserInterface>();
            _gridServiceMock = new Mock<IGridService>();
            _matchingSequenceCheckerMock = new Mock<IMatchingSequenceChecker>();

            _gameEngine = new GameEngine(_gameRepositoryMock.Object, _userInterfaceMock.Object, _gridServiceMock.Object, _matchingSequenceCheckerMock.Object);
        }

        [Test]
        public void RunGame_NegativeBalance_ReturnsImmediately()
        {
            // Arrange
            decimal balance = -10;

            // Act
            _gameEngine.RunGame(balance);

            // Assert
            // Ensure no interactions with dependencies occur
            _gameRepositoryMock.VerifyNoOtherCalls();
            _userInterfaceMock.VerifyNoOtherCalls();
            _gridServiceMock.VerifyNoOtherCalls();
            _matchingSequenceCheckerMock.VerifyNoOtherCalls();
        }

        [Test]
        public void RunGame_StakeAmountZero_DisplayThankYouMessage()
        {
            // Arrange
            decimal balance = 100;
            decimal stakeAmount = 0;
            _userInterfaceMock.SetupSequence(u => u.GetStakeAmount(balance))
                              .Returns(stakeAmount);
            var gameSettings = new GameSettings();
            _gameRepositoryMock.Setup(repo => repo.GetGameSettings())
                               .Returns(gameSettings);

            // Act
            _gameEngine.RunGame(balance);

            // Assert
            _gameRepositoryMock.Verify(repo => repo.GetGameSettings(), Times.Once);
            _userInterfaceMock.Verify(u => u.DisplayMessage("Thank you for playing. Goodbye!"), Times.Once);

            // Ensure no other interactions with dependencies occur
            _gameRepositoryMock.VerifyNoOtherCalls();
            _gridServiceMock.VerifyNoOtherCalls();
            _matchingSequenceCheckerMock.VerifyNoOtherCalls();
        }

        [Test]
        public void RunGame_InsufficientBalance_DisplayInsufficientBalanceMessage()
        {
            // Arrange
            decimal balance = 100;
            decimal stakeAmount = 150;
            _userInterfaceMock.SetupSequence(u => u.GetStakeAmount(balance))
                              .Returns(stakeAmount);
            _userInterfaceMock.Setup(u => u.DisplayMessage("Insufficient balance. Please enter a lower stake amount."));
            var gameSettings = new GameSettings();
            _gameRepositoryMock.Setup(repo => repo.GetGameSettings())
                               .Returns(gameSettings);

            // Act
            _gameEngine.RunGame(balance);

            // Assert
            _gameRepositoryMock.Verify(repo => repo.GetGameSettings(), Times.Once);
            _userInterfaceMock.Verify(u => u.DisplayMessage("Insufficient balance. Please enter a lower stake amount."), Times.Once);

            // Ensure no other interactions with dependencies occur
            _gameRepositoryMock.VerifyNoOtherCalls();
            _gridServiceMock.VerifyNoOtherCalls();
            _matchingSequenceCheckerMock.VerifyNoOtherCalls();
        }

        [Test]
        public void RunGame_ValidStakeAmount_DisplayGridAndWinMessage()
        {
            // Arrange
            var balance = 100m;
            var stakeAmount = 10m;
            var winAmount = 24m;
            var gameSettings = new GameSettings();
            var grid = new SymbolSettings[,]
            {
                { new SymbolSettings { Symbol = SymbolType.Apple, SymbolValue = 'A', Coefficient = 0.4m, Probability = 45 } },
                { new SymbolSettings { Symbol = SymbolType.Banana, SymbolValue = 'B', Coefficient = 0.6m, Probability = 35 } },
                { new SymbolSettings { Symbol = SymbolType.Pineapple, SymbolValue = 'P', Coefficient = 0.8m, Probability = 15 } },
                { new SymbolSettings { Symbol = SymbolType.Wildcard, SymbolValue = '*', Coefficient = 0m, Probability = 5 } } 
            };
            var matchingSequences = new List<MatchingSequence> 
            { 
                new MatchingSequence { Symbol = SymbolType.Pineapple, Coefficient = 0.8m },          
                new MatchingSequence { Symbol = SymbolType.Pineapple, Coefficient = 0.8m },          
                new MatchingSequence { Symbol = SymbolType.Pineapple, Coefficient = 0.8m }
            };
            _userInterfaceMock.SetupSequence(u => u.GetStakeAmount(balance))
                              .Returns(stakeAmount)
                              .Returns(0);
            _gameRepositoryMock.Setup(r => r.GetGameSettings()).Returns(gameSettings);
            _gridServiceMock.Setup(g => g.GenerateNewGrid(gameSettings)).Returns(grid).Verifiable();
            _matchingSequenceCheckerMock.Setup(m => m.FindMatchingSequences(gameSettings, grid)).Returns(matchingSequences);
            _userInterfaceMock.Setup(u => u.DisplayGrid(grid));
            _userInterfaceMock.Setup(u => u.DisplayMessage($"You have won: {winAmount}"));
            _userInterfaceMock.Setup(u => u.DisplayBalance(balance));

            // Act
            _gameEngine.RunGame(balance);

            // Assert
            _gameRepositoryMock.Verify(repo => repo.GetGameSettings(), Times.Once);
            _gridServiceMock.Verify(u => u.GenerateNewGrid(gameSettings), Times.Once);
            _matchingSequenceCheckerMock.Verify(u => u.FindMatchingSequences(gameSettings, grid), Times.Once);
            _userInterfaceMock.Verify(u => u.DisplayGrid(grid), Times.Once);
            _userInterfaceMock.Verify(u => u.DisplayBalance(balance), Times.AtLeastOnce);
            _userInterfaceMock.Verify(u => u.DisplayMessage($"You have won: {winAmount:N1}"), Times.Once);
            // Ensure no other interactions with dependencies occur
            _gameRepositoryMock.VerifyNoOtherCalls();
            _gridServiceMock.VerifyNoOtherCalls();
            _matchingSequenceCheckerMock.VerifyNoOtherCalls();
        }
    }
}
