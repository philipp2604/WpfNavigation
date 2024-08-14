using System;
using System.Windows;
using System.Windows.Controls;
using WpfNavigation.Exceptions;
using WpfNavigation.Interfaces.ViewModels;
using WpfNavigation.Services;

namespace WpfNavigation.Test.Services;

public class NavigationContentServiceTests
{
    [Fact]
    public void KeyRegistered_KeyIsRegistered_ReturnsTrue()
    {
        //Arrange
        var serviceProvider = new Moq.Mock<IServiceProvider>();
        var key = "testKey";

        var navigationContentService = new NavigationContentService(serviceProvider.Object);
        navigationContentService.RegisterContent<Page>(key);


        //Act
        var isRegistered = navigationContentService.KeyRegistered(key);


        //Assert
        Assert.True(isRegistered);
    }

    [Fact]
    public void KeyRegistered_KeyIsNotRegistered_ReturnsFalse()
    {
        //Arrange
        var serviceProvider = new Moq.Mock<IServiceProvider>();
        var key = "testKey";

        var navigationContentService = new NavigationContentService(serviceProvider.Object);


        //Act
        var isRegistered = navigationContentService.KeyRegistered(key);


        //Assert
        Assert.False(isRegistered);
    }

    [Fact]
    public void KeyRegistered_KeyIsNull_Throws()
    {
        //Arrange
        var serviceProvider = new Moq.Mock<IServiceProvider>();
        string? key = null;

        var navigationContentService = new NavigationContentService(serviceProvider.Object);


        //Act
        Assert.Throws<NavigationContentServiceException>(() => navigationContentService.KeyRegistered(key!));
    }

    [Fact]
    public void RegisterContent_ViewOnly_Successful()
    {
        //Arrange
        var serviceProvider = new Moq.Mock<IServiceProvider>();
        var key = "testKey";

        var navigationContentService = new NavigationContentService(serviceProvider.Object);


        //Act
        navigationContentService.RegisterContent<Page>(key);
    }

    [Fact]
    public void RegisterContent_ViewAndViewModel_Successful()
    {
        //Arrange
        var serviceProvider = new Moq.Mock<IServiceProvider>();
        var key = "testKey";

        var navigationContentService = new NavigationContentService(serviceProvider.Object);


        //Act
        navigationContentService.RegisterContent<Page, IViewModel>(key);
    }

    [Fact]
    public void RegisterContent_KeyIsNull_Throws()
    {
        //Arrange
        var serviceProvider = new Moq.Mock<IServiceProvider>();
        string? key = null;

        var navigationContentService = new NavigationContentService(serviceProvider.Object);


        //Act & Assert
        Assert.Throws<NavigationContentServiceException>(() => navigationContentService.RegisterContent<Page, IViewModel>(key!));
    }

    [Fact]
    public void RegisterContent_KeyIsAlreadyRegistered_Throws()
    {
        //Arrange
        var serviceProvider = new Moq.Mock<IServiceProvider>();
        var key = "testKey";

        var navigationContentService = new NavigationContentService(serviceProvider.Object);
        navigationContentService.RegisterContent<Page, IViewModel>(key);


        //Act & Assert
        Assert.Throws<NavigationContentServiceException>(() => navigationContentService.RegisterContent<Page, IViewModel>(key));
    }

    [StaFact]
    public void GetContentView_Successful()
    {
        //Arrange
        var key = "testKey";

        var serviceProvider = new Moq.Mock<IServiceProvider>();
        serviceProvider
            .Setup(x => x.GetService(typeof(Page)))
            .Returns(new Page());


        var navigationContentService = new NavigationContentService(serviceProvider.Object);
        navigationContentService.RegisterContent<Page>(key);


        //Act
        var view = navigationContentService.GetContentView(key);

        //Assert
        Assert.NotNull(view);
        Assert.IsType<Page>(view);
    }

    [StaFact]
    public void GetContentView_KeyIsNull_Throws()
    {
        //Arrange
        string? key = null;

        var serviceProvider = new Moq.Mock<IServiceProvider>();

        var navigationContentService = new NavigationContentService(serviceProvider.Object);


        //Act & Assert
        Assert.Throws<NavigationContentServiceException>(() => navigationContentService.GetContentView(key!));
    }

    [StaFact]
    public void GetContentView_KeyNotRegistered_Throws()
    {
        //Arrange
        var key = "testKey";

        var serviceProvider = new Moq.Mock<IServiceProvider>();

        var navigationContentService = new NavigationContentService(serviceProvider.Object);


        //Act & Assert
        Assert.Throws<NavigationContentServiceException>(() => navigationContentService.GetContentView(key));
    }

    [StaFact]
    public void GetContentView_ServiceNotResolved_Throws()
    {
        //Arrange
        var key = "testKey";

        var serviceProvider = new Moq.Mock<IServiceProvider>();
        serviceProvider
            .Setup(x => x.GetService(typeof(Page)))
            .Returns(null!);


        var navigationContentService = new NavigationContentService(serviceProvider.Object);
        navigationContentService.RegisterContent<Page>(key);


        //Act & Assert
        Assert.Throws<NavigationContentServiceException>(() => navigationContentService.GetContentView(key));
    }

    [StaFact]
    public void GetContentViewModel_Successful()
    {
        //Arrange
        var key = "testKey";
        var viewModelMock = new Moq.Mock<IViewModel>();

        var serviceProvider = new Moq.Mock<IServiceProvider>();
        serviceProvider
            .Setup(x => x.GetService(typeof(IViewModel)))
            .Returns(viewModelMock.Object);


        var navigationContentService = new NavigationContentService(serviceProvider.Object);
        navigationContentService.RegisterContent<Page, IViewModel>(key);


        //Act
        var viewModel = navigationContentService.GetContentViewModel(key);

        //Assert
        Assert.NotNull(viewModel);
        Assert.IsAssignableFrom<IViewModel>(viewModel);
        Assert.Equal(viewModel, viewModelMock.Object);
    }

    [StaFact]
    public void GetContentViewModel_NoViewModelRegistered_Successful()
    {
        //Arrange
        var key = "testKey";

        var serviceProvider = new Moq.Mock<IServiceProvider>();


        var navigationContentService = new NavigationContentService(serviceProvider.Object);
        navigationContentService.RegisterContent<Page>(key);


        //Act
        var viewModel = navigationContentService.GetContentViewModel(key);


        //Assert
        Assert.Null(viewModel);
    }

    [StaFact]
    public void GetContentViewModel_KeyIsNull_Throws()
    {
        //Arrange
        string? key = null;

        var serviceProvider = new Moq.Mock<IServiceProvider>();

        var navigationContentService = new NavigationContentService(serviceProvider.Object);


        //Act & Assert
        Assert.Throws<NavigationContentServiceException>(() => navigationContentService.GetContentViewModel(key!));
    }

    [StaFact]
    public void GetContentViewModel_KeyNotRegistered_Throws()
    {
        //Arrange
        var key = "testKey";

        var serviceProvider = new Moq.Mock<IServiceProvider>();

        var navigationContentService = new NavigationContentService(serviceProvider.Object);


        //Act & Assert
        Assert.Throws<NavigationContentServiceException>(() => navigationContentService.GetContentViewModel(key));
    }

    [StaFact]
    public void GetContentViewModel_ServiceNotResolved_Throws()
    {
        //Arrange
        var key = "testKey";

        var serviceProvider = new Moq.Mock<IServiceProvider>();
        serviceProvider
            .Setup(x => x.GetService(typeof(IViewModel)))
            .Returns(null!);


        var navigationContentService = new NavigationContentService(serviceProvider.Object);
        navigationContentService.RegisterContent<Page, IViewModel>(key);


        //Act & Assert
        Assert.Throws<NavigationContentServiceException>(() => navigationContentService.GetContentViewModel(key));
    }
}