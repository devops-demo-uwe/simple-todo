using Spectre.Console;

namespace SimpleTodo;

internal class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("[bold blue]Simple ToDo Application[/]");
        AnsiConsole.WriteLine("Welcome to your personal task manager!");
        AnsiConsole.WriteLine();
        
        // Main application loop
        bool continueRunning = true;
        while (continueRunning)
        {
            continueRunning = ShowMainMenu();
        }
        
        AnsiConsole.MarkupLine("[green]Thank you for using Simple ToDo![/]");
    }
    
    private static bool ShowMainMenu()
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
            
            return HandleMenuChoice(choice);
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
    
    private static bool HandleMenuChoice(string choice)
    {
        switch (choice)
        {
            case "📋 View all tasks":
                ViewAllTasks();
                break;
            case "➕ Add new task":
                AddNewTask();
                break;
            case "✏️ Update task status":
                UpdateTaskStatus();
                break;
            case "🗑️ Delete task":
                DeleteTask();
                break;
            case "🚪 Exit application":
                return false; // Exit the loop
            default:
                AnsiConsole.MarkupLine("[red]Invalid selection. Please try again.[/]");
                break;
        }
        
        return true; // Continue the loop
    }
    
    private static void ViewAllTasks()
    {
        AnsiConsole.MarkupLine("[blue]📋 View All Tasks[/]");
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
    
    private static void AddNewTask()
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
    
    private static void UpdateTaskStatus()
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
    
    private static void DeleteTask()
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
