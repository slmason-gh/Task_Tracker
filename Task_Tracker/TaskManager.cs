﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task_Tracker.Helpers;

namespace Task_Tracker
{
    /// <summary>
    /// Task management class to handle task operations
    /// </summary>
    internal class TaskManager
    {

        // List to store tasks
        private readonly List<TaskItem> tasks = new List<TaskItem>();

        /// <summary>
        /// Print a message and wait for user input to continue
        /// </summary>
        private void Pause() { 
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        /// <summary>
        /// Print a numbered list of the task names and their statuses
        /// </summary>
        private void ListTasks()
        {
            // Print the short details of each task in a numbered list
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i].GetShortDetails()}");
            }
        }

        /// <summary> 
        /// Get the user's choice of task from the list 
        /// </summary>
        /// <returns>Index of the selected task, or -1 if invalid</returns>
        private int ChooseTask()
        {
            ListTasks();
            Console.Write("Enter the task number: ");
            if (int.TryParse(Console.ReadLine(), out int choice) &&
                    choice >= 1 && choice <= tasks.Count)
            {
                return choice - 1; // Return zero-based index
            }
            else
            {
                return -1; // Return -1 for invalid selection
            }

        }

        /// <summary>
        /// Add a new task
        /// </summary>
        public void AddTask()
        {
            Console.Clear();
            Console.WriteLine("== ADD NEW TASK ==");

            // Prompt user for task details

            // Title
            string title;
            do
            {
                Console.Write("Enter Title: ");
                title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                    Console.WriteLine("Title cannot be empty.");
            } while (string.IsNullOrWhiteSpace(title));


            // Description
            Console.Write("Enter Description or leave empty for none: ");
            string description = Console.ReadLine();

            // Priority
            Console.WriteLine("Available priorities:");
            int? priorityIndex = EnumHelper.ChooseEnumValue<TaskPriority>("Enter Priority: ");
            TaskPriority priority = (priorityIndex.HasValue && priorityIndex.Value > -1)
                ? (TaskPriority)priorityIndex.Value
                : TaskPriority.Medium;

            // Status
            Console.WriteLine("Available statuses:");
            int? statusIndex = EnumHelper.ChooseEnumValue<TaskStatus>("Enter status: ");
            TaskStatus status = (statusIndex.HasValue && statusIndex.Value > -1)
                ? (TaskStatus)statusIndex.Value
                : TaskStatus.Pending;

            Console.Write("Enter Due Date (yyyy-mm-dd) or leave empty for none: ");
            // Try to parse the due date, assignigning null if parsing fails
            DateTime? due = null;
            if (DateTime.TryParse(Console.ReadLine(), out var parsed)) due = parsed;

            // Create a new task item and add it to the list
            tasks.Add(new TaskItem
            {
                Title = title,
                Description = description,
                Priority = priority,
                Status = status,
                DueDate = due
            });

            Console.WriteLine("Task added!");
            Pause();
        }

        /// <summary>
        /// View all tasks
        /// </summary>
        public void ViewTasks()
        {
            Console.Clear();
            Console.WriteLine("== VIEW ALL TASKS ==");
            // If no tasks, print a message; otherwise, print each task
            if (!tasks.Any())
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                foreach (var task in tasks) { Console.WriteLine(task); }
            }

            Pause();
        }

        /// <summary>
        /// View the details of a selected task
        /// </summary>
        public void ViewTaskDetails()
        {
            Console.Clear();
            Console.WriteLine("== VIEW TASK DETAILS ==");
            // If no tasks, print a message; otherwise, list tasks and prompt for selection
            if (!tasks.Any())
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                // List tasks and prompt for selection
                int taskIndex = ChooseTask();
                if (taskIndex > -1)
                {
                    Console.Clear();
                    Console.WriteLine("== TASK DETAILS ==");
                    Console.WriteLine(tasks[taskIndex].GetDetails());
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }
            Pause();
        }

        /// <summary>
        /// Update all information of a task
        /// </summary>
        public void UpdateTask()
        {
            Console.Clear();
            Console.WriteLine("== UPDATE TASK ==");

            if (!tasks.Any())
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                int taskIndex = ChooseTask();
                if (taskIndex > -1)
                {
                    var task = tasks[taskIndex];

                    // Title
                    Console.WriteLine($"Title [{task.Title}]");
                    Console.Write("Enter new title (or leave empty to keep current): ");
                    string input = Console.ReadLine();
                    task.Title = (!string.IsNullOrWhiteSpace(input)) ? input : task.Title;

                    // Description
                    Console.WriteLine($"Description [{task.Description}]");
                    Console.Write("Enter new description (or leave empty to keep current): ");
                    input = Console.ReadLine();
                    task.Description = (!string.IsNullOrWhiteSpace(input)) ? input : task.Description;

                    // Priority
                    Console.WriteLine($"Priority [{EnumHelper.GetDescription(task.Priority)}]: ");
                    int? priorityIndex = EnumHelper.ChooseEnumValue<TaskPriority>("Enter new priority (or leave empty to keep current): ", true);
                    if (priorityIndex.HasValue && priorityIndex.Value > -1)
                        task.Priority = (TaskPriority)priorityIndex.Value;

                    // Status
                    Console.WriteLine($"Status [{EnumHelper.GetDescription(task.Status)}]: ");
                    int? statusIndex = EnumHelper.ChooseEnumValue<TaskStatus>("Enter new status (or leave empty to keep current): ", true);
                    if (statusIndex.HasValue && statusIndex.Value > -1)
                        task.Status = (TaskStatus)statusIndex.Value;

                    // Due Date
                    Console.WriteLine($"Due Date [{(task.DueDate.HasValue ? task.DueDate.Value.ToString("yyyy-MM-dd") : "N/A")}]");
                    Console.Write($"Due Date (yyyy-mm-dd) (or leave empty to keep current): ");
                    input = Console.ReadLine();
                    task.DueDate = DateTime.TryParse(input, out var parsed) ? parsed : task.DueDate;

                    Console.WriteLine("Task updated!");
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }
            Pause();
        }

        /// <summary>
        /// Remove a task
        /// </summary>
        public void RemoveTask()
        {
            Console.Clear();
            Console.WriteLine("== REMOVE TASK ==");

            if (!tasks.Any())
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                // List tasks and prompt for selection
                int taskIndex = ChooseTask();
                if (taskIndex > -1)
                {
                    // Prompt for confirmation
                    Console.Write($"Are you sure you want to remove the task: {tasks[taskIndex].Title}? (Y/N)");
                    string input = Console.ReadLine();
                    if (string.Equals(input, "Y", StringComparison.OrdinalIgnoreCase))
                    {
                        // Remove the selected task
                        tasks.RemoveAt(taskIndex);
                        Console.WriteLine("Task removed.");
                    }
                    else
                    {
                        Console.WriteLine("Removal cancelled.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }
            Pause();
        }

        /// <summary>
        /// Filter tasks
        /// </summary>
        public void FilterTasks()
        {
            Console.Clear();
            Console.WriteLine("== FILTER TASKS ==");
            Console.WriteLine("1. Filter by Status");
            Console.WriteLine("2. Filter by Priority");
            Console.Write("Choose filter type: ");
            string filterType = Console.ReadLine();

            if (filterType == "1")
            {
                int? statusIndex = EnumHelper.ChooseEnumValue<TaskStatus>("Select status: ");
                if (statusIndex.HasValue && statusIndex.Value > -1)
                {
                    var status = (TaskStatus)statusIndex.Value;
                    var filtered = tasks.Where(t => t.Status == status);

                    if (!filtered.Any())
                    {
                        Console.WriteLine("No tasks found with the selected status.");
                        Pause();
                        return;
                    }
                    else
                    {
                        // Print the filtered tasks
                        Console.Clear();
                        Console.WriteLine($"== TASKS FILTERED BY STATUS: {EnumHelper.GetDescription(status)} == ");
                        foreach (var task in filtered)
                            Console.WriteLine(task);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid status selection.");
                }
            }
            else if (filterType == "2")
            {
                int? priorityIndex = EnumHelper.ChooseEnumValue<TaskPriority>("Select priority: ");
                if (priorityIndex.HasValue && priorityIndex.Value > -1)
                {
                    var priority = (TaskPriority)priorityIndex.Value;
                    var filtered = tasks.Where(t => t.Priority == priority);

                    if (!filtered.Any())
                    {
                        Console.WriteLine("No tasks found with the selected priority.");
                        Pause();
                        return;
                    }
                    else
                    {
                        // Print the filtered tasks
                        Console.Clear();
                        Console.WriteLine($"== TASKS FILTERED BY PRIORITY: {EnumHelper.GetDescription(priority)} == ");
                        foreach (var task in filtered)
                            Console.WriteLine(task);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid priority selection.");
                }
            }
            else
            {
                Console.WriteLine("Invalid filter type.");
            }

            Pause();
        }

        /// <summary>
        /// Save all tasks to a file
        /// </summary>
        public void SaveTasks(string path = "tasks.txt")
        {
            Console.Clear();
            Console.WriteLine("== SAVE TASKS ==");

            // Check if there are no tasks to save, and prompt the user for confirmation
            if (!tasks.Any())
            {
                Console.WriteLine("Warning: There are no tasks to save.");
                Console.WriteLine("If you continue, the tasks file will be overwritten and left blank.");
                Console.Write("Do you want to overwrite the file? (Y/N): ");
                var input = Console.ReadLine();
                if (!string.Equals(input, "Y", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Save cancelled.");
                    Pause();
                    return;
                }
            }

            try
            {
                // Get all task properties in a string format to write/overwrite to file
                var lines = tasks.Select(t => $"{t.Title}|{t.Description}|{t.Priority}|{t.Status}|{t.DueDate}");
                File.WriteAllLines(path, lines);
                Console.WriteLine("Tasks saved!");
            }
            catch (Exception e)
            {
                // Catch any exceptions that occur during file operations and print the error message
                Console.WriteLine($"Error saving: {e.Message}");
            }
            Pause();
        }

        /// <summary>
        /// Load all tasks from a file
        /// </summary>
        /// <param name="path">Path to the file containing tasks</param>
        /// <param name="startup">Automatically load tasks without menu options and text</param>
        public void LoadTasks(string path = "tasks.txt", bool startup = false)
        {
            if (!startup)
            { 
                Console.Clear();
                Console.WriteLine("== LOAD TASKS ==");
            }

            try
            {
                var lines = File.ReadAllLines(path);

                tasks.Clear();
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length < 5)
                        continue; // Skip malformed lines

                    // Parse Priority
                    if (!Enum.TryParse(parts[2], out TaskPriority priority))
                        priority = TaskPriority.Medium; // Default if invalid

                    // Parse Status
                    if (!Enum.TryParse(parts[3], out TaskStatus status))
                        status = TaskStatus.Pending; // Default if invalid

                    // Parse DueDate
                    DateTime? dueDate = null;
                    if (!string.IsNullOrEmpty(parts[4]))
                    {
                        if (DateTime.TryParse(parts[4], out DateTime parsedDate))
                            dueDate = parsedDate;
                    }

                    tasks.Add(new TaskItem
                    {
                        Title = parts[0],
                        Description = parts[1],
                        Priority = priority,
                        Status = status,
                        DueDate = dueDate
                    });
                }
                if (!startup){
                    Console.WriteLine("Tasks loaded!");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error loading: File not found.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error loading: {e.Message}");
            }
            if (!startup){
                Pause();
            }
        }
    }
}