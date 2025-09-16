# Simple ToDo Application

A feature-complete console-based ToDo application built with .NET 9, demonstrating modern C# development practices and AI-assisted development workflows.

## 🎯 Overview

This application provides a clean, intuitive interface for managing personal tasks through a rich console experience. Built as a demonstration of professional software development practices, it showcases proper architecture, error handling, and user experience design in a console application.

## ✨ Features

### Core Functionality
- ✅ **Create Tasks** - Add new tasks with description validation (max 255 characters)
- 📋 **View All Tasks** - Display tasks with visual state indicators and organized layout
- ✏️ **Update Task Status** - Interactive status management with three states:
  - 🆕 **New** - Newly created tasks
  - ⏳ **In Progress** - Tasks currently being worked on  
  - ✅ **Done** - Completed tasks
- 🗑️ **Delete Tasks** - Remove tasks permanently with clear confirmation

### User Experience
- 🎨 **Rich Console Interface** - Colorful, interactive menus using Spectre.Console
- 🚫 **Graceful Cancellation** - Cancel operations at any point
- ⚠️ **Smart Validation** - Prevents invalid inputs and redundant changes
- 🖥️ **Terminal Compatibility** - Works in both interactive and non-interactive environments
- 💾 **Auto-Save** - All changes are automatically persisted to file

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 or later
- Windows, macOS, or Linux

### Installation & Running
1. **Clone the repository**
   ```bash
   git clone https://github.com/devops-demo-uwe/simple-todo.git
   cd simple-todo
   ```

2. **Build the application**
   ```bash
   dotnet build
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

### Quick Start Guide
1. Launch the application
2. Use arrow keys to navigate the main menu
3. Select "➕ Add new task" to create your first task
4. Use "📋 View all tasks" to see your task list
5. Use "✏️ Update task status" to change task states
6. Use "🗑️ Delete task" to remove completed tasks

## 🏗️ Technical Architecture

### Project Structure
```
SimpleTodo/
├── Program.cs              # Main application and UI logic
├── ToDoItem.cs            # Data model and validation
├── ToDoDataService.cs     # JSON persistence layer
├── SimpleTodo.csproj      # Project configuration
├── README.md              # Project documentation
├── REVIEW.md              # Comprehensive code review
└── todos.json             # Data storage (created at runtime)
```

### Technology Stack
- **Framework**: .NET 9.0
- **UI Library**: Spectre.Console 0.51.1
- **Serialization**: System.Text.Json (built-in)
- **Storage**: JSON file-based persistence
- **Language Features**: C# 13 with nullable reference types

### Key Design Patterns
- **Separation of Concerns** - Clear distinction between UI, business logic, and data access
- **Async/Await** - Non-blocking I/O operations for file handling
- **Defensive Programming** - Comprehensive input validation and error handling
- **Single Responsibility** - Each class has a focused, well-defined purpose

## 🔧 Development

### Building from Source
```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run tests (when available)
dotnet test

# Publish for deployment
dotnet publish -c Release
```

### Available Tasks
The project includes VS Code tasks for common operations:
- `build` - Build the project
- `publish` - Create release build
- `watch` - Run with file watching for development

## 📁 Data Storage

Tasks are stored in a `todos.json` file in the application directory. The JSON format is human-readable:

```json
[
  {
    "description": "Complete project documentation",
    "state": "inProgress"
  },
  {
    "description": "Review code quality",
    "state": "done"
  }
]
```

## 🎨 User Interface

The application features a modern console interface with:
- **Color-coded states** - Visual distinction between task states
- **Interactive menus** - Arrow-key navigation and selection
- **Progress feedback** - Clear status messages for all operations
- **Error handling** - User-friendly error messages and recovery options
- **Emoji indicators** - Visual enhancement for better UX

## 🔒 Validation & Error Handling

### Input Validation
- Task descriptions cannot be empty or whitespace-only
- Maximum description length of 255 characters
- State transitions are validated and prevent redundant changes

### Error Recovery
- Graceful handling of file I/O errors
- Recovery from JSON parsing issues
- Non-interactive terminal fallback
- Comprehensive exception handling throughout

## 📊 Code Quality

The project maintains high code quality standards:
- **Grade: A** - Comprehensive code review available in [REVIEW.md](REVIEW.md)
- **100% Functional Compliance** - All specified requirements implemented
- **Production Ready** - Suitable for real-world usage
- **Best Practices** - Following C# and .NET conventions

## 🤝 Contributing

This project demonstrates AI-assisted development workflows and serves as a reference implementation for:
- Console application development with .NET
- Proper error handling and user experience design
- Modern C# programming practices
- JSON-based data persistence patterns

## 📄 License

This project is created for demonstration purposes as part of AI-assisted development workflows.

## 📞 Support

For questions or issues, please refer to the comprehensive [REVIEW.md](REVIEW.md) which contains detailed analysis of the codebase and implementation decisions.

---

**Built with ❤️ using .NET 9 and AI-assisted development**
