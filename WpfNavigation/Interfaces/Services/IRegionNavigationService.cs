using System.Windows.Controls;
using WpfNavigation.Exceptions;
using WpfNavigation.Models;

namespace WpfNavigation.Interfaces.Services;

/// <summary>
/// Describes the RegionNavigationService, a class to navigate between views inside regions.
/// </summary>
public interface IRegionNavigationService
{
    /// <summary>
    /// Registers a navigation region.
    /// </summary>
    /// <param name="region">The region to register.</param>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public void RegisterRegion(NavigationRegion region);

    /// <summary>
    /// Creates and registers a navigatin region.
    /// </summary>
    /// <param name="key">The region's key to recall it.</param>
    /// <param name="control">The region's ContentControl, used to display the navigation content.</param>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public void RegisterRegion(string key, ContentControl control);

    /// <summary>
    /// Returns a regitered <see cref="NavigationRegion"/>.
    /// </summary>
    /// <param name="key">The region's registered key.</param>
    /// <returns>The <see cref="NavigationRegion"/> registered with the key.</returns>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public NavigationRegion GetNavigationRegion(string key);

    /// <summary>
    /// Navigates to the specified content.
    /// </summary>
    /// <param name="regionKey">Region that shall navigate.</param>
    /// <param name="contentKey">The content to navigate to.</param>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public void Navigate(string regionKey, string contentKey);

    /// <summary>
    /// Checks if a region's key is already registered.
    /// </summary>
    /// <param name="key">The region key to check.</param>
    /// <returns>True if the key is already registered, otherwise false.</returns>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public bool KeyRegistered(string key);
}