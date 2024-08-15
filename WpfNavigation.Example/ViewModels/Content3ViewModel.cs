﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfNavigation.Interfaces.ViewModels;

namespace WpfNavigation.Example.ViewModels;


/// <summary>
/// This example view model implements the INavigationAware interface, which means it is being notified when it's navigated to/from.
/// </summary>
public class Content3ViewModel : IViewModel, INavigationAware
{
    /// <inheritdoc/>
    void INavigationAware.OnNavigatedTo(object? lastViewModel, object? parameters)
    {
    }

    /// <inheritdoc/>
    void INavigationAware.OnNavigatedFrom(object? nextViewModel, object? parameters)
    {
    }
}