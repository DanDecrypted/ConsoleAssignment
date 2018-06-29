using System;
using System.Reflection;

namespace ConsoleAssignment.Core
{
    public class UserInteraction
    {
        static IUserInteraction userInteraction = new ConsoleUserInteraction();
        public static void ShowMessage(string message)
        {
            userInteraction.ShowMessage(message);
        }
        
        public static void ShowMessages(string[] messages)
        {
            foreach (string message in messages)
            {
                ShowMessage(message + "\n\r");
            }
        }

        public static string PromptResponse(string message)
        {
            return userInteraction.PromptResponse(message);
        }

        public static string ReadMessage()
        {
            return userInteraction.ReadMessage();
        }

        public static void AssignInteractionType(Type t)
        {
            if (typeof(IUserInteraction).IsAssignableFrom(t))
            {
                ConstructorInfo ci = t.GetConstructor(new Type[] { });
                userInteraction = (IUserInteraction)ci.Invoke(new Object[] { });
            }
        }

        public static void InputLoop()
        {
            userInteraction.InputLoop();
        }
    }
}
