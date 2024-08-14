namespace WpfNavigation.Exceptions;

/// <summary>
/// Exception used by the <see cref="IRegionNavigationService"/>.
/// </summary>
public class RegionNavigationServiceException : Exception
{
    private string? _extraData;

    /// <summary>
    /// Creates a new instance of <see cref="RegionNavigationServiceException"/>.
    /// </summary>
    public RegionNavigationServiceException() : base()
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="NavigationContentServiceException"/>.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public RegionNavigationServiceException(string message) : this(message, null)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="NavigationContentServiceException"/>.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="InnerException">Inner exception.</param>
    /// <param name="extraData">Extra data.</param>
    public RegionNavigationServiceException(string message, Exception? InnerException, string? extraData = null) : base(message, InnerException)
    {
        _extraData = extraData;
    }

    /// <summary>
    /// Extra data about the exception.
    /// </summary>
    public string ExtraData { get => _extraData ??= string.Empty; set => _extraData = value; }

    /// <summary>
    /// Prebuilt <see cref="RegionNavigationServiceException"/>s for common use cases.
    /// </summary>
    public static class Prebuilt
    {
        /// <summary>
        /// Generates a new <see cref="RegionNavigationServiceException"/> for arguments being null.
        /// </summary>
        /// <param name="argumentName">Name of the argument.</param>
        /// <returns>A <see cref="RegionNavigationServiceException"/></returns>
        public static RegionNavigationServiceException ArgumentNullException(string? argumentName = null)
        {
            return new RegionNavigationServiceException("Argument cannot be null.", new ArgumentNullException(argumentName), argumentName);
        }

        /// <summary>
        /// Generates a new <see cref="RegionNavigationServiceException"/> for the region key requested not being registered.
        /// </summary>
        /// <param name="key">The region key.</param>
        /// <returns>A <see cref="RegionNavigationServiceException"/></returns>
        public static RegionNavigationServiceException RegionKeyNotRegistered(string? key = null)
        {
            return new RegionNavigationServiceException("Region key is not registered.", new KeyNotFoundException("Key value not found in collection."), key);
        }

        /// <summary>
        /// Generates a new <see cref="RegionNavigationServiceException"/> for the region key already being registered while trying to add it again.
        /// </summary>
        /// <param name="key">The region key.</param>
        /// <returns>A <see cref="RegionNavigationServiceException"/></returns>
        public static RegionNavigationServiceException RegionKeyAlreadyRegistered(string? key = null)
        {
            return new RegionNavigationServiceException("Region key is already registered.", new ArgumentException("Key value already exists in collection."), key);
        }

        /// <summary>
        /// Generates a new <see cref="RegionNavigationServiceException"/> for a requested service not being able to be resolved.
        /// </summary>
        /// <param name="name">The service's name.</param>
        /// <returns>A <see cref="RegionNavigationServiceException"/></returns>
        public static RegionNavigationServiceException ServiceNotResolved(string? name = null)
        {
            return new RegionNavigationServiceException("Requested service could not be resolved.", new System.InvalidOperationException("Requested service could not be resolved"), name);
        }

        /// <summary>
        /// Generates a new <see cref="RegionNavigationServiceException"/> for registering a Control which is NOT a <see cref="ContentControl"/>.
        /// </summary>
        /// <returns>A <see cref="RegionNavigationServiceException"/></returns>
        public static RegionNavigationServiceException UnsupportedControlType()
        {
            return new RegionNavigationServiceException("The control type is not supported for registering a region.", new ArgumentException("Type not supported."));
        }
    }
}