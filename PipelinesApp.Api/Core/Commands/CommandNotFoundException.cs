namespace PipelinesApp.Api.Core.Commands
{
    using System;

    internal class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(Type type)
        {
        }
    }
}