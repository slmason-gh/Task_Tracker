using System;
using Task_Tracker.Helpers;

namespace Task_Tracker
{
    /// <summary>
    /// A task, with its name and information
    /// </summary>
    internal sealed class TaskItem
    {
        // Task details
        private string _title;
        public string Title
        {
            get => _title;
            set => _title = string.IsNullOrWhiteSpace(value) ? "Untitled" : value;
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => _description = string.IsNullOrWhiteSpace(value) ? "None" : value;
        }

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
                $"Priority: {EnumHelper.GetDescription(Priority)}",
                $"Status: {EnumHelper.GetDescription(Status)}",
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
            return $"{Title} | Priority: {EnumHelper.GetDescription(Priority)} | Status: {EnumHelper.GetDescription(Status)}";
        }

        /// <summary>
        /// Get a summary of task details
        /// </summary>
        /// <returns>A string with the task title, priority, status and due date</returns>
        public override string ToString()
        {
            return $"{Title} | Priority: {EnumHelper.GetDescription(Priority)} | Status: {EnumHelper.GetDescription(Status)} | Due: {(DueDate.HasValue ? DueDate.Value.ToShortDateString() : "N/A")}";
        }
    }
}
