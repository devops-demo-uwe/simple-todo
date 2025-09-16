using Spectre.Console;

namespace SimpleTodo;

internal class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("[bold blue]Simple ToDo Application[/]");
        AnsiConsole.WriteLine("Welcome to your personal task manager!");
        AnsiConsole.WriteLine();
        
        // Breakpoint here for debugging demonstration
        var debugMessage = "Debug: Application started successfully";
        AnsiConsole.MarkupLine($"[dim]{debugMessage}[/]");
        
        // TODO: Add main application logic here
        AnsiConsole.MarkupLine("[dim]Press any key to exit...[/]");
        
        try
        {
            Console.ReadKey();
        }
        catch (InvalidOperationException)
        {
            // Handle case when running in non-interactive console (like VS Code internal console)
            AnsiConsole.MarkupLine("[yellow]Note: Interactive input not available in this console mode.[/]");
            AnsiConsole.MarkupLine("[dim]Application will exit automatically.[/]");
            
            // Add a small delay for readability
            Thread.Sleep(2000);
        }
    }
}
