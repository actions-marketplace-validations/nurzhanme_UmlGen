namespace UmlGen.Infrastructure;

public static class IoHelper
{
    public static string CreateDirectory(string[] pathList)
    {
        var path = Path.Combine(pathList);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        return path;
    }

    public static string ReadFile(string path)
    {
        using var streamReader = new StreamReader(path);

        return streamReader.ReadToEnd();
    }

    public static string CreateFile(string[] pathList, string data)
    {
        var path = Path.Combine(pathList);
        

        using var streamWriter = new StreamWriter($"{path}.md");
        streamWriter.Write(data);

        return path;
    }

    
    public static string GetOnlyCurrentDirectory()
    {
        return Directory.GetCurrentDirectory().Substring(Directory.GetCurrentDirectory().LastIndexOf("\\") + 1);
    }

    private static string RemoveLastItemInPath(string path)
    {
        var index = path.LastIndexOf("\\", StringComparison.Ordinal);
        return path.Substring(0, index);
    }
}
