namespace SimplifiedSlotMachine.Services.Interfaces
{
    public interface IGameEngine
    {
        /// <summary>
        /// Runs the slot machine game with the specified balance.
        /// </summary>
        /// <param name="balance">The initial balance of the game.</param>
        void RunGame(decimal balance);
    }
}
