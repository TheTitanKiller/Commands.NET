using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Commands.NET
{
    /// <summary>
    /// Represents a command.
    /// </summary>
    public class Command
    {
        /// <summary>
        /// The name of the command.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// The method this command encapsulates.
        /// </summary>
        public readonly MethodInfo Self;
        /// <summary>
        /// The aliases used to call the command.
        /// </summary>
        public readonly IReadOnlyList<string> Aliases;
        /// <summary>
        /// The arguments of the command.
        /// </summary>
        public readonly IReadOnlyList<CommandArgument> Arguments;
        /// <summary>
        /// <c>true</c> if the command's name has aliases, else <c>false</c>.
        /// </summary>
        public bool HasAliases { get { return Aliases.Count != 0; } }

        /// <summary>
        /// Creates a command usable in your application.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="self">The method this command encapsulates.</param>
        /// <param name="aliases">The aliases used to call the command.</param>
        internal Command(string name, MethodInfo self, IReadOnlyList<string> aliases)
        {
            Name = name;
            Self = self;
            Aliases = aliases;
            List<CommandArgument> tmp = new List<CommandArgument>();
            foreach (ParameterInfo param in self.GetParameters())
                tmp.Add(new CommandArgument(param.Name, param.ParameterType, param.IsOptional, param.IsDefined(typeof(ParamArrayAttribute)), param.DefaultValue));
            Arguments = tmp.AsReadOnly();
        }

        /// <summary>
        /// Executes the command on the given context.
        /// </summary>
        /// <param name="context">The context on which to execute the command.</param>
        /// <returns>The result of the command, stored in a CommandResult struct.</returns>
        /// <seealso cref="CommandResult"/>
        internal async ValueTask<CommandResult> Execute(Context context)
        {
            try
            {
                var tmp = (Task)Self.Invoke(Tools.CommandsInstance, CheckArgs(context));
                await tmp.ConfigureAwait(false);
                return new CommandResult(tmp.GetType().GetProperty("Result").GetValue(tmp));
            }
            catch (Exception e)
            {
                return new CommandResult(e);
            }
        }

        private object[] CheckArgs(Context context)
        {
            string[] s = context.CommandArgs.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<object> tmp = new List<object>() { context };
            int i = 1, j = 0;
            for (; i < Arguments.Count; i++, j++)
            {
                var arg = Arguments[i];
                if (arg.IsOptional)
                    if(j < s.Length)
                        tmp.Add((arg.ArgType == typeof(string)) ? s[j] : Convert.ChangeType(s[j], arg.ArgType));
                    else
                        tmp.Add(arg.DefaultValue);
                else if (arg.IsMultiple)
                {
                    while (j < s.Length)
                        tmp.Add((arg.ArgType == typeof(string)) ? s[j] : Convert.ChangeType(s[j++], arg.ArgType));
                    break;
                }
                else if(j < s.Length)
                    tmp.Add((arg.ArgType == typeof(string)) ? s[j] : Convert.ChangeType(s[j], arg.ArgType));
                else throw new ArgumentException("Missing arguments in command call.");
            }
            return tmp.ToArray();
        }
    }
}
