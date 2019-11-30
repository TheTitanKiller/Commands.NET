using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Commands.NET;

namespace ExampleProject
{
    static class ExampleProgram
    {
        static void Main(string[] args)
        {
            AsyncMain().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        //asynchronous main function, to call library methods
        static async Task AsyncMain()
        {
            //registering the commands, for later calls
            Tools.RegisterCommands<ExampleCommands>();
            //simple default user
            User user = new User("John Doe", "123456", false);

            while(true)
            {
                //prompts the user to enter a command
                Console.Write("> ");
                //simple parsing of the user's request
                string[] command = Console.ReadLine().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                //creating a new context
                LocalContext ctx = new LocalContext(command.Length > 1 ? command[1] : null, user, Console.OpenStandardOutput());
                //calling the requested command
                CommandResult result = await Tools.ExecuteCommand(ctx, command[0]);
                //displaying the error if any happend
                if (!result.IsSuccessful)
                    Console.WriteLine(result.Exception.Message);
            }
        }
    }
}
