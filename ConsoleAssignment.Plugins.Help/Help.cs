using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAssignment.Core;

namespace ConsoleAssignment.Plugins.Help
{
    public class Help : ICommandPlugin, IDescribable
    {
        public Help()
        {
        }

        public bool CanHandle(string command)
        {
            return command.ToLower() == "help";
        }

        public string GetDescription()
        {
            return "Usage: Help [CommandName]\n\rGives help text on the specified command.";
        }

        public string GetName()
        {
            return "Plugins.Help.dll";
        }

        public string[] Handle(string[] args)
        {
            List<string> returns = new List<string>();
            bool found = false;
            foreach (ICommandPlugin plugin in PluginRepository.Commands)
            {
                if ((args.Length > 0 && plugin.CanHandle(args[0])) || args.Length == 0)
                {
                    found = true;
                    IDescribable description = (IDescribable)plugin;
                    returns.Add(description.GetName() + "\n\r" + description.GetDescription());
                }
            }
            if (!found) returns.Add("The command " + args[0] + " is unrecongnised and therefore has no help text available");
            return returns.ToArray();
        }
    }
}
