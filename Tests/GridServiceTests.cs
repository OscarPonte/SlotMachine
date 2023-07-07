using NUnit.Framework;
using SimplifiedSlotMachine.Enums;
using SimplifiedSlotMachine.Models;
using SimplifiedSlotMachine.Services;
using SimplifiedSlotMachine.Services.Interfaces;

namespace SimplifiedSlotMachine.Tests
{
    [TestFixture]
    public class GridServiceTests
    {
        private IGridService _gridService;

        [SetUp]
        public void Setup()
        {
            _gridService = new GridService();
        }

        [Test]
        public void GenerateNewGrid_ValidGameSettings_ReturnsGridWithCorrectSize()
        {
            // Arrange
            var gameSettings = new GameSettings
            {
                Rows = 4,
                Columns = 3,
                SupportedSymbols = new List<SymbolSettings>
                {
                    { new SymbolSettings { Symbol = SymbolType.Apple, SymbolValue = 'A', Coefficient = 0.4m, Probability = 45 } },
                    { new SymbolSettings { Symbol = SymbolType.Banana, SymbolValue = 'B', Coefficient = 0.6m, Probability = 35 } },
                    { new SymbolSettings { Symbol = SymbolType.Pineapple, SymbolValue = 'P', Coefficient = 0.8m, Probability = 15 } },
                    { new SymbolSettings { Symbol = SymbolType.Wildcard, SymbolValue = '*', Coefficient = 0m, Probability = 5 } }
                }
            };

            // Act
            var grid = _gridService.GenerateNewGrid(gameSettings);

            // Assert
            Assert.AreEqual(gameSettings.Rows, grid.GetLength(0));
            Assert.AreEqual(gameSettings.Columns, grid.GetLength(1));
        }

        [Test]
        public void GenerateNewGrid_ValidGameSettings_ReturnsGridWithSupportedSymbols()
        {
            // Arrange
            var gameSettings = new GameSettings
            {
                Rows = 3,
                Columns = 5,
                SupportedSymbols = new List<SymbolSettings>
                {
                    { new SymbolSettings { Symbol = SymbolType.Apple, SymbolValue = 'A', Coefficient = 0.4m, Probability = 45 } },
                    { new SymbolSettings { Symbol = SymbolType.Banana, SymbolValue = 'B', Coefficient = 0.6m, Probability = 35 } },
                    { new SymbolSettings { Symbol = SymbolType.Pineapple, SymbolValue = 'P', Coefficient = 0.8m, Probability = 15 } },
                    { new SymbolSettings { Symbol = SymbolType.Wildcard, SymbolValue = '*', Coefficient = 0m, Probability = 5 } }
                }
            };

            // Act
            var grid = _gridService.GenerateNewGrid(gameSettings);

            // Assert
            var symbols = grid.Cast<SymbolSettings>().ToList();
            foreach (var symbol in symbols)
            {
                Assert.IsTrue(gameSettings.SupportedSymbols.Contains(symbol));
            }
        }
    }
}
