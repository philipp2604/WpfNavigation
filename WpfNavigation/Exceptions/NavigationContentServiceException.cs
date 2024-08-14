using WpfNavigation.Services;

namespace WpfNavigation.Exceptions;

/// <summary>
/// Exception used by the <see cref="NavigationContentService"/>.
/// </summary>
public class NavigationContentServiceException : Exception
{
    private string? _extraData;

    /// <summary>
    /// Creates a new instance of <see cref="NavigationContentServiceException"/>.
    /// </summary>
    public NavigationContentServiceException() : base()
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="NavigationContentServiceException"/>.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public NavigationContentServiceException(string message) : this(message, null)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="NavigationContentServiceException"/>.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="InnerException">Inner exception.</param>
    /// <param name="extraData">Extra data.</param>
    public NavigationContentServiceException(string message, Exception? InnerException, string? extraData = null) : base(message, InnerException)
    {
        _extraData = extraData;
    }

    /// <summary>
    /// Extra data about the exception.
    /// </summary>
    public string ExtraData { get => _extraData ??= string.Empty; set => _extraData = value; }

    /// <summary>
    /// Prebuilt <see cref="NavigationContentServiceException"/>s for common use cases.
    /// </summary>
    public static class Prebuilt
    {
        /// <summary>
        /// Generates a new <see cref="NavigationContentServiceException"/> for arguments being null.
        /// </summary>
        /// <param name="argumentName">Name of the argument.</param>
        /// <returns>A <see cref="NavigationContentServiceException"/></returns>
        public static NavigationContentServiceException ArgumentNullException(string? argumentName = null)
        {
            return new NavigationContentServiceException("Argument cannot be null.", new ArgumentNullException(argumentName), argumentName);
        }

        /// <summary>
        /// Generates a new <see cref="NavigationContentServiceException"/> for the content key requested not being registered.
        /// </summary>
        /// <param name="key">The content key.</param>
        /// <returns>A <see cref="NavigationContentServiceException"/></returns>
        public static NavigationContentServiceException ContentKeyNotRegistered(string? key = null)
        {
            return new NavigationContentServiceException("Content key is not registered.", new KeyNotFoundException("Key value not found in collection."), key);
        }

        /// <summary>
        /// Generates a new <see cref="NavigationContentServiceException"/> for the content key already being registered while trying to add it again.
        /// </summary>
        /// <param name="key">The content key.</param>
        /// <returns>A <see cref="NavigationContentServiceException"/></returns>
        public static NavigationContentServiceException ContentKeyAlreadyRegistered(string? key = null)
        {
            return new NavigationContentServiceException("Content key is already registered.", new ArgumentException("Key value already exists in collection."), key);
        }

        /// <summary>
        /// Generates a new <see cref="NavigationContentServiceException"/> for a requested service not being able to be resolved.
        /// </summary>
        /// <param name="name">The service's name.</param>
        /// <returns>A <see cref="NavigationContentServiceException"/></returns>
        public static NavigationContentServiceException ServiceNotResolved(string? name = null)
        {
            return new NavigationContentServiceException("Requested service could not be resolved.", new System.InvalidOperationException("Requested service could not be resolved"), name);
        }
    }
}