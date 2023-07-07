using SimplifiedSlotMachine.Models;

namespace SimplifiedSlotMachine.Services.Interfaces
{
    public interface IUserInterface
    {
        /// <summary>
        /// Gets the stake amount from the user based on their balance.
        /// </summary>
        /// <param name="balance">The current balance of the user.</param>
        /// <returns>The stake amount entered by the user.</returns>
        decimal GetStakeAmount(decimal balance);

        /// <summary>
        /// Displays the grid of symbol settings to the user.
        /// </summary>
        /// <param name="grid">The symbol settings grid to display.</param>
        void DisplayGrid(SymbolSettings[,] grid);

        /// <summary>
        /// Displays the current balance to the user.
        /// </summary>
        /// <param name="balance">The current balance of the user.</param>
        void DisplayBalance(decimal balance);

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message to display.</param>
        void DisplayMessage(string message);
    }
}
