using SimplifiedSlotMachine.Models;

namespace SimplifiedSlotMachine.Repositories.Interfaces
{
    public interface IGameRepository
    {
        /// <summary>
        /// Gets the game settings from the repository.
        /// </summary>
        /// <returns>The game settings.</returns>
        GameSettings GetGameSettings();
    }
}
