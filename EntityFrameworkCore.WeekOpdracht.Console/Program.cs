using EntityFrameworkCore.WeekOpdracht.Business;
using EntityFrameworkCore.WeekOpdracht.Business.Entities;
using EntityFrameworkCore.WeekOpdracht.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading;

namespace EntityFrameworkCore.WeekOpdracht.Console
{
    class Program
    {
        private static IUserService userService;
        private static IMessageService messageService;
        private static ILogger logger;

        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            userService = serviceProvider.GetService<IUserService>();
            messageService = serviceProvider.GetService<IMessageService>();
            logger = serviceProvider.GetService<ILogger>();

            System.Console.WriteLine("Deleting old stuff");
            DeleteAll();

            System.Console.WriteLine("Creating new user");
            var user = CreateUser();

            System.Console.WriteLine("Creating a message from the new user");
            CreateMessage(user);

            System.Console.WriteLine("Basic test succeeded.");
            System.Console.ReadKey();
        }

        private static void CreateMessage(User user)
        {
            logger.LogInformation("Create Message called");
            var message = messageService.Add(new Business.Entities.Message
            {
                Content = "Test bericht",
                SenderId = user.Id,
                Title = $"Nieuw test bericht van {user.Lastname}"
            });

            System.Console.WriteLine($"    Message with title {message.Title} created. New ID is {message.Id}");
            Thread.Sleep(2500);
        }

        private static User CreateUser()
        {
            logger.LogInformation("Create User called");
            var user = userService.Add(new Business.Entities.User
            {
                Email = "Test@test.nl",
                Lastname = "De Tester",
                Name = "Test"
            });

            System.Console.WriteLine($"    User with name {user.Name} created. New ID is {user.Id}");
            Thread.Sleep(2500);

            return user;
        }
        private static void DeleteAll()
        {
            logger.LogInformation("Delete all called");
            foreach (var item in userService.GetAll().ToList())
                userService.Delete(item.Id);

            foreach (var item in messageService.GetAll().ToList())
                messageService.Delete(item.Id);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ILogger, ConsoleLogger>();
        }
    }
}
