using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using WpfNavigation.Example.Constants;
using WpfNavigation.Example.Services;
using WpfNavigation.Example.ViewModels;
using WpfNavigation.Example.Views;
using WpfNavigation.Interfaces.Services;
using WpfNavigation.Services;

namespace WpfNavigation.Example;

public partial class App : Application
{
    private IHost? _host;

    /// <summary>
    /// Gets a service of type <c>T</c>.
    /// </summary>
    /// <typeparam name="T">Service type to return.</typeparam>
    /// <returns>A service of type T?, returns null if service could not be resolved.</returns>
    public T? GetService<T>()
    {
        return (T?)_host?.Services.GetService(typeof(T));
    }

    /// <summary>
    /// Creates a new instance of the App class.
    /// </summary>
    public App()
    { }

    /// <summary>
    /// Asynchronous startup procedure.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"><see cref="StartupEventArgs"/></param>
    private async void OnStartup(object sender, StartupEventArgs e)
    {
        _host = Host.CreateDefaultBuilder(e.Args)
            .ConfigureServices(RegisterServices)
            .Build();

        RegisterNavigationContent();

        await _host.StartAsync();
    }

    private void OnUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        /// TODO: Handle exception
    }

    /// <summary>
    /// Registers the services.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="services"></param>
    private void RegisterServices(HostBuilderContext context, IServiceCollection services)
    {
        /// TODO: Register services

        services.AddHostedService<AppHostService>();

        services.AddSingleton<INavigationContentService, NavigationContentService>();
        services.AddSingleton<IRegionNavigationService, RegionNavigationService>();

        services.AddTransient<MainWindow>();
        services.AddTransient<Content1>();
        services.AddTransient<Content1ViewModel>();
        services.AddTransient<Content2>();
        services.AddTransient<Content3>();
        services.AddTransient<Content3ViewModel>();
    }

    /// <summary>
    /// Registers content for navigation regions.
    /// </summary>
    /// <exception cref="NullReferenceException"></exception>
    private void RegisterNavigationContent()
    {
        var navigationContentService = GetService<INavigationContentService>();

        if (navigationContentService == null)
            throw new NullReferenceException(nameof(navigationContentService));

        navigationContentService.RegisterContent<Content1, Content1ViewModel>(NavigationConstants.Content.Content1);
        navigationContentService.RegisterContent<Content2>(NavigationConstants.Content.Content2);
        navigationContentService.RegisterContent<Content3, Content3ViewModel>(NavigationConstants.Content.Content3);
    }

    /// <summary>
    /// Asynchronous exit procedure.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"><see cref="ExitEventArgs"/></param>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        if (_host != null)
        {
            await _host.StopAsync();
            _host.Dispose();
            _host = null;
        }
    }
}