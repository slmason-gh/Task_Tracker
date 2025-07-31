using System;

namespace Task_Tracker
{
    internal class Program
    {
        static void Main()
        {
            // Create an instance of TaskManager
            TaskManager manager = new TaskManager();

            // Load existing tasks from file if available
            manager.LoadTasks(startup: true);

            // Main loop for the task tracker application
            bool running = true;
            while (running)
            {
                try
                {   
                    // Clear the console and display the menu
                    Console.Clear();
                    Console.WriteLine(String.Join("\n", new[]
                        {
                        "== TASK TRACKER ==",
                        "1. View All Tasks",
                        "2. View Task Details",
                        "3. Add New Task",
                        "4. Update Task",
                        "5. Remove Task",
                        "6. Filter Tasks",
                        "7. Save Tasks to File",
                        "8. Load Tasks from File",
                        "0. Exit"
                        })
                    );
                    Console.Write("Select an option: ");

                        // Read user input and perform the corresponding action
                        switch (Console.ReadLine())
                        {
                            case "1":
                                manager.ViewTasks();
                                break;
                            case "2":
                                manager.ViewTaskDetails();
                                break;
                            case "3":
                                manager.AddTask();
                                break;
                            case "4":
                                manager.UpdateTask();
                                break;
                            case "5":
                                manager.RemoveTask();
                                break;
                            case "6":
                                manager.FilterTasks();
                                break;
                            case "7":
                                manager.SaveTasks();
                                break;
                            case "8":
                                manager.LoadTasks();
                                break;
                            case "0":
                                running = false;
                                break;
                            default:
                                Console.WriteLine("Invalid option. Press Enter to continue.");
                                Console.ReadLine();
                                break;
                        }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during the execution of the program
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
            }
            // Print a message and exit the application
            Console.WriteLine("Goodbye!");
        }
    }
}
