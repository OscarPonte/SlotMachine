using Microsoft.Extensions.DependencyInjection;
using SimplifiedSlotMachine.Repositories;
using SimplifiedSlotMachine.Repositories.Interfaces;
using SimplifiedSlotMachine.Services;
using SimplifiedSlotMachine.Services.Interfaces;

var balance = 0m;

var serviceProvider = ConfigureServices();
var gameEngine = serviceProvider.GetRequiredService<IGameEngine>();

Console.Write("Enter the deposit amount: ");
decimal.TryParse(Console.ReadLine(), out balance);

gameEngine.RunGame(balance);

Console.WriteLine("Game Over. You have run out of balance.");

static IServiceProvider ConfigureServices()
{
    var services = new ServiceCollection();

    // Register your services and dependencies here
    services.AddSingleton<IGameRepository, GameRepository>();
    services.AddSingleton<IGridService, GridService>();
    services.AddSingleton<IGameEngine, GameEngine>();
    services.AddSingleton<IUserInterface, UserInterface>();
    services.AddSingleton<IMatchingSequenceChecker, MatchingSequenceChecker>();

    return services.BuildServiceProvider();
}
