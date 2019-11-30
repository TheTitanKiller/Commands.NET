using System;
using System.Runtime.Serialization;

namespace Commands.NET
{
    /// <summary>
    /// The exception that is thrown when a given command is not found
    /// in the list of registered commands.
    /// </summary>
    [Serializable]
    public class CommandNotFoundException : Exception
    {
        /// <summary>
        /// The name of the command that caused the exception.
        /// </summary>
        public readonly string CommandName;

        /// <see cref="Exception()"/>
        public CommandNotFoundException() : base() { }
        /// <see cref="Exception(string)"/>
        public CommandNotFoundException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the Commands.NET.InvalidSignatureException class
        /// with a specified error message and the name of the command that caused the exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="commandName">The name of the command that caused the exception.</param>
        public CommandNotFoundException(string message, string commandName) : base(message) { CommandName = commandName; }
        /// <see cref="Exception(string, Exception)"/>
        public CommandNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// Initializes a new instance of the Commands.NET.InvalidSignatureException class
        /// with a specified error message, the name of the command that caused the exception
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="commandName">The name of the command that caused the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public CommandNotFoundException(string message, string commandName, Exception innerException) : base(message, innerException) { CommandName = commandName; }
        /// <see cref="Exception(SerializationInfo, StreamingContext)"/>
        protected CommandNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <see cref="GetObjectData(SerializationInfo, StreamingContext)"/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
    }

    /// <summary>
    /// The exception that is thrown when a method doesn't have the
    /// right signature to be a command.
    /// </summary>
    [Serializable]
    public class InvalidSignatureException : Exception
    {
        /// <summary>
        /// The name of the method that caused the exception.
        /// </summary>
        public readonly string MethodName;

        /// <see cref="Exception()"/>
        public InvalidSignatureException() : base() { }
        /// <see cref="Exception(string)"/>
        public InvalidSignatureException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the Commands.NET.InvalidSignatureException class
        /// with a specified error message and the name of the method that caused the exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="methodName">The name of the method that caused the exception.</param>
        public InvalidSignatureException(string message, string methodName) : base(message) { MethodName = methodName; }
        /// <see cref="Exception(string, Exception)"/>
        public InvalidSignatureException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// Initializes a new instance of the Commands.NET.InvalidSignatureException class
        /// with a specified error message, the name of the method that caused the exception
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="methodName">The name of the method that caused the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidSignatureException(string message, string methodName, Exception innerException) : base(message, innerException) { MethodName = methodName; }
        /// <see cref="Exception(SerializationInfo, StreamingContext)"/>
        protected InvalidSignatureException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <see cref="GetObjectData(SerializationInfo, StreamingContext)"/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
    }
}
