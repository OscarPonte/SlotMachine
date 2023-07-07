using SimplifiedSlotMachine.Enums;
using SimplifiedSlotMachine.Models;
using SimplifiedSlotMachine.Repositories.Interfaces;

namespace SimplifiedSlotMachine.Repositories
{
    public class GameRepository : IGameRepository
    {
        public GameSettings GetGameSettings()
        {
            // Here, I would implement the logic to fetch the game settings from a data source.
            // For demonstration purposes, I am returning hardcoded settings.
            return new GameSettings
            {
                Rows = 4,
                Columns = 3,
                MatchingSymbols = 3,
                HorizontalMatchingEnabled = true,
                VerticalMatchingEnabled = false, // These are just for fun. feel free to enable them and enjoy greater winnings :)
                DiagonalMatchingEnabled = false,
                SupportedSymbols = new List<SymbolSettings> 
                {
                    new SymbolSettings { Symbol = SymbolType.Apple, SymbolValue = 'A', Coefficient = 0.4m, Probability = 45 },             
                    new SymbolSettings { Symbol = SymbolType.Banana, SymbolValue = 'B', Coefficient = 0.6m, Probability = 35 },             
                    new SymbolSettings { Symbol = SymbolType.Pineapple, SymbolValue = 'P', Coefficient = 0.8m, Probability = 15 },             
                    new SymbolSettings { Symbol = SymbolType.Wildcard, SymbolValue = '*', Coefficient = 0m, Probability = 5 },
                }
            };
        }
    }
}
