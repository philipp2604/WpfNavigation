using System.Windows;
using System.Windows.Controls;
using WpfNavigation.Exceptions;
using WpfNavigation.Interfaces.Services;
using WpfNavigation.Interfaces.ViewModels;
using WpfNavigation.Models;

namespace WpfNavigation.Services;

/// <summary>
/// Implements IRegionNavigationService, a class to navigate between views inside regions.
/// </summary>
/// <param name="contentService">The content service to provide the views and view models.</param>
public class RegionNavigationService(INavigationContentService contentService) : IRegionNavigationService
{
    private readonly INavigationContentService _contentService = contentService;
    private static readonly Dictionary<string, NavigationRegion> _regions = [];

    /// <summary>
    /// Removes all regions.
    /// </summary>
    public static void ClearRegions()
    {
        _regions.Clear();
    }

    /// <summary>
    /// Registers a navigation region.
    /// </summary>
    /// <param name="region">The region to register.</param>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public static void RegisterRegion(NavigationRegion region)
    {
        if (region.Key == null)
            throw RegionNavigationServiceException.Prebuilt.ArgumentNullException(nameof(region.Key));

        if (region.Control == null)
            throw RegionNavigationServiceException.Prebuilt.ArgumentNullException(nameof(region.Control));

        if (KeyRegistered(region.Key))
            throw RegionNavigationServiceException.Prebuilt.RegionKeyAlreadyRegistered(region.Key);

        _regions.Add(region.Key, region);
    }

    /// <inheritdoc/>
    void IRegionNavigationService.RegisterRegion(NavigationRegion region)
    {
        RegisterRegion(region);
    }

    /// <summary>
    /// Creates and registers a navigatin region.
    /// </summary>
    /// <param name="key">The region's key to recall it.</param>
    /// <param name="control">The region's ContentControl, used to display the navigation content.</param>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public static void RegisterRegion(string key, ContentControl control)
    {
        if (key == null)
            throw RegionNavigationServiceException.Prebuilt.ArgumentNullException(nameof(key));
        if (control == null)
            throw RegionNavigationServiceException.Prebuilt.ArgumentNullException(nameof(control));

        if (KeyRegistered(key))
            throw RegionNavigationServiceException.Prebuilt.RegionKeyAlreadyRegistered(key);

        _regions.Add(key, new NavigationRegion(key, control));
    }

    /// <inheritdoc/>
    void IRegionNavigationService.RegisterRegion(string key, ContentControl control)
    {
        RegisterRegion(key, control);
    }

    /// <summary>
    /// Returns a regitered <see cref="NavigationRegion"/>.
    /// </summary>
    /// <param name="key">The region's registered key.</param>
    /// <returns>The <see cref="NavigationRegion"/> registered with the key.</returns>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public static NavigationRegion GetRegion(string key)
    {
        return key == null ?
            throw RegionNavigationServiceException.Prebuilt.ArgumentNullException(nameof(key))
            : !KeyRegistered(key) ?
            throw RegionNavigationServiceException.Prebuilt.RegionKeyNotRegistered(key) : _regions[key];
    }

    /// <inheritdoc/>
    NavigationRegion IRegionNavigationService.GetRegion(string key)
    {
        return GetRegion(key);
    }

    /// <inheritdoc/>
    public void Navigate(string regionKey, string contentKey, object? parametersNavigatingFrom = null, object? parametersNavigatingTo = null)
    {
        if (regionKey == null)
            throw RegionNavigationServiceException.Prebuilt.ArgumentNullException(nameof(regionKey));

        if (contentKey == null)
            throw RegionNavigationServiceException.Prebuilt.ArgumentNullException(nameof(contentKey));

        if (!KeyRegistered(regionKey))
            throw RegionNavigationServiceException.Prebuilt.RegionKeyNotRegistered(regionKey);

        object? lastViewModel = null;
        var view = _contentService.GetContentView(contentKey);
        var viewModel = _contentService.GetContentViewModel(contentKey);

        if (_regions[regionKey].Control.Content != null)
        {
            var dataContext = ((FrameworkElement)_regions[regionKey].Control.Content).DataContext;
            if(dataContext != null)
                lastViewModel = dataContext;

            if (dataContext?.GetType().IsAssignableTo(typeof(INavigationAware)) == true)
                ((INavigationAware)dataContext).OnNavigatedFrom(viewModel, parametersNavigatingFrom);
        }

        _regions[regionKey].Control.Content = view;
        ((FrameworkElement)_regions[regionKey].Control.Content).DataContext = viewModel;

        if(viewModel?.GetType().IsAssignableTo(typeof(INavigationAware)) == true)
            ((INavigationAware)viewModel).OnNavigatedTo(lastViewModel, parametersNavigatingTo);
    }

    /// <inheritdoc/>
    public async Task NavigateAsync(string regionKey, string contentKey, object? parametersNavigatingFrom = null, object? parametersNavigatingTo = null)
    {
        if (regionKey == null)
            throw RegionNavigationServiceException.Prebuilt.ArgumentNullException(nameof(regionKey));

        if (contentKey == null)
            throw RegionNavigationServiceException.Prebuilt.ArgumentNullException(nameof(contentKey));

        if (!KeyRegistered(regionKey))
            throw RegionNavigationServiceException.Prebuilt.RegionKeyNotRegistered(regionKey);

        object? lastViewModel = null;
        var view = _contentService.GetContentView(contentKey);
        var viewModel = _contentService.GetContentViewModel(contentKey);

        if (_regions[regionKey].Control.Content != null)
        {
            var dataContext = ((FrameworkElement)_regions[regionKey].Control.Content).DataContext;
            if (dataContext != null)
                lastViewModel = dataContext;

            if (dataContext?.GetType().IsAssignableTo(typeof(INavigationAware)) == true)
                await ((INavigationAware)dataContext).OnNavigatedFromAsync(viewModel, parametersNavigatingFrom);

            _regions[regionKey].Control.Content = view;
            ((FrameworkElement)_regions[regionKey].Control.Content).DataContext = viewModel;

            if (viewModel?.GetType().IsAssignableTo(typeof(INavigationAware)) == true)
                await ((INavigationAware)viewModel).OnNavigatedToAsync(lastViewModel, parametersNavigatingTo);

            await Task.CompletedTask;
        }
    }

    /// <summary>
    /// Checks if a region's key is already registered.
    /// </summary>
    /// <param name="key">The region key to check.</param>
    /// <returns>True if the key is already registered, otherwise false.</returns>
    /// <exception cref="RegionNavigationServiceException"></exception>
    public static bool KeyRegistered(string key)
    {
        return key == null ? throw RegionNavigationServiceException.Prebuilt.ArgumentNullException(nameof(key)) : _regions.ContainsKey(key);
    }

    /// <inheritdoc/>
    bool IRegionNavigationService.KeyRegistered(string key)
    {
        return KeyRegistered(key);
    }

    //Allows adding a "Region" property inside the designer.

    #region DependecyProperty

    /// <summary>
    /// The attached dependecy property for the "NavigationRegion" property.
    /// </summary>
    public static readonly DependencyProperty RegionProperty =
        DependencyProperty.RegisterAttached(
            "NavigationRegion", typeof(string), typeof(IRegionNavigationService),
            new PropertyMetadata(
                null, OnRegionSet)
            );

    /// <summary>
    /// Callback function to register a NavigationRegion, if a valid "NavigationRegion" property was set.
    /// </summary>
    /// <param name="d">The <see cref="DependencyObject"/> which owns the property.</param>
    /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/>.</param>
    /// <exception cref="RegionNavigationServiceException"></exception>
    private static void OnRegionSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue == null)
            return;
        if (e.NewValue != null && e.NewValue.GetType() != typeof(string))
            return;

        if (d.GetType() != typeof(ContentControl))
            throw RegionNavigationServiceException.Prebuilt.UnsupportedControlType();

        RegisterRegion((string)e.NewValue!, (ContentControl)d);
    }

    /// <summary>
    /// Setter method for the "NavigationRegion" property.
    /// </summary>
    /// <param name="control"></param>
    /// <param name="key"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void SetNavigationRegion(DependencyObject control, string key)
    {
        ArgumentNullException.ThrowIfNull(control);

        control.SetValue(RegionProperty, key);
    }

    /// <summary>
    /// Getter method for the "Region" property."
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string GetNavigationRegion(DependencyObject control)
    {
        ArgumentNullException.ThrowIfNull(control);

        return (string)control.GetValue(RegionProperty) ?? "";
    }

    #endregion DependecyProperty
}