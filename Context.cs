using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Commands.NET
{
    /// <summary>
    /// Represents the context in which a given command is executed.
    /// </summary>
    public abstract class Context
    {
        /// <summary>
        /// The command's arguments values, separated by a space.
        /// </summary>
        public string CommandArgs { get; }
        /// <summary>
        /// The user who called the command.
        /// </summary>
        public User User { get; }

        /// <summary>
        /// Creates a Context used to call a command.
        /// </summary>
        /// <param name="args">The command's arguments values, separated by a space.</param>
        /// <param name="user">The user who called the command.</param>
        protected Context(string args, User user)
        {
            CommandArgs = args ?? String.Empty;
            User = user;
        }
    }

    /// <summary>
    /// Represents a context in an application used on a network.
    /// </summary>
    public sealed class NetworkContext : Context
    {
        /// <summary>
        /// The network socket from which the command was received.
        /// </summary>
        public Socket Connection { get; }

        /// <summary>
        /// Creates a Context used to call a command.
        /// </summary>
        /// <param name="args">The command's arguments values, separated by a space.</param>
        /// <param name="user">The user who called the command.</param>
        /// <param name="connection">The network socket from which the command was received.</param>
        public NetworkContext(string args, User user, Socket connection) : base(args, user) => Connection = connection;

        /// <summary>
        /// Sends a message to the user using the current connection socket.
        /// </summary>
        /// <param name="content">The message to send.</param>
        /// <returns>The Task currently running.</returns>
        public Task SendAsync(string content) => Connection.SendAsync(new ArraySegment<byte>(content.ToByteArray()), SocketFlags.None);
    }

    /// <summary>
    /// Represents a context in a local application.
    /// </summary>
    public sealed class LocalContext : Context
    {
        /// <summary>
        /// The stream used to answer the user.
        /// </summary>
        public Stream Answer { get; }

        /// <summary>
        /// Creates a Context used to call a command.
        /// </summary>
        /// <param name="args">The command's arguments values, separated by a space.</param>
        /// <param name="user">The user who called the command.</param>
        /// <param name="answer">The stream used to answer the user.</param>
        public LocalContext(string args, User user, Stream answer) : base(args, user) => Answer = answer;

        /// <summary>
        /// Sends a message to the user using the current stream.
        /// </summary>
        /// <param name="content">The message to send.</param>
        /// <returns>The Task currently running.</returns>
        public Task AnswerAsync(string content) => Answer.WriteAsync(content.ToByteArray(), 0, content.Length);
    }
}
