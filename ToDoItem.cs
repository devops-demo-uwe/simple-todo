namespace SimpleTodo;

/// <summary>
/// Represents the state of a ToDo item
/// </summary>
public enum ToDoState
{
    New,
    InProgress,
    Done
}

/// <summary>
/// Represents a ToDo item with description and state
/// </summary>
public class ToDoItem
{
    private string _description = string.Empty;
    
    /// <summary>
    /// Gets or sets the description of the ToDo item (max 255 characters)
    /// </summary>
    public string Description 
    { 
        get => _description;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Description cannot be empty or whitespace.", nameof(value));
            
            if (value.Length > 255)
                throw new ArgumentException("Description cannot exceed 255 characters.", nameof(value));
                
            _description = value.Trim();
        }
    }
    
    /// <summary>
    /// Gets or sets the current state of the ToDo item
    /// </summary>
    public ToDoState State { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the ToDoItem class
    /// </summary>
    /// <param name="description">The description of the task</param>
    /// <param name="state">The initial state (defaults to New)</param>
    public ToDoItem(string description, ToDoState state = ToDoState.New)
    {
        Description = description; // Uses the setter for validation
        State = state;
    }
    
    /// <summary>
    /// Parameterless constructor for JSON deserialization
    /// </summary>
    public ToDoItem()
    {
        State = ToDoState.New;
    }
}