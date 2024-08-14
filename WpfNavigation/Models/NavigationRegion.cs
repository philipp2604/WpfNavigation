using System.Windows.Controls;

namespace WpfNavigation.Models;

/// Creates a new instance of the NavigationRegion class.
/// </remarks>
/// <param name="key">Key of the region.</param>
/// <param name="control">ContentControl of the region.</param>
public class NavigationRegion(string key, ContentControl control)
{
    /// <summary>
    /// The region's name.
    /// </summary>
    public string? Key { get; set; } = key;

    /// <summary>
    /// The region's ContentControl, used to display navigated content.
    /// </summary>
    public ContentControl Control { get; set; } = control;
}