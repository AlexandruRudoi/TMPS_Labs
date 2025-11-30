using System.Text.Json;

namespace Lab_3.Application.Readers;

/// <summary>
///     JSON configuration file reader
///     Handles loading and saving JSON configuration files
/// </summary>
public class JsonConfigurationReader
{
    private readonly string _dataPath;
    private readonly JsonSerializerOptions _jsonOptions;

    public JsonConfigurationReader()
    {
        _dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
            WriteIndented = true
        };
    }

    public JsonConfigurationReader(string customDataPath) : this()
    {
        _dataPath = customDataPath;
    }

    /// <summary>
    ///     Load configuration from JSON file
    /// </summary>
    public T Load<T>(string fileName) where T : class
    {
        var filePath = Path.Combine(_dataPath, fileName);
        
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Configuration file not found: {filePath}");
        }

        var jsonContent = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(jsonContent, _jsonOptions)
               ?? throw new InvalidOperationException($"Failed to deserialize {fileName}");
    }

    /// <summary>
    ///     Try to load configuration, returns null if file doesn't exist
    /// </summary>
    public T? TryLoad<T>(string fileName) where T : class
    {
        try
        {
            return Load<T>(fileName);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    ///     Save configuration to JSON file
    /// </summary>
    public void Save<T>(string fileName, T data) where T : class
    {
        var filePath = Path.Combine(_dataPath, fileName);
        var directory = Path.GetDirectoryName(filePath);
        
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var jsonContent = JsonSerializer.Serialize(data, _jsonOptions);
        File.WriteAllText(filePath, jsonContent);
    }

    /// <summary>
    ///     Check if configuration file exists
    /// </summary>
    public bool Exists(string fileName)
    {
        var filePath = Path.Combine(_dataPath, fileName);
        return File.Exists(filePath);
    }
}
