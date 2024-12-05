namespace WpfNavigation.Interfaces.ViewModels;

//Interface for navigation aware view models.
public interface INavigationAware
{
    /// <summary>
    /// Callback, called when navigating to this view model.
    /// </summary>
    /// <param name="lastViewModel">The last/current view model.</param>
    /// <param name="sender">Sender that invoked the navigation.</param>
    /// <param name="parameters">Optional parameters.</param>
    public void OnNavigatedTo(object? lastViewModel, object? sender, object? parameters);

    /// <summary>
    /// Asynchronous callback, called when navigating to this view model.
    /// </summary>
    /// <param name="lastViewModel">The last/current view model.</param>
    /// <param name="sender">Sender that invoked the navigation.</param>
    /// <param name="parameters">Optional parameters.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    public Task OnNavigatedToAsync(object? lastViewModel, object? sender, object? parameters);

    /// <summary>
    /// Callback, called when navigating away from this view model.
    /// </summary>
    /// <param name="nextViewModel">The next view model.</param>
    /// <param name="parameters">Optional parameters.</param>
    public void OnNavigatedFrom(object? nextViewModel, object? parameters);

    /// <summary>
    /// Asynchronous callback, called when navigating away from this view model.
    /// </summary>
    /// <param name="nextViewModel">The next view model.</param>
    /// <param name="parameters">Optional parameters.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    public Task OnNavigatedFromAsync(object? nextViewModel, object? parameters);
}