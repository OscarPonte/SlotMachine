using SimplifiedSlotMachine.Enums;
using SimplifiedSlotMachine.Models;
using SimplifiedSlotMachine.Services.Interfaces;

namespace SimplifiedSlotMachine.Services
{
    public class MatchingSequenceChecker : IMatchingSequenceChecker
    {
        public IEnumerable<MatchingSequence> FindMatchingSequences(GameSettings gameSettings, SymbolSettings[,] grid)
        {
            try
            {
                var matchingSymbols = gameSettings.MatchingSymbols;

                List<MatchingSequence> matchingSequences = new List<MatchingSequence>();

                var rows = grid.GetLength(0);
                var columns = grid.GetLength(1);

                // Check for horizontal matching sequences
                if (gameSettings.HorizontalMatchingEnabled)
                {
                    matchingSequences.AddRange(GetOrthogonalMatchingSequence(MatchingSequenceType.Horizontal, grid, matchingSymbols, rows, columns));
                }

                // Check for vertical matching sequences
                if (gameSettings.VerticalMatchingEnabled)
                {
                    matchingSequences.AddRange(GetOrthogonalMatchingSequence(MatchingSequenceType.Vertical, grid, matchingSymbols, columns, rows));
                }

                // Check for diagonal matching sequences
                if (gameSettings.DiagonalMatchingEnabled)
                {
                    matchingSequences.AddRange(GetDiagonalMatchingSequence(grid, matchingSymbols));
                }

                return matchingSequences;
            }
            catch (Exception)
            {
                // Log and handle exception
                throw;
            }            
        }

        private List<MatchingSequence> GetOrthogonalMatchingSequence(MatchingSequenceType sequenceType, SymbolSettings[,] grid, int matchingSymbols, int rows, int length)
        {
            List<MatchingSequence> matchingSequences = new List<MatchingSequence>();

            for (int i = 0; i < rows; i++)
            {
                matchingSequences.AddRange(GetMatchingSequence(sequenceType, grid, matchingSymbols, i, length));
            }

            return matchingSequences;
        }

        private List<MatchingSequence> GetDiagonalMatchingSequence(SymbolSettings[,] grid, int matchingSymbols)
        {
            var rows = grid.GetLength(0);
            var columns = grid.GetLength(1);
            var maxDiagonalLength = Math.Min(rows, columns);

            List<MatchingSequence> matchingSequences = new List<MatchingSequence>();

            // Check for diagonal (\) matching sequences
            for (int i = 0; i <= rows; i++)
            {
                var lengthAdjustment = i > 0 && rows > columns ? rows - columns : 0;

                var currentDiagonalLength = maxDiagonalLength - (i - lengthAdjustment);

                if (currentDiagonalLength < matchingSymbols)
                {
                    break; // Not enough diagonal symbols to match.
                }

                matchingSequences.AddRange(GetMatchingSequence(MatchingSequenceType.DiagonalDownward, grid, matchingSymbols, i, currentDiagonalLength));
            }

            // Check for diagonal (/) matching sequences
            for (int i = rows - 1; i > 0; i--)
            {
                var currentDiagonalLength = i + 1 < maxDiagonalLength ? i + 1 : maxDiagonalLength;

                if (currentDiagonalLength < matchingSymbols)
                {
                    break; // Not enough diagonal symbols to match.
                }

                matchingSequences.AddRange(GetMatchingSequence(MatchingSequenceType.DiagonalUpward, grid, matchingSymbols, i, currentDiagonalLength));
            }

            return matchingSequences;
        }

        private List<MatchingSequence> GetMatchingSequence(MatchingSequenceType sequenceType, SymbolSettings[,] grid, int matchingSymbols, int position, int length)
        {
            List<MatchingSequence> matchingSequences = new List<MatchingSequence>();

            var matches = new List<SymbolSettings>() { GetSymbol(grid, sequenceType, position, 0) };

            for (int i = 1; i < length; i++)
            {
                var currentSymbol = GetSymbol(grid, sequenceType, position, i);

                if (currentSymbol.Symbol == SymbolType.Wildcard ||
                    matches.All(x => x.Symbol == currentSymbol.Symbol || x.Symbol == SymbolType.Wildcard))
                {
                    matches.Add(currentSymbol);
                }
                else
                {
                    break; // Break the loop as soon as a non-matching symbol is encountered.
                }
            }

            if (matches.Count >= matchingSymbols)
            {
                var sequence = matches.Select(x => new MatchingSequence { Symbol = x.Symbol, Coefficient = x.Coefficient });                
                matchingSequences.AddRange(sequence);
            }

            return matchingSequences;
        }

        private SymbolSettings GetSymbol(SymbolSettings[,] grid, MatchingSequenceType sequenceType, int position, int offset)
        {
            switch (sequenceType)
            {
                case MatchingSequenceType.Vertical:
                    return grid[offset, position];

                case MatchingSequenceType.Horizontal:
                    return grid[position, offset];

                case MatchingSequenceType.DiagonalDownward:
                    return grid[position + offset, offset];

                case MatchingSequenceType.DiagonalUpward:
                    return grid[position - offset, offset];

                default:
                    return null;
            }
        }
    }
}
