﻿using Microsoft.Extensions.Hosting;
using WpfNavigation.Example.Constants;
using WpfNavigation.Example.Views;
using WpfNavigation.Interfaces.Services;

namespace WpfNavigation.Example.Services;

/// <summary>
/// AppHostService class.
/// </summary>
/// <param name="navigationService">Navigation service.</param>
public class AppHostService(IServiceProvider serviceProvider, IRegionNavigationService navigationRegionService) : IHostedService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IRegionNavigationService _navigationRegionService = navigationRegionService;
    private bool _isInitialized;

    /// <summary>
    /// Starts the service asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Initialize();
        _isInitialized = true;
    }

    /// <summary>
    /// Stops the service asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    /// <summary>
    /// Asynchronous initialization procedure.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task Initialize()
    {
        if (!_isInitialized)
        {
            var mainWnd = _serviceProvider.GetService(typeof(MainWindow));

            if (mainWnd == null)
                throw new NullReferenceException(nameof(mainWnd));

            ((MainWindow)mainWnd).Show();
            _navigationRegionService.Navigate(NavigationConstants.Regions.Region1, NavigationConstants.Content.Content1);
            _navigationRegionService.Navigate(NavigationConstants.Regions.Region2, NavigationConstants.Content.Content2);
            await Task.CompletedTask;
        }
    }
}