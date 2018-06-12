namespace ConsoleAssignment.Core
{
    public interface ISerialiser
    {
        string[] Serialise<T>(T t, string path);
        T Deserialise<T>(string path);
    }
}
