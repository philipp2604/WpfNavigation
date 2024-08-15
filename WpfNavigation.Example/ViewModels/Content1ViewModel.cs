using CommunityToolkit.Mvvm.Input;
using WpfNavigation.Example.Constants;
using WpfNavigation.Interfaces.Services;
using WpfNavigation.Interfaces.ViewModels;

namespace WpfNavigation.Example.ViewModels;

/// <summary>
/// Example view model which controls navigation of 'Region2'.
/// </summary>
public class Content1ViewModel(IRegionNavigationService navigationRegionService) : IViewModel
{
    private readonly IRegionNavigationService _navigationRegionService = navigationRegionService;
    private RelayCommand? _switchCmd;
    private bool _state;

    public RelayCommand SwitchCmd => _switchCmd ??= new RelayCommand(() =>
    {
        if (_state)
        {
            _navigationRegionService.Navigate(NavigationConstants.Regions.Region2, NavigationConstants.Content.Content2, "Bye, Content3!");
            _state = false;
        }
        else
        {
            _navigationRegionService.Navigate(NavigationConstants.Regions.Region2, NavigationConstants.Content.Content3, "Hello, Content3!");
            _state = true;
        }
    });
}