using System;
using ConsoleAssignment.Core;

namespace ConsoleAssignment
{
    class Program
    {
        static void Main()
        {
            UserInteraction.ShowMessage(String.Format("Loaded {0} command plugins\n\r", PluginRepository.LoadPlugins()));
            foreach (ICommandPlugin plugin in PluginRepository.Commands)
            {   
                IDescribable description = (IDescribable)plugin;
                UserInteraction.ShowMessage(String.Format($"{description.GetName()}\n\r{description.GetDescription()}\n\r-----------\n\r"));
            }

            string command = "";
            while (command != "exit")
            {
                command = UserInteraction.PromptResponse("command >").Trim();
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
                    UserInteraction.ShowMessage(String.Format("There is currently no plugin to handle the command {0}\n\r", command));
            }
        }

    }
}
