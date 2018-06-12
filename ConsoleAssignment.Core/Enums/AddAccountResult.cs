namespace ConsoleAssignment.Core
{
    public enum AddAccountResult
    {
        Undefined = 0,
        Exists = 1, 
        Successful = 2, 
        InvalidEmail = 3, 
        InvalidEmailAndUserExists = 4
    }
}
