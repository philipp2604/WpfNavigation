using System.Windows;
using WpfNavigation.Exceptions;
using WpfNavigation.Interfaces.ViewModels;

namespace WpfNavigation.Interfaces.Services;

/// <summary>
/// Describes the NavigationContentService, a class to store and provide view models.
/// </summary>
public interface INavigationContentService
{
    /// <summary>
    /// Registers a view-only content with a specified key.
    /// </summary>
    /// <typeparam name="TView">A <see cref="FrameworkElement"/> to be registered as the view./></typeparam>
    /// <param name="key">The key to recall the content.</param>
    public void RegisterContent<TView>(string key) where TView : FrameworkElement;

    /// <summary>
    /// Registers a content (with view and view model) with a specified key.
    /// </summary>
    /// <typeparam name="TView">A <see cref="FrameworkElement"/> to be registered as the view./></typeparam>
    /// <typeparam name="TViewModel">A type of <see cref="IViewModel"/> to be registered as the view model..</typeparam>
    /// <param name="key">The key to recall the content.</param>
    public void RegisterContent<TView, TViewModel>(string key)
        where TView : FrameworkElement
        where TViewModel : IViewModel;

    /// <summary>
    /// Checks if the content key is already registered.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True, if the key is already registered, otherwise false.</returns>
    /// <exception cref="NavigationContentServiceException"></exception>
    public bool KeyRegistered(string key);

    /// <summary>
    /// Returns a registered content's view, recalled by the key.
    /// </summary>
    /// <param name="key">Key to recall the content's view.</param>
    /// <returns>A <see cref="FrameworkElement"/> representing the view.</returns>
    /// <exception cref="NavigationContentServiceException"></exception>
    public FrameworkElement GetContentView(string key);

    /// <summary>
    /// Returns a registered content's view model, recalled by the key.
    /// </summary>
    /// <param name="key">Key to recall the content's view model</param>
    /// <returns>An <see cref="IViewModel"/> representing the view model.</returns>
    /// <exception cref="NavigationContentServiceException"></exception>
    public IViewModel? GetContentViewModel(string key);
}