# GitHub Copilot Instructions

## Code Style & Generation
- Use clear, descriptive names; modern JS/TS syntax; keep functions small
- **Default: Generate ONLY what's explicitly requested**
- Basic structure = empty methods with signatures only
- Minimal implementation = simplest working code with placeholders
- Add error handling, logging, validation, comments, optimizations ONLY when explicitly requested

## MCP Configuration
```json
{
  "mcp": {
    "servers": {
      "git": {
        "command": "npx",
        "args": ["@cyanheads/git-mcp-server"],
        "env": {"MCP_LOG_LEVEL": "info"}
      },
      "github": {
        "type": "http",
        "url": "https://api.githubcopilot.com/mcp/",
        "headers": {"Authorization": "Bearer ${input:github_mcp_pat}"}
      }
    }
  }
}
```

## Core Workflow Principles
- **ALWAYS** follow: Issue → Branch → Commits → PR → Merge
- **Git MCP** for local ops (pull, fetch, commit, branch, merge, status, diff)
- **GitHub MCP** for remote ops (issues, PRs, repository management)
- **NEVER** use terminal commands for git/gh operations
- **ALWAYS** link commits to GitHub issues

## Naming Conventions
- **Branches**: `feature/45-user-auth`, `bugfix/23-login-error`, `hotfix/99-security-patch`
- **Commits**: `type(scope): description - addresses #issue`
  - Types: `feat`, `fix`, `refactor`, `docs`, `test`, `chore`, `perf`, `style`
- **Issues**: `[Feature]: title` or `[Bug]: title` with proper labels

## Standard Workflows

### Start New Work
```
"Pull latest main, create [feature/bug] issue for [description], create branch [issue-number-name] from main"
```

### Development Cycle
```
1. "Show git status and stage [files]"
2. "Commit: 'type(scope): description - addresses #issue'"
3. "Fetch origin and merge main if needed"
4. "Push current branch"
```

### Create PR
```
"Create pull request for current branch targeting main for issue #[number]"
Auto-includes: title "#issue: description", issue link, labels
```

### Handle Review Feedback
```
"Make changes → stage → commit: 'fix(scope): address review feedback - addresses #issue' → push"
```

### Complete Work
```
"Merge PR, switch to main, pull latest, delete feature branch"
```

## Essential Commands

### Daily Operations
| Task | Command |
|------|---------|
| Start work on [feature] | `"Pull main, create issue and branch for [feature]"` |
| Check status | `"Show git status and uncommitted changes"` |
| Save progress | `"Stage [files] and commit: '[format] - addresses #issue'"` |
| Sync with team | `"Fetch origin, merge main if needed"` |
| Create PR | `"Push branch and create PR for issue #[num]"` |
| Clean up | `"Switch to main, pull, delete branch [name]"` |

### Advanced Operations
| Task | Command |
|------|---------|
| Handle conflicts | `"Show merge conflicts and guide resolution"` |
| View history | `"Show commit log/diff for [branch/file]"` |
| Stash work | `"Stash changes with message: '[desc]'"` |
| Branch management | `"Show all branches and tracking status"` |

## Quality Gates

### Pre-Commit Checks
- Proper commit message format with issue reference
- All changes staged and reviewed via `"Show diff of staged changes"`

### Pre-PR Checks
- Branch synced with main: `"Fetch origin and merge main if needed"`
- All commits follow format and reference valid issues
- Branch name follows convention

### Merge Requirements
- All reviews approved, CI passing, no conflicts, branch up-to-date

## Issue Management

### Feature Issues
```
Title: "[Feature]: Clear description"
Labels: "feature" + priority + component
Body: User story, acceptance criteria, technical approach
```

### Bug Issues
```
Title: "[Bug]: Problem description"  
Labels: "bug" + severity + component
Body: Steps to reproduce, expected vs actual, environment
```

## PR Template
```markdown
## Description
Brief overview of changes

## Related Issues
- Closes #[issue-number]

## Changes Made
- [ ] Change 1
- [ ] Change 2
- [ ] Tests added/updated

## Testing
- [ ] Unit tests pass
- [ ] Manual testing complete

## Breaking Changes
- [ ] None / [ ] Documented below
```

## Error Recovery

| Problem | Solution |
|---------|----------|
| Sync issues | `"Fetch origin and show branch status vs remote"` |
| Merge conflicts | `"Show conflicts and resolve step by step"` |
| Lost changes | `"Show reflog/stash list to recover"` |
| Branch cleanup | `"Prune remote tracking branches"` |

## Hotfix Process
```
Critical issues only:
1. "Pull main → create hotfix branch from main"
2. "Fix → commit: 'fix(security): patch - addresses #issue'"  
3. "Push → create urgent PR with immediate review"
4. After merge: "Backport to development branches"
```

## Project-Specific Functional Specifications

### Functional Specifications - Simple ToDo App

#### Overview
A minimalist ToDo application that allows users to manage basic task items with essential CRUD operations (Create, Read, Delete) and simple state tracking.

### Core Entities

#### ToDoItem
- **Description**: String field containing the task description (required, max 255 characters)
- **State**: Enumerated field with exactly three possible values:
  - `"new"` - Default state for newly created items
  - `"in progress"` - Item is being worked on
  - `"done"` - Item has been completed

### Functional Requirements

#### 1. Create ToDo Item
- **Action**: Add a new task to the system
- **Input**: Task description (string, required)
- **Behavior**: 
  - Create new ToDoItem with provided description
  - Set initial state to `"new"`
  - Add item to the active list
- **Validation**: Description cannot be empty or whitespace-only

#### 2. List ToDo Items
- **Action**: Display all existing tasks
- **Output**: Complete list of all ToDoItems showing:
  - Task description
  - Current state
- **Behavior**: 
  - Show all items regardless of state
  - Display in order of creation (newest first recommended)
  - Empty list should display appropriate message

#### 3. Delete ToDo Item
- **Action**: Remove a task from the system
- **Input**: Item identifier or selection mechanism
- **Behavior**: 
  - Permanently remove the selected item
  - No confirmation dialog required (keep it simple)
  - Item cannot be recovered after deletion

#### 4. Update Item State
- **Action**: Change the status of an existing task
- **Input**: Item identifier and new state value
- **Behavior**:
  - Update item state to one of the three allowed values
  - State transitions are unrestricted (any state can change to any other state)
- **Validation**: New state must be one of: `"new"`, `"in progress"`, `"done"`

### Non-Functional Requirements

#### Simplicity Constraints
- No user authentication or multi-user support
- No due dates, priorities, or categories
- No data persistence requirements specified (implementation choice)
- No search or filter functionality
- No bulk operations (select all, delete multiple, etc.)
- No item editing (description cannot be modified after creation)

#### User Interface
- Should be intuitive and require minimal learning
- Clear visual distinction between different item states
- Simple interaction patterns (click, tap, or basic form submission)

### Edge Cases & Error Handling
- Empty description submission should be rejected with clear feedback
- Attempting to delete non-existent item should fail gracefully
- Invalid state transitions should be prevented with appropriate messaging
- Application should handle empty list state appropriately

### Success Criteria
- User can create a new task with description
- User can view all tasks and their current states
- User can change any task's state between the three options
- User can delete any task permanently
- Application maintains data integrity throughout all operations

## Project-Specific Technical Specifications  
### Framework & Runtime
.NET 9 Console Application
Target Framework: net9.0
Entry Point: Main() method

### Dependencies
Spectre.Console: For colored console output
System.Text.Json: For JSON serialization (built-in)

### Data Storage
Format: JSON file
Location: Program directory (todos.json)
Persistence: File-based, no database

### Application Type
Console application with colored text output
Single-user, local execution
Command-line interface


## Integration Rules
When generating code:
1. ALWAYS follow the functional requirements above
2. ALWAYS implement according to technical specifications above
3. Reference specific sections by name when explaining decisions