using System;
using ConsoleAssignment.Core;

namespace ConsoleAssignment
{
    class Program
    {
        static void Main()
        {
            PluginRepository.LoadPlugins();
            PluginRepository.DisplayLoadedPlugins();

            UserInteraction.InputLoop();
        }

    }
}
