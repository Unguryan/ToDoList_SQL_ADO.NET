namespace TodoList.ADO_NET.Sql;

public sealed class FileQueryLoader : IQueryLoader
{
    private readonly string _basePath;

    public FileQueryLoader(string basePath)
    {
        _basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
    }

    public string Load(string relativePath)
    {
        var fullPath = Path.Combine(_basePath, relativePath.Replace('/', Path.DirectorySeparatorChar));
        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"SQL file not found: {fullPath}", fullPath);
        return File.ReadAllText(fullPath);
    }
}
