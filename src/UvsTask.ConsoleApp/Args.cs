namespace UvsTask.ConsoleApp;

internal sealed class Args
{
    private readonly string[] _args;

    private Args(string[] args) => _args = args;

    public static Args Parse(string[] args) => new(args);

    public int RequireInt(string key)
    {
        var value = RequireString(key);
        return int.TryParse(value, out var parsed)
            ? parsed
            : throw new ArgumentException($"{key} must be INT");
    }

    public string RequireString(string key)
    {
        var index = Array.IndexOf(_args, key);
        if (index < 0 || index == _args.Length - 1)
            throw new ArgumentException($"{key} is required");

        return _args[index + 1];
    }
}