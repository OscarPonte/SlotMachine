using NUnit.Framework;
using SimplifiedSlotMachine.Enums;
using SimplifiedSlotMachine.Models;
using SimplifiedSlotMachine.Services.Interfaces;
using SimplifiedSlotMachine.Services;

[TestFixture]
public class MatchingSequenceCheckerTests
{
    private IMatchingSequenceChecker _matchingSequenceChecker;

    [SetUp]
    public void Setup()
    {
        _matchingSequenceChecker = new MatchingSequenceChecker();
    }

    [Test]
    public void FindMatchingSequences_HorizontalMatchingEnabled_ReturnsMatchingSequences()
    {
        // Arrange
        var gameSettings = new GameSettings
        {
            HorizontalMatchingEnabled = true,
            VerticalMatchingEnabled = false,
            DiagonalMatchingEnabled = false,
            MatchingSymbols = 3,
            Rows = 4,
            Columns = 3
        };

        var appleSymbol = new SymbolSettings { Symbol = SymbolType.Apple, Coefficient = 0.4m };
        var bananaSymbol = new SymbolSettings { Symbol = SymbolType.Banana, Coefficient = 0.4m };
        var pineappleSymbol = new SymbolSettings { Symbol = SymbolType.Pineapple, Coefficient = 0.4m };
        var wildcardSymbol = new SymbolSettings { Symbol = SymbolType.Wildcard, Coefficient = 0.4m };

        var grid = new SymbolSettings[,]
        {
            { appleSymbol, appleSymbol, appleSymbol },
            { pineappleSymbol, bananaSymbol, appleSymbol },
            { wildcardSymbol, pineappleSymbol, bananaSymbol },
            { appleSymbol, appleSymbol, appleSymbol }
        };

        // Act
        var matchingSequences = _matchingSequenceChecker.FindMatchingSequences(gameSettings, grid);

        // Assert
        Assert.AreEqual(6, matchingSequences.Count());
        // Additional assertions for the matching sequences
    }

    [Test]
    public void FindMatchingSequences_VerticalMatchingEnabled_ReturnsMatchingSequences()
    {
        // Arrange
        var gameSettings = new GameSettings
        {
            HorizontalMatchingEnabled = false,
            VerticalMatchingEnabled = true,
            DiagonalMatchingEnabled = false,
            MatchingSymbols = 3,
            Rows = 4,
            Columns = 3
        };

        var appleSymbol = new SymbolSettings { Symbol = SymbolType.Apple, Coefficient = 0.4m };
        var bananaSymbol = new SymbolSettings { Symbol = SymbolType.Banana, Coefficient = 0.4m };
        var pineappleSymbol = new SymbolSettings { Symbol = SymbolType.Pineapple, Coefficient = 0.4m };
        var wildcardSymbol = new SymbolSettings { Symbol = SymbolType.Wildcard, Coefficient = 0.4m };

        var grid = new SymbolSettings[,]
        {
            { appleSymbol, appleSymbol, appleSymbol },
            { appleSymbol, wildcardSymbol, appleSymbol },
            { appleSymbol, wildcardSymbol, bananaSymbol },
            { pineappleSymbol, appleSymbol, appleSymbol }
        };

        // Act
        var matchingSequences = _matchingSequenceChecker.FindMatchingSequences(gameSettings, grid);

        // Assert
        Assert.AreEqual(7, matchingSequences.Count());
        // Additional assertions for the matching sequences
    }

    [Test]
    // Pending bug fix. if the matrix is 3x4 the diagonal check skips a potential matching sequence.
    public void FindMatchingSequences_DiagonalMatchingEnabled_ReturnsMatchingSequences()
    {
        // Arrange
        var gameSettings = new GameSettings
        {
            HorizontalMatchingEnabled = false,
            VerticalMatchingEnabled = false,
            DiagonalMatchingEnabled = true,
            MatchingSymbols = 3,
            Rows = 4,
            Columns = 3
        };

        var appleSymbol = new SymbolSettings { Symbol = SymbolType.Apple, Coefficient = 0.4m };
        var bananaSymbol = new SymbolSettings { Symbol = SymbolType.Banana, Coefficient = 0.4m };
        var pineappleSymbol = new SymbolSettings { Symbol = SymbolType.Pineapple, Coefficient = 0.4m };
        var wildcardSymbol = new SymbolSettings { Symbol = SymbolType.Wildcard, Coefficient = 0.4m };

        var grid = new SymbolSettings[,]
        {
            { appleSymbol, bananaSymbol, appleSymbol },
            { wildcardSymbol, wildcardSymbol, appleSymbol },
            { pineappleSymbol, wildcardSymbol, appleSymbol },
            { appleSymbol, appleSymbol, appleSymbol }
        };

        // Act
        var matchingSequences = _matchingSequenceChecker.FindMatchingSequences(gameSettings, grid);

        // Assert
        Assert.AreEqual(9, matchingSequences.Count());
        // Additional assertions for the matching sequences
    }

    [Test]
    public void FindMatchingSequences_AllMatchingEnabled_ReturnsMatchingSequences()
    {
        // Arrange
        var gameSettings = new GameSettings
        {
            HorizontalMatchingEnabled = true,
            VerticalMatchingEnabled = true,
            DiagonalMatchingEnabled = true,
            MatchingSymbols = 3,
            Rows = 5,
            Columns = 5
        };

        var appleSymbol = new SymbolSettings { Symbol = SymbolType.Apple, Coefficient = 0.4m };
        var bananaSymbol = new SymbolSettings { Symbol = SymbolType.Banana, Coefficient = 0.4m };
        var pineappleSymbol = new SymbolSettings { Symbol = SymbolType.Pineapple, Coefficient = 0.4m };
        var wildcardSymbol = new SymbolSettings { Symbol = SymbolType.Wildcard, Coefficient = 0.4m };

        var grid = new SymbolSettings[,]
        {
            { wildcardSymbol, appleSymbol, appleSymbol, appleSymbol, appleSymbol },
            { appleSymbol, appleSymbol, bananaSymbol, appleSymbol, pineappleSymbol },
            { appleSymbol, pineappleSymbol, appleSymbol, pineappleSymbol, pineappleSymbol },
            { appleSymbol, appleSymbol, pineappleSymbol, appleSymbol, pineappleSymbol },
            { appleSymbol, pineappleSymbol, pineappleSymbol, pineappleSymbol, appleSymbol }
        };

        // Act
        var matchingSequences = _matchingSequenceChecker.FindMatchingSequences(gameSettings, grid);

        // Assert
        Assert.AreEqual(23, matchingSequences.Count());
        // Additional assertions for the matching sequences
    }
}
