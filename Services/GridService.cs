using SimplifiedSlotMachine.Enums;
using SimplifiedSlotMachine.Models;
using SimplifiedSlotMachine.Services.Interfaces;

namespace SimplifiedSlotMachine.Services
{
    public class GridService : IGridService
    {
        private readonly Random _random;

        public GridService()
        {
            _random = new Random();
        }

        public SymbolSettings[,] GenerateNewGrid(GameSettings gameSettings)
        {
            try
            {
                var rows = gameSettings.Rows;
                var columns = gameSettings.Columns;

                SymbolSettings[,] symbols = new SymbolSettings[rows, columns];

                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < columns; j++)
                    {
                        symbols[i, j] = GetRandomSymbol(gameSettings.SupportedSymbols);
                    }
                }

                return symbols;
            }
            catch (Exception)
            {
                // Log and handle exception
                throw;
            }
    
        }

        private SymbolSettings GetRandomSymbol(List<SymbolSettings> symbolSettings)
        {
            double totalProbability = symbolSettings.Sum(sp => sp.Probability);
            double randomValue = _random.NextDouble() * totalProbability;

            foreach (var symbol in symbolSettings)
            {
                if (randomValue < symbol.Probability)
                {
                    return symbol;
                }

                randomValue -= symbol.Probability;
            }

            // Fallback to Wildcard if no symbol matched (should not happen if probabilities are correct)
            return symbolSettings.FirstOrDefault(x => x.Symbol == SymbolType.Wildcard);
        }
    }
}
