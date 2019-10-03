using System;
using System.Collections.Generic;

namespace Commands.NET
{
    /// <summary>
    /// Marks a method as a command for your application.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class CommandAttribute : Attribute
    {
        /// <summary>
        /// The name of the command.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Marks a method as a command for your application.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        public CommandAttribute(string name) => Name = name;
    }

    /// <summary>
    /// Adds aliases of the name of your command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class AliasesAttribute : Attribute
    {
        /// <summary>
        /// The aliases usable to call the command.
        /// </summary>
        public readonly IReadOnlyList<string> Aliases;

        /// <summary>
        /// Adds aliases of the name of your command.
        /// </summary>
        /// <param name="names">The aliases usable to call the command.</param>
        public AliasesAttribute(params string[] names) => Aliases = names;
    }
}