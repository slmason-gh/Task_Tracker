using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Tracker
{
    /// <summary>
    /// A task, with its name and information
    /// </summary>
    internal class TaskItem
    {
        // Task details
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; }

        /// <summary>
        /// Print the task details
        /// </summary>
        /// <returns></returns>
        public string GetDetails()
        {
            return String.Join("\n", new[]
            {
                $"Task: {Title}",
                $"Priority: {Priority}",
                $"Status: {Status}",
                $"Due: {(DueDate.HasValue ? DueDate.Value.ToShortDateString() : "N/A")}",
                $"Description: {Description}"
            });
        }
        
        public string GetShortDetails()
        {
            return $"{Title} | Priority: {Priority} | Status: {Status}";
        }

        // Print a summary of the task details
        public override string ToString()
        {
            return $"{Title} | Priority: {Priority} | Status: {Status} | Due: {(DueDate.HasValue ? DueDate.Value.ToShortDateString() : "N/A")}";
        }
    }
}
