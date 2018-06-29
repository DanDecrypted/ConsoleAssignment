using System;

namespace ConsoleAssignment.Core
{
    class ConsoleUserInteraction : IUserInteraction
    {
        public string PromptResponse(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public string ReadMessage()
        {
            return Console.ReadLine();
        }

        public void ShowMessage(string message)
        {
            Console.Write(message);
        }

        public void InputLoop()
        {
            string command = "";
            while (command != "exit")
            {
                command = PromptResponse("command >").Trim();
                SplicedContainer commandSplicedFromArgs = Utils.SplicedContainerForIndexOfAray(command.Split(' '), 0);

                string[] arguments = commandSplicedFromArgs.RemainingArray;
                command = commandSplicedFromArgs.Spliced;

                bool found = false;
                foreach (ICommandPlugin plugin in PluginRepository.Commands)
                {
                    if (plugin.CanHandle(command))
                    {
                        string[] results = plugin.Handle(arguments);
                        UserInteraction.ShowMessages(results);
                        found = true;
                        break;
                    }
                }
                if (!found)
                    ShowMessage($"There is currently no plugin to handle the command {command}\n\r");
            }
        }
    }
}
