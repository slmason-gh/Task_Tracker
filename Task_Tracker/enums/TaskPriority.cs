using System.ComponentModel;

/// <summary>
/// Priority of a task
/// </summary>
public enum TaskPriority
{
    [Description("Low")]
    Low,
    [Description("Medium")]
    Medium,
    [Description("High")]
    High,
    [Description("Urgent")]
    Urgent,
    [Description("Critical")]
    Critical
}