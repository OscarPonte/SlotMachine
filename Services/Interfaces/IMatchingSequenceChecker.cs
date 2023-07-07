using SimplifiedSlotMachine.Models;

namespace SimplifiedSlotMachine.Services.Interfaces
{
    public interface IMatchingSequenceChecker
    {
        /// <summary>
        /// Finds the matching sequences on the symbol grid based on the game settings.
        /// </summary>
        /// <param name="gameSettings">The game settings.</param>
        /// <param name="grid">The symbol settings grid.</param>
        /// <returns>The matching sequences found on the grid.</returns>
        IEnumerable<MatchingSequence> FindMatchingSequences(GameSettings gameSettings, SymbolSettings[,] grid);
    }
}
