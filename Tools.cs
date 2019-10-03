using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Commands.NET
{
    /// <summary>
    /// Static class containing useful functions.
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// The list of commands found by the <c>RegisterCommands</c> function.
        /// </summary>
        public static List<Command> CommandsList { get; private set; } = new List<Command>();
        /// <summary>
        /// The instance of the class holding the commands. Needed to invoke them.
        /// </summary>
        public static object CommandsInstance { get; private set; }
        /// <summary>
        /// The text encoding to use in all your application. Default at UTF8.
        /// </summary>
        public static Encoding TextEncoding { get; private set; } = Encoding.UTF8;

        ///<summary>Registers all commands defined in the class T.</summary>
        ///<typeparam name="T">The class where the commands are stored.</typeparam>
        ///<remarks>The class holding the commands cannot be static.
        ///Commands must have a return type of Task/ValueTask and cannot be static.
        ///The first parameter must be a <c>Context</c>.</remarks>
        public static void RegisterCommands<T>() where T : class
        {
            CommandsInstance = Activator.CreateInstance(typeof(T));
            MethodInfo[] methods = typeof(T).GetMethods();
            foreach (MethodInfo m in methods)
            {
                if (m.IsDefined(typeof(CommandAttribute)))
                    if (m.ReturnType.GetMethod(nameof(Task.GetAwaiter)) != null)
                        if (!m.IsStatic)
                            if (m.GetParameters()[0].ParameterType == typeof(Context))
                                if (m.IsDefined(typeof(AliasesAttribute)))
                                    CommandsList.Add(new Command(m.GetCustomAttribute<CommandAttribute>().Name, m, m.GetCustomAttribute<AliasesAttribute>().Aliases));
                                else
                                    CommandsList.Add(new Command(m.GetCustomAttribute<CommandAttribute>().Name, m, new List<string>()));
                            else throw new InvalidSignatureException("The first argument of a command must be a Context.", m.Name);
                        else throw new InvalidSignatureException("A static method cannot be a command.", m.Name);
                    else throw new InvalidSignatureException("The return type of the method must derive from Task or ValueTask to be a command.", m.Name);
            }
        }

        private static Command FindCommand(string name) => CommandsList.Find((Command c) => c.Name == name || c.Aliases.Contains(name)) ?? throw new CommandNotFoundException($"Command not found.", name);

        /// <summary>
        /// Executes the said command with the given context.
        /// </summary>
        /// <param name="context">The context of the command.</param>
        /// <param name="name">The name of the command.</param>
        /// <returns>The result value of the command, stored in a CommandResult struct.</returns>
        /// <exception cref="CommandNotFoundException"/>
        public static ValueTask<CommandResult> ExecuteCommand(Context context, string name) => FindCommand(name).Execute(context);

        /// <summary>
        /// Writes the error's message and stacktrace to the console.
        /// </summary>
        /// <param name="e">The error to display.</param>
        public static void PrintError(Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }

        /// <summary>
        /// Generates a log file containing the given error.
        /// </summary>
        /// <param name="e">The error to write.</param>
        public static void CrashLog(Exception e)
        {
            if (!Directory.Exists("logs"))
                Directory.CreateDirectory("logs");
            using StreamWriter file = new StreamWriter(File.Create($@"logs\log_{DateTime.UtcNow.ToString("ddMMyyyy_HHmmss")}.txt"));
            file.WriteLine(DateTime.Now.ToString() + '\n');
            file.WriteLine(e.Message);
            file.WriteLine(e.StackTrace);
        }

        /// <summary>
        /// Changes the text encoding of your application.
        /// </summary>
        /// <param name="e">The encoding to use.</param>
        public static void DefineTextEncoding(Encoding e) => TextEncoding = e;

        /// <summary>
        /// Extension method to convert a string to a byte array, with the defined <c>TextEncoding</c>.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>The byte array containing the conversion of the string.</returns>
        internal static byte[] ToByteArray(this string s) => TextEncoding.GetBytes(s);
    }
}
