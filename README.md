# Task Tracker Console App

A simple C# console application for tracking tasks, built to gain practical experience and demonstrate key C# concepts such as classes, enums, collections, file I/O, user input handling, and basic application structure.

## Features

- Add, view, update, and remove tasks
- Filter tasks by status or priority
- View detailed or summary information for each task
- Save tasks to and load tasks from a file
- User-friendly prompts and input validation
- Demonstrates use of enums, nullable types, LINQ, and exception handling

## Getting Started

### Prerequisites

- [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)
- Visual Studio 2022 or later (recommended)

### Running the App

1. Clone this repository:
    ```sh
    git clone https://github.com/slmason-gh/Task_Tracker.git
    ```
2. Open the solution in Visual Studio.
3. Build and run the project (F5 or Ctrl+F5).

### Usage

- The main menu will guide you through all available actions.
- When adding or updating a task, you can select priority and status from a list.
- Tasks are saved to a text file (`tasks.txt`) in the application directory.
- If you try to save with no tasks, you will be warned before overwriting the file.

## Key C# Concepts Demonstrated

- **Classes & Objects:** `TaskManager`, `TaskItem`
- **Enums with Descriptions:** `TaskPriority`, `TaskStatus` (using `[Description]` attributes)
- **Collections:** `List<T>`
- **LINQ:** Filtering and displaying tasks
- **File I/O:** Saving and loading tasks
- **Exception Handling:** Robust error messages for file operations and user input
- **User Input:** Console prompts, validation, and confirmation
- **Nullable Types:** Optional due dates
- **Method Overriding:** Custom `ToString()` for task summaries
- **Property Validation:** Centralized validation for title and description
- **Static Helpers:** `EnumHelper` for enum display and selection

## Main Menu Example
```
== TASK TRACKER ==
1. View All Tasks
2. View Task Details
3. Add New Task
4. Update Task
5. Remove Task
6. Filter Tasks by Status or Priority
7. Save Tasks to File
8. Load Tasks from File
0. Exit
Select an option:
```
