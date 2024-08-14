using System.Windows.Controls;
using WpfNavigation.Exceptions;
using WpfNavigation.Interfaces.Services;
using WpfNavigation.Interfaces.ViewModels;
using WpfNavigation.Models;
using WpfNavigation.Services;

namespace WpfNavigation.Test.Services;

public class RegionNavigationServiceTests
{
    [WpfFact]
    public void KeyRegistered_KeyIsRegistered_ReturnsTrue()
    {
        //Arrange
        var key = "testKey";
        var region = new NavigationRegion(key, new ContentControl());
        RegionNavigationService.ClearRegions();
        RegionNavigationService.RegisterRegion(region);

        //Act
        var isRegistered = RegionNavigationService.KeyRegistered(key);

        //Assert
        Assert.True(isRegistered);
    }

    [Fact]
    public void KeyRegistered_KeyIsNotRegistered_ReturnsFalse()
    {
        //Arrange
        var key = "testKey";
        RegionNavigationService.ClearRegions();

        //Act
        var isRegistered = RegionNavigationService.KeyRegistered(key);

        //Assert
        Assert.False(isRegistered);
    }

    [Fact]
    public void KeyRegistered_KeyIsNull_Throws()
    {
        //Arrange
        string? key = null;

        //Act & Assert
        Assert.Throws<RegionNavigationServiceException>(() => RegionNavigationService.KeyRegistered(key!));
    }

    [WpfFact]
    public void Static_RegisterRegion_Successful()
    {
        //Arrange
        var key = "testKey";
        var region = new NavigationRegion(key, new ContentControl());
        RegionNavigationService.ClearRegions();

        //Act
        RegionNavigationService.RegisterRegion(region);
    }

    [WpfFact]
    public void Static_RegisterRegion_KeyIsNull_Throws()
    {
        //Arrange
        string? key = null;
        var region = new NavigationRegion(key!, new ContentControl());

        //Act & Assert
        Assert.Throws<RegionNavigationServiceException>(() => RegionNavigationService.RegisterRegion(region));
    }

    [Fact]
    public void Static_RegisterRegion_ControlIsNull_Throws()
    {
        //Arrange
        var key = "testKey";
        var region = new NavigationRegion(key!, null!);

        //Act & Assert
        Assert.Throws<RegionNavigationServiceException>(() => RegionNavigationService.RegisterRegion(region));
    }

    [WpfFact]
    public void Static_RegisterRegion_KeyIsAlreadyRegistered_Throws()
    {
        //Arrange
        var key = "testKey";
        var region = new NavigationRegion(key!, new ContentControl());
        RegionNavigationService.RegisterRegion(region);

        //Act & Assert
        Assert.Throws<RegionNavigationServiceException>(() => RegionNavigationService.RegisterRegion(region));
    }

    [WpfFact]
    public void Static_GetRegion_Successful()
    {
        //Arrange
        var key = "testKey";
        var region = new NavigationRegion(key!, new ContentControl());
        RegionNavigationService.ClearRegions();
        RegionNavigationService.RegisterRegion(region);

        //Act
        var regionReturned = RegionNavigationService.GetRegion(key);

        //Assert
        Assert.NotNull(regionReturned);
        Assert.Equal(region, regionReturned);
    }

    [WpfFact]
    public void Static_GetRegion_KeyIsNull_Throws()
    {
        //Arrange
        string? key = null;

        //Act & Assert
        Assert.Throws<RegionNavigationServiceException>(() => RegionNavigationService.GetRegion(key!));
    }

    [WpfFact]
    public void Static_GetRegion_KeyIsNotRegistered_Throws()
    {
        //Arrange
        var key = "testKey";
        RegionNavigationService.ClearRegions();

        //Act & Assert
        Assert.Throws<RegionNavigationServiceException>(() => RegionNavigationService.GetRegion(key));
    }

    [WpfFact]
    public void Navigate_ViewOnly_Successful()
    {
        //Arrange
        var key = "testKey";
        var view = new Page();
        var control = new ContentControl();
        var region = new NavigationRegion(key, control);

        var navigationContentService = new Moq.Mock<INavigationContentService>();
        navigationContentService
            .Setup(x => x.GetContentView(key))
            .Returns(view);

        RegionNavigationService.ClearRegions();
        RegionNavigationService.RegisterRegion(region);

        var regionNavigationService = new RegionNavigationService(navigationContentService.Object);

        //Act
        regionNavigationService.Navigate(key, key);

        //Assert
        Assert.NotNull(control.Content);
        Assert.Equal(control.Content, view);
    }

    [WpfFact]
    public void Navigate_ViewAndViewModel_Successful()
    {
        //Arrange
        var key = "testKey";
        var view = new Page();
        var viewModel = new Moq.Mock<IViewModel>();
        var control = new ContentControl();
        var region = new NavigationRegion(key, control);

        var navigationContentService = new Moq.Mock<INavigationContentService>();
        navigationContentService
            .Setup(x => x.GetContentView(key))
            .Returns(view);
        navigationContentService
            .Setup(x => x.GetContentViewModel(key))
            .Returns(viewModel.Object);

        RegionNavigationService.ClearRegions();
        RegionNavigationService.RegisterRegion(region);

        var regionNavigationService = new RegionNavigationService(navigationContentService.Object);

        //Act
        regionNavigationService.Navigate(key, key);

        //Assert
        Assert.NotNull(control.Content);
        Assert.Equal(control.Content, view);
        Assert.NotNull(((Page)control.Content).DataContext);
        Assert.Equal(((Page)control.Content).DataContext, viewModel.Object);
    }

    [WpfFact]
    public void Navigate_RegionKeyIsNull_Throws()
    {
        //Arrange
        string? regionKey = null;
        var contentKey = "testKey";

        var navigationContentService = new Moq.Mock<INavigationContentService>();

        var regionNavigationService = new RegionNavigationService(navigationContentService.Object);

        //Act & Assert
        Assert.Throws<RegionNavigationServiceException>(() => regionNavigationService.Navigate(regionKey!, contentKey));
    }

    [WpfFact]
    public void Navigate_ContentKeyIsNull_Throws()
    {
        //Arrange
        var regionKey = "testKey";
        string? contentKey = null;

        var navigationContentService = new Moq.Mock<INavigationContentService>();

        var regionNavigationService = new RegionNavigationService(navigationContentService.Object);

        //Act & Assert
        Assert.Throws<RegionNavigationServiceException>(() => regionNavigationService.Navigate(regionKey, contentKey!));
    }

    [WpfFact]
    public void Navigate_RegionKeyIsNotRegistered_Throws()
    {
        //Arrange
        var regionKey = "testKey";
        var contentKey = "testKey";

        var navigationContentService = new Moq.Mock<INavigationContentService>();

        var regionNavigationService = new RegionNavigationService(navigationContentService.Object);
        RegionNavigationService.ClearRegions();

        //Act & Assert
        Assert.Throws<RegionNavigationServiceException>(() => regionNavigationService.Navigate(regionKey, contentKey!));
    }
}