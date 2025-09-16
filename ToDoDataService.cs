using System.Text.Json;

namespace SimpleTodo;

/// <summary>
/// Service for handling JSON file operations for ToDo items
/// </summary>
public class ToDoDataService
{
    private readonly string _dataFilePath;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Initializes a new instance of the ToDoDataService
    /// </summary>
    public ToDoDataService()
    {
        // Set the data file path to todos.json in the program directory
        _dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "todos.json");
        
        // Configure JSON serialization options
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true, // Pretty-print JSON for readability
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    /// <summary>
    /// Loads ToDo items from the JSON file
    /// </summary>
    /// <returns>List of ToDo items, empty list if file doesn't exist or on error</returns>
    public async Task<List<ToDoItem>> LoadTodosAsync()
    {
        try
        {
            // Check if the file exists
            if (!File.Exists(_dataFilePath))
            {
                // Return empty list if file doesn't exist (first run)
                return new List<ToDoItem>();
            }

            // Read the JSON content from file
            string jsonContent = await File.ReadAllTextAsync(_dataFilePath);
            
            // Handle empty file
            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                return new List<ToDoItem>();
            }

            // Deserialize JSON to list of ToDo items
            var todos = JsonSerializer.Deserialize<List<ToDoItem>>(jsonContent, _jsonOptions);
            return todos ?? new List<ToDoItem>();
        }
        catch (JsonException ex)
        {
            // Handle JSON parsing errors
            throw new InvalidOperationException($"Error parsing todos.json file: {ex.Message}", ex);
        }
        catch (IOException ex)
        {
            // Handle file I/O errors
            throw new InvalidOperationException($"Error reading todos.json file: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors
            throw new InvalidOperationException($"Unexpected error loading todos: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Saves ToDo items to the JSON file
    /// </summary>
    /// <param name="todos">List of ToDo items to save</param>
    public async Task SaveTodosAsync(List<ToDoItem> todos)
    {
        try
        {
            // Ensure todos list is not null
            todos ??= new List<ToDoItem>();

            // Serialize the list to JSON
            string jsonContent = JsonSerializer.Serialize(todos, _jsonOptions);

            // Ensure the directory exists
            string? directory = Path.GetDirectoryName(_dataFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Write JSON content to file
            await File.WriteAllTextAsync(_dataFilePath, jsonContent);
        }
        catch (JsonException ex)
        {
            // Handle JSON serialization errors
            throw new InvalidOperationException($"Error serializing todos to JSON: {ex.Message}", ex);
        }
        catch (IOException ex)
        {
            // Handle file I/O errors
            throw new InvalidOperationException($"Error writing to todos.json file: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors
            throw new InvalidOperationException($"Unexpected error saving todos: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the full path to the data file
    /// </summary>
    public string DataFilePath => _dataFilePath;

    /// <summary>
    /// Checks if the data file exists
    /// </summary>
    public bool DataFileExists => File.Exists(_dataFilePath);
}