using SimplifiedSlotMachine.Models;

namespace SimplifiedSlotMachine.Services.Interfaces
{
    public interface IGridService
    {
        SymbolSettings[,] GenerateNewGrid(GameSettings gameSettings);
    }
}
