namespace SimplifiedSlotMachine.Models
{
    public class MatchingSequence
    {
        public SymbolSettings[] Symbols { get; set; } = Array.Empty<SymbolSettings>();
        public int StartRow { get; set; }
        public int StartColumn { get; set; }
    }
}
