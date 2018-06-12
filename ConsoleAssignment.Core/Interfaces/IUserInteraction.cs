namespace ConsoleAssignment.Core
{
    public interface IUserInteraction
    {
        void ShowMessage(string message);
        string ReadMessage();
        string PromptResponse(string message);
    }
}
