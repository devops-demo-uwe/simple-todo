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
        
        if (_todos.Count == 0)
        {
            AnsiConsole.MarkupLine("[dim]No tasks found. Add some tasks to get started![/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[dim]Found {_todos.Count} task(s):[/]");
            // TODO: Display actual tasks here
            AnsiConsole.MarkupLine("[dim]Task display will be implemented soon...[/]");
        }
        
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
    
    private static async Task AddNewTaskAsync()
    {
        AnsiConsole.MarkupLine("[green]➕ Add New Task[/]");
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
}
