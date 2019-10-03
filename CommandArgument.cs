using System;

namespace Commands.NET
{
    /// <summary>
    /// Represents a command's argument.
    /// </summary>
    public sealed class CommandArgument
    {
        /// <summary>
        /// The name of the argument.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// The type of the argument.
        /// </summary>
        public readonly Type ArgType;
        /// <summary>
        /// <c>true</c> if the argument has a default value.
        /// </summary>
        public readonly bool IsOptional;
        /// <summary>
        /// <c>true</c> if the argument uses the <c>params</c> keyword.
        /// </summary>
        public readonly bool IsMultiple;
        /// <summary>
        /// The default value of the argument.
        /// </summary>
        public readonly object DefaultValue;

        internal CommandArgument(string name, Type type, bool optional, bool multiple, object defaultValue)
        {
            Name = name;
            ArgType = type;
            IsOptional = optional;
            IsMultiple = multiple;
            DefaultValue = defaultValue;
        }
    }
}
