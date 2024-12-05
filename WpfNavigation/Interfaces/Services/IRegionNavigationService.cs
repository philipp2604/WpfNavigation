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
    /// Creates and registers a navigation region.
    /// </summary>
    /// <param name="key">The region's key to recall it.</param>
    /// <param name="control">The region's ContentControl, used to display the navigation content.</param>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public void RegisterRegion(string key, ContentControl control);

    /// <summary>
    /// Removes a registered navigation region from the dictionary.
    /// </summary>
    /// <param name="key">The region's key to remove.</param>
    public void UnregisterRegion(string key);

    /// <summary>
    /// Returns a registered <see cref="NavigationRegion"/>.
    /// </summary>
    /// <param name="key">The region's registered key.</param>
    /// <returns>The <see cref="NavigationRegion"/> registered with the key.</returns>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public NavigationRegion GetRegion(string key);

    /// <summary>
    /// Navigates to the specified content and optionally calls the callbacks of <see cref="INavigationAware"/> view models.
    /// </summary>
    /// <param name="regionKey">Region that shall navigate.</param>
    /// <param name="contentKey">The content to navigate to.</param>
    /// <param name="sender">Sender that invokes the navigation.</param>
    /// <param name="parametersNavigatingFrom">Optional parameters passed to the current view model.</param>
    /// <param name="parametersNavigatingTo">Optional parameters passed to the next view model.</param>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public void Navigate(string regionKey, string contentKey, object? sender, object? parametersNavigatingFrom = null, object? parametersNavigatingTo = null);

    /// <summary>
    /// Navigates to the specified content and optionally calls the asynchronous callbacks of <see cref="INavigationAware"/> view models.
    /// </summary>
    /// <param name="regionKey">Region that shall navigate.</param>
    /// <param name="contentKey">The content to navigate to.</param>
    /// <param name="sender">Sender that invokes the navigation.</param>
    /// <param name="parametersNavigatingFrom">Optional parameters passed to the current view model.</param>
    /// <param name="parametersNavigatingTo">Optional parameters passed to the next view model.</param>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public Task NavigateAsync(string regionKey, string contentKey, object? sender, object? parametersNavigatingFrom = null, object? parametersNavigatingTo = null);

    /// <summary>
    /// Checks if a region's key is already registered.
    /// </summary>
    /// <param name="key">The region key to check.</param>
    /// <returns>True if the key is already registered, otherwise false.</returns>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public bool KeyRegistered(string key);
}