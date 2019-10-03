using System;

namespace Commands.NET
{
    /// <summary>
    /// Represents the result of a command's execution.
    /// </summary>
    public struct CommandResult : IEquatable<CommandResult>
    {
        /// <summary>
        /// If an exception occured during the treatment, it's stored here.
        /// </summary>
        public Exception Exception { get; }
        /// <summary>
        /// If the command ended normally, <c>true</c>.
        /// </summary>
        public bool IsSuccessful { get; }
        /// <summary>
        /// The return value of the command, if any.
        /// </summary>
        public object Result { get; }

        /// <summary>
        /// Used if the command threw an exception.
        /// </summary>
        /// <param name="exception">The exception that was thrown.</param>
        public CommandResult(Exception exception)
        {
            Exception = exception;
            IsSuccessful = false;
            Result = null;
        }

        /// <summary>
        /// Used if the command completed without errors.
        /// </summary>
        /// <param name="result">The return value of the command.</param>
        public CommandResult(object result)
        {
            Exception = null;
            IsSuccessful = true;
            Result = result;
        }

        /// <see cref="Object.Equals(object)"/>
        public bool Equals(CommandResult other) => Exception == other.Exception
                && IsSuccessful == other.IsSuccessful
                && Result == other.Result;
    }
}
