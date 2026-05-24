using SiLadhida.Core.Enums;

namespace SiLadhida.API.DTOs;

/// <summary>
/// Data transfer object for updating an order status
/// </summary>
public class UpdateStatusDto
{
    /// <summary>
    /// The state transition trigger for status update
    /// </summary>
    public StateTrigger Trigger { get; set; }
}