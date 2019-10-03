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
        public readonly string CommandName;

        public CommandNotFoundException() : base() { }
        public CommandNotFoundException(string message) : base(message) { }
        public CommandNotFoundException(string message, string commandName) : base(message) { CommandName = commandName; }
        public CommandNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public CommandNotFoundException(string message, string commandName, Exception innerException) : base(message, innerException) { CommandName = commandName; }
        protected CommandNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
    }

    /// <summary>
    /// The exception that is thrown when a method doesn't have the
    /// right signature to be a command.
    /// </summary>
    [Serializable]
    public class InvalidSignatureException : Exception
    {
        public readonly string MethodName;

        public InvalidSignatureException() : base() { }
        public InvalidSignatureException(string message) : base(message) { }
        public InvalidSignatureException(string message, string methodName) : base(message) { MethodName = methodName; }
        public InvalidSignatureException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidSignatureException(string message, string methodName, Exception innerException) : base(message, innerException) { MethodName = methodName; }
        protected InvalidSignatureException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
    }
}
