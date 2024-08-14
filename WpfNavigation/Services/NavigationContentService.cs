using System.Windows;
using WpfNavigation.Exceptions;
using WpfNavigation.Interfaces.Services;
using WpfNavigation.Interfaces.ViewModels;

namespace WpfNavigation.Services;

/// <summary>
/// Implements INavigationContentService, a class to store and provide views and view models.
/// </summary>
/// <param name="serviceProvider">Service provider to request service instances.</param>
public class NavigationContentService(IServiceProvider serviceProvider) : INavigationContentService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly Dictionary<string, (Type view, Type? viewModel)> _content = [];

    /// <inheritdoc/>
    public void RegisterContent<TView>(string key) where TView : FrameworkElement
    {
        RegisterContent(key, typeof(TView), null);
    }

    /// <inheritdoc/>
    public void RegisterContent<TView, TViewModel>(string key)
        where TView : FrameworkElement
        where TViewModel : IViewModel
    {
        RegisterContent(key, typeof(TView), typeof(TViewModel));
    }

    /// <inheritdoc/>
    public bool KeyRegistered(string key)
    {
        return key == null ? throw NavigationContentServiceException.Prebuilt.ArgumentNullException(nameof(key)) : _content.ContainsKey(key);
    }

    /// <summary>
    /// Registers the content with view (optionally view model) for a key.
    /// </summary>
    /// <param name="key">Content key.</param>
    /// <param name="view">The content's view type.</param>
    /// <param name="viewModel">The content's view model type.</param>
    private void RegisterContent(string key, Type view, Type? viewModel)
    {
        if (key == null)
            throw NavigationContentServiceException.Prebuilt.ArgumentNullException(nameof(key));

        if (view == null)
            throw NavigationContentServiceException.Prebuilt.ArgumentNullException(nameof(view));

        if (KeyRegistered(key))
            throw NavigationContentServiceException.Prebuilt.ContentKeyAlreadyRegistered(key);

        _content.Add(key, (view, viewModel));
    }

    /// <inheritdoc/>
    public FrameworkElement GetContentView(string key)
    {
        if (key == null)
            throw NavigationContentServiceException.Prebuilt.ArgumentNullException(nameof(key));

        if (!KeyRegistered(key))
            throw NavigationContentServiceException.Prebuilt.ContentKeyNotRegistered(key);

        var view = _serviceProvider.GetService(_content[key].view) as FrameworkElement ?? throw NavigationContentServiceException.Prebuilt.ServiceNotResolved(_content[key].view.ToString());
        return view;
    }

    /// <inheritdoc/>
    public IViewModel? GetContentViewModel(string key)
    {
        if (key == null)
            throw NavigationContentServiceException.Prebuilt.ArgumentNullException(nameof(key));

        if (!KeyRegistered(key))
            throw NavigationContentServiceException.Prebuilt.ContentKeyNotRegistered(key);

        IViewModel? viewModel = null;

        if (_content[key].viewModel != null)
        {
            viewModel = _serviceProvider.GetService(_content[key].viewModel!) as IViewModel;

            if (viewModel == null)
                throw NavigationContentServiceException.Prebuilt.ServiceNotResolved(_content[key].viewModel!.ToString());
        }

        return viewModel;
    }
}