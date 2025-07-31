using System.ComponentModel;

/// <summary>
/// Status of a task
/// </summary>
public enum TaskStatus
{
    [Description("Pending")]
    Pending,
    [Description("In Progress")]
    InProgress,
    [Description("Completed")]
    Completed,
    [Description("On Hold")]
    OnHold,
    [Description("Cancelled")]
    Cancelled
}