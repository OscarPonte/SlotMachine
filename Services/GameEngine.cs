using SimplifiedSlotMachine.Models;
using SimplifiedSlotMachine.Repositories.Interfaces;
using SimplifiedSlotMachine.Services.Interfaces;

namespace SimplifiedSlotMachine.Services
{
    public class GameEngine : IGameEngine
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUserInterface _userInterface;
        private readonly IGridService _gridService;
        private readonly IMatchingSequenceChecker _matchingSequenceChecker;

        public GameEngine(IGameRepository gameRepository, IUserInterface userInterface, IGridService gridService, IMatchingSequenceChecker matchingSequenceChecker)
        {
            _gameRepository = gameRepository;
            _userInterface = userInterface;
            _gridService = gridService;
            _matchingSequenceChecker = matchingSequenceChecker;
        }

        public void RunGame(decimal balance)
        {
            if (balance < 0)
            {
                return;
            }

            try
            {
                var gameSettings = _gameRepository.GetGameSettings();
                decimal currentBalance = balance;
                decimal stakeAmount;

                while (balance > 0)
                {
                    _userInterface.DisplayBalance(balance);
                    stakeAmount = _userInterface.GetStakeAmount(balance);

                    if (stakeAmount == 0)
                    {
                        _userInterface.DisplayMessage("Thank you for playing. Goodbye!");
                        break;
                    }

                    if (stakeAmount > currentBalance)
                    {
                        _userInterface.DisplayMessage("Insufficient balance. Please enter a lower stake amount.");
                        continue;
                    }

                    var grid = _gridService.GenerateNewGrid(gameSettings);

                    _userInterface.DisplayGrid(grid);

                    var winAmount = CalculateWinAmount(gameSettings, grid, stakeAmount);
                    currentBalance = UpdateBalance(currentBalance, stakeAmount, winAmount);

                    balance = (balance - stakeAmount) + winAmount;

                    if (winAmount > 0)
                    {
                        _userInterface.DisplayMessage($"You have won: {winAmount}");
                    }
                }
            }
            catch (Exception)
            {
                // Log and handle exception.
                throw;
            }    
        }

        private decimal CalculateWinAmount(GameSettings gameSettings, SymbolSettings[,] grid, decimal stakeAmount)
        {
            var matchingSequences = _matchingSequenceChecker.FindMatchingSequences(gameSettings, grid);

            var sequenceCoefficient = matchingSequences.Sum(x => x.Coefficient);
            var winAmount = sequenceCoefficient * stakeAmount;

            return winAmount;
        }       

        private decimal UpdateBalance(decimal currentBalance, decimal stakeAmount, decimal winAmount)
        {
            return (currentBalance - stakeAmount) + winAmount;
        }
    }
}