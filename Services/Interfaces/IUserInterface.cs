using SimplifiedSlotMachine.Models;

namespace SimplifiedSlotMachine.Services.Interfaces
{
    public interface IUserInterface
    {
        decimal GetStakeAmount(decimal balance);
        void DisplayGrid(SymbolSettings[,] grid);
        void DisplayBalance(decimal balance);
        void DisplayMessage(string message);
    }
}
