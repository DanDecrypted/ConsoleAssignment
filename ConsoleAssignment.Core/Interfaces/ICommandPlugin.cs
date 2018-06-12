namespace ConsoleAssignment.Core
{
    public interface ICommandPlugin
    {
        bool CanHandle(string command);
        string[] Handle(string[] args);
    }
}
