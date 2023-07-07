using SimplifiedSlotMachine.Models;

namespace SimplifiedSlotMachine.Services.Interfaces
{
    public interface IGridService
    {
        /// <summary>
        /// Generates a new symbol grid based on the game settings.
        /// </summary>
        /// <param name="gameSettings">The game settings.</param>
        /// <returns>The generated symbol settings grid.</returns>
        SymbolSettings[,] GenerateNewGrid(GameSettings gameSettings);
    }
}
