using Spectre.Console;

namespace SimpleTodo;

internal class Program
{
    // Static instances for data service and runtime todo storage
    private static readonly ToDoDataService _dataService = new();
    private static List<ToDoItem> _todos = new();

    static async Task Main(string[] args)
    {
        AnsiConsole.MarkupLine("[bold blue]Simple ToDo Application[/]");
        AnsiConsole.WriteLine("Welcome to your personal task manager!");
        
        // Load existing todos on startup
        await LoadTodosOnStartup();
        
        AnsiConsole.WriteLine();
        
        // Main application loop
        bool continueRunning = true;
        while (continueRunning)
        {
            continueRunning = await ShowMainMenuAsync();
        }
        
        AnsiConsole.MarkupLine("[green]Thank you for using Simple ToDo![/]");
    }
    
    private static async Task LoadTodosOnStartup()
    {
        try
        {
            AnsiConsole.MarkupLine("[dim]Loading existing tasks...[/]");
            _todos = await _dataService.LoadTodosAsync();
            
            if (_todos.Count > 0)
            {
                AnsiConsole.MarkupLine($"[dim]Loaded {_todos.Count} existing task(s).[/]");
            }
            else if (_dataService.DataFileExists)
            {
                AnsiConsole.MarkupLine("[dim]No existing tasks found.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[dim]Starting with empty task list.[/]");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Warning: Could not load existing tasks: {ex.Message}[/]");
            AnsiConsole.MarkupLine("[dim]Starting with empty task list.[/]");
            _todos = new List<ToDoItem>();
        }
    }
    
    private static async Task<bool> ShowMainMenuAsync()
    {
        AnsiConsole.WriteLine();
        
        try
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]What would you like to do?[/]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "📋 View all tasks",
                        "➕ Add new task", 
                        "✏️ Update task status",
                        "🗑️ Delete task",
                        "🚪 Exit application"
                    }));
            
            return await HandleMenuChoiceAsync(choice);
        }
        catch (NotSupportedException)
        {
            // Handle case when running in non-interactive terminal
            AnsiConsole.MarkupLine("[yellow]Note: Interactive menus not available in this terminal mode.[/]");
            AnsiConsole.MarkupLine("[dim]Application will exit automatically.[/]");
            Thread.Sleep(2000);
            return false; // Exit the application
        }
    }
    
    private static async Task<bool> HandleMenuChoiceAsync(string choice)
    {
        switch (choice)
        {
            case "📋 View all tasks":
                await ViewAllTasksAsync();
                break;
            case "➕ Add new task":
                await AddNewTaskAsync();
                break;
            case "✏️ Update task status":
                await UpdateTaskStatusAsync();
                break;
            case "🗑️ Delete task":
                await DeleteTaskAsync();
                break;
            case "🚪 Exit application":
                return false; // Exit the loop
            default:
                AnsiConsole.MarkupLine("[red]Invalid selection. Please try again.[/]");
                break;
        }
        
        return true; // Continue the loop
    }
    
    private static async Task ViewAllTasksAsync()
    {
        AnsiConsole.MarkupLine("[blue]📋 View All Tasks[/]");
        AnsiConsole.WriteLine();
        
        if (_todos.Count == 0)
        {
            AnsiConsole.MarkupLine("[dim]No tasks found. Add some tasks to get started![/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[dim]Found {_todos.Count} task(s):[/]");
            AnsiConsole.WriteLine();
            
            // Display tasks in order (newest first - reverse iteration since Add appends to end)
            for (int i = _todos.Count - 1; i >= 0; i--)
            {
                var todo = _todos[i];
                var taskNumber = _todos.Count - i; // Number from 1 to count (newest = 1)
                
                // Format task display with state-based coloring
                string stateDisplay = GetStateDisplayText(todo.State);
                string taskLine = $"{taskNumber}. {stateDisplay} {EscapeMarkup(todo.Description)}";
                
                AnsiConsole.MarkupLine(taskLine);
            }
        }
        
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
        
        try
        {
            Console.ReadKey();
        }
        catch (InvalidOperationException)
        {
            Thread.Sleep(2000);
        }
    }
    
    private static string GetStateDisplayText(ToDoState state)
    {
        return state switch
        {
            ToDoState.New => "[white][[New]][/]",
            ToDoState.InProgress => "[yellow][[In Progress]][/]",
            ToDoState.Done => "[green]✅ [[Done]][/]",
            _ => "[dim][[Unknown]][/]"
        };
    }
    
    private static string EscapeMarkup(string text)
    {
        // Escape any markup characters in the task description to prevent formatting issues
        return text.Replace("[", "[[").Replace("]", "]]");
    }
    
    private static async Task AddNewTaskAsync()
    {
        AnsiConsole.MarkupLine("[green]➕ Add New Task[/]");
        AnsiConsole.WriteLine();
        
        try
        {
            // Prompt user for task description with validation
            string description = string.Empty;
            bool validInput = false;
            
            while (!validInput)
            {
                try
                {
                    description = AnsiConsole.Ask<string>(
                        "[yellow]Enter task description (max 255 characters):[/]");
                    
                    // Validate input
                    if (string.IsNullOrWhiteSpace(description))
                    {
                        AnsiConsole.MarkupLine("[red]Error: Task description cannot be empty. Please try again.[/]");
                        AnsiConsole.WriteLine();
                        continue;
                    }
                    
                    if (description.Length > 255)
                    {
                        AnsiConsole.MarkupLine($"[red]Error: Task description is too long ({description.Length} characters). Maximum allowed is 255 characters.[/]");
                        AnsiConsole.WriteLine();
                        continue;
                    }
                    
                    validInput = true;
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Error reading input: {ex.Message}[/]");
                    AnsiConsole.MarkupLine("[dim]Please try again...[/]");
                    AnsiConsole.WriteLine();
                }
            }
            
            // Create new todo item
            ToDoItem newTodo;
            try
            {
                newTodo = new ToDoItem(description, ToDoState.New);
            }
            catch (ArgumentException ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
                AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
                
                try
                {
                    Console.ReadKey();
                }
                catch (InvalidOperationException)
                {
                    Thread.Sleep(2000);
                }
                return;
            }
            
            // Add to in-memory list
            _todos.Add(newTodo);
            
            // Save to file
            AnsiConsole.MarkupLine("[dim]Saving task...[/]");
            await _dataService.SaveTodosAsync(_todos);
            
            // Success feedback
            AnsiConsole.MarkupLine("[green]✅ Task added successfully![/]");
            AnsiConsole.MarkupLine($"[dim]Task: \"{newTodo.Description}\"[/]");
            AnsiConsole.MarkupLine($"[dim]Status: {newTodo.State}[/]");
            AnsiConsole.MarkupLine($"[dim]Total tasks: {_todos.Count}[/]");
        }
        catch (InvalidOperationException ex)
        {
            AnsiConsole.MarkupLine($"[red]Error saving task: {ex.Message}[/]");
            AnsiConsole.MarkupLine("[yellow]The task was created but may not be saved to file.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Unexpected error: {ex.Message}[/]");
            AnsiConsole.MarkupLine("[yellow]Please try again.[/]");
        }
        
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
        
        try
        {
            Console.ReadKey();
        }
        catch (InvalidOperationException)
        {
            Thread.Sleep(2000);
        }
    }
    
    private static async Task UpdateTaskStatusAsync()
    {
        AnsiConsole.MarkupLine("[yellow]✏️ Update Task Status[/]");
        AnsiConsole.MarkupLine("[dim]This feature will be implemented soon...[/]");
        AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
        
        try
        {
            Console.ReadKey();
        }
        catch (InvalidOperationException)
        {
            Thread.Sleep(2000);
        }
    }
    
    private static async Task DeleteTaskAsync()
    {
        AnsiConsole.MarkupLine("[red]🗑️ Delete Task[/]");
        AnsiConsole.WriteLine();
        
        // Handle empty todo list case
        if (_todos.Count == 0)
        {
            AnsiConsole.MarkupLine("[dim]No tasks found. Add some tasks first to delete them![/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
            
            try
            {
                Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                Thread.Sleep(2000);
            }
            return;
        }
        
        try
        {
            // Display available tasks for deletion
            AnsiConsole.MarkupLine($"[dim]Found {_todos.Count} task(s) available for deletion:[/]");
            AnsiConsole.WriteLine();
            
            // Display tasks in order (newest first - same as ViewAllTasksAsync)
            for (int i = _todos.Count - 1; i >= 0; i--)
            {
                var todo = _todos[i];
                var taskNumber = _todos.Count - i; // Number from 1 to count (newest = 1)
                
                // Format task display with state-based coloring
                string stateDisplay = GetStateDisplayText(todo.State);
                string taskLine = $"{taskNumber}. {stateDisplay} {EscapeMarkup(todo.Description)}";
                
                AnsiConsole.MarkupLine(taskLine);
            }
            
            AnsiConsole.WriteLine();
            
            // Get user input for task selection
            bool validInput = false;
            
            while (!validInput)
            {
                try
                {
                    int selectedTaskNumber = AnsiConsole.Ask<int>(
                        $"[yellow]Enter the number of the task to delete (1-{_todos.Count}):[/]");
                    
                    // Validate input range
                    if (selectedTaskNumber < 1 || selectedTaskNumber > _todos.Count)
                    {
                        AnsiConsole.MarkupLine($"[red]Error: Please enter a number between 1 and {_todos.Count}.[/]");
                        AnsiConsole.WriteLine();
                        continue;
                    }
                    
                    // Convert task number to list index (reverse order)
                    int todoIndex = _todos.Count - selectedTaskNumber;
                    var todoToDelete = _todos[todoIndex];
                    
                    // Perform deletion
                    _todos.RemoveAt(todoIndex);
                    
                    // Save changes to file
                    AnsiConsole.MarkupLine("[dim]Saving changes...[/]");
                    await _dataService.SaveTodosAsync(_todos);
                    
                    // Success feedback
                    AnsiConsole.MarkupLine("[green]✅ Task deleted successfully![/]");
                    AnsiConsole.MarkupLine($"[dim]Deleted task: \"{todoToDelete.Description}\"[/]");
                    AnsiConsole.MarkupLine($"[dim]Status was: {todoToDelete.State}[/]");
                    AnsiConsole.MarkupLine($"[dim]Remaining tasks: {_todos.Count}[/]");
                    
                    validInput = true;
                }
                catch (FormatException)
                {
                    AnsiConsole.MarkupLine("[red]Error: Please enter a valid number.[/]");
                    AnsiConsole.WriteLine();
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Error reading input: {ex.Message}[/]");
                    AnsiConsole.MarkupLine("[dim]Please try again...[/]");
                    AnsiConsole.WriteLine();
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            AnsiConsole.MarkupLine($"[red]Error saving changes: {ex.Message}[/]");
            AnsiConsole.MarkupLine("[yellow]The task may have been removed from memory but not saved to file.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Unexpected error: {ex.Message}[/]");
            AnsiConsole.MarkupLine("[yellow]Please try again.[/]");
        }
        
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
        
        try
        {
            Console.ReadKey();
        }
        catch (InvalidOperationException)
        {
            Thread.Sleep(2000);
        }
    }
}
