using CommunityToolkit.Mvvm.Input;
using WpfNavigation.Interfaces.Services;
using WpfNavigation.Interfaces.ViewModels;

namespace WpfNavigation.Example
{
    public class Content1ViewModel(IRegionNavigationService navigationRegionService) : IViewModel
    {
        private readonly IRegionNavigationService _navigationRegionService = navigationRegionService;
        private RelayCommand? _switchCmd;
        private bool _state;

        public RelayCommand SwitchCmd => _switchCmd ??= new RelayCommand(() =>
        {
            if (_state)
            {
                _navigationRegionService.Navigate("Control2", "Content2");
                _state = false;
            }
            else
            {
                _navigationRegionService.Navigate("Control2", "Content3");
                _state = true;
            }
        });
    }
}