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

    public static void CreateFile(string path, List<string> lines)
    {
        using var streamWriter = new StreamWriter(path);
        foreach (var line in lines)
        {
            streamWriter.WriteLine(line);
        }
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
