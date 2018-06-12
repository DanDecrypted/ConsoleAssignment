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
    }
}
