namespace SimplifiedSlotMachine.Models
{
    public class GameSettings
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int MatchingSymbols { get; set; }
        public bool HorizontalMatchingEnabled { get; set; }
        public bool VerticalMatchingEnabled { get; set; }
        public bool DiagonalMatchingEnabled { get; set; }
        public List<SymbolSettings> SupportedSymbols { get; set; } = new List<SymbolSettings>();
    }
}
