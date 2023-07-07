using SimplifiedSlotMachine.Models;

namespace SimplifiedSlotMachine.Services.Interfaces
{
    public interface IMatchingSequenceChecker
    {
        IEnumerable<MatchingSequence> FindMatchingSequences(GameSettings gameSettings, SymbolSettings[,] grid);
    }
}
