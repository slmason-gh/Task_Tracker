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
        /// Get all task details
        /// </summary>
        /// <returns>A multiline string with all task details</returns>
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

        /// <summary>
        /// Get a short summary of task details
        /// </summary>
        /// <returns>A string with the task title, priority and status</returns>
        public string GetShortDetails()
        {
            return $"{Title} | Priority: {Priority} | Status: {Status}";
        }

        /// <summary>
        /// Get a summary of task details
        /// </summary>
        /// <returns>A string with the task title, priority, status and due date</returns>
        public override string ToString()
        {
            return $"{Title} | Priority: {Priority} | Status: {Status} | Due: {(DueDate.HasValue ? DueDate.Value.ToShortDateString() : "N/A")}";
        }
    }
}
