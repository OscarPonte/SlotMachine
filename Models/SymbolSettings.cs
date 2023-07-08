using SimplifiedSlotMachine.Enums;

namespace SimplifiedSlotMachine.Models
{
    public class SymbolSettings
    {
        public SymbolType Symbol { get; set; }
        public char SymbolValue { get; set; }
        public decimal Coefficient { get; set; }
        public double Probability { get; set; }
    }
}
