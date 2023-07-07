using SimplifiedSlotMachine.Models;
using SimplifiedSlotMachine.Services.Interfaces;

namespace SimplifiedSlotMachine.Services
{
    public class UserInterface : IUserInterface
    {
        public decimal GetStakeAmount(decimal balance)
        {
            Console.Write("Enter the amount to stake (or 0 to quit): ");
            decimal stakeAmount;

            if (!decimal.TryParse(Console.ReadLine(), out stakeAmount))
            {
                DisplayMessage("That is not a valid stake. Please enter a correct amount to stake.");
                return GetStakeAmount(balance);
            }

            if (stakeAmount == 0)
            {
                return 0;
            }

            if (stakeAmount > balance)
            {
                DisplayMessage("Insufficient balance. Please enter a lower stake amount.");
                return GetStakeAmount(balance);
            }

            return stakeAmount;
        }

        public void DisplayGrid(SymbolSettings[,] grid)
        {
            Console.WriteLine();

            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(grid[i, j].SymbolValue + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public void DisplayBalance(decimal balance)
        {
            Console.WriteLine($"Current Balance is: {balance}");
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
