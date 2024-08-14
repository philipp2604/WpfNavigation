# WpfNavigation &middot; [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) &middot; [![build and test](https://github.com/philipp2604/WpfNavigation/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/philipp2604/WpfNavigation/actions/workflows/build-and-test.yml)


## Description 
<p>This library allows for simple adding of navigation regions to WPF applications.<br/>This means, you can navigate to different views (optionally with view models) in ContentControls.</p>


## Quick Start
**<p>I recommend having a look at the example project.</p>**

1. Prepare your existing view by adding a `ContentControl` and registering it to the `RegionNavigationService` with a specific region key.
    - Registration of regions can be done by calling `RegionNavigationService.RegisterRegion(_YourRegionKey_, _YourContentControl_)`
    - Or by using the `NavigationService.NavigationRegion="_YourRegionKey_"` property inside the ContentControl's xaml.
2. Create the views and view models that you want to navigate to inside your `ContentControl` and register them to the `RegionContentService` with a specific content key.
    - Views-only are registered by calling `RegionContentService.RegisterContent<_YourViewType_>(_YourContentKey_)`.
    - If you want to register a view and a view model, you can call `RegionContentService.RegisterContent<_YourViewType_, _YourViewModelType_>(_YourContentKey_)`.
3. **Done!** You can now navigate to your views by simply calling `RegionNavigationService.Navigate(_YourRegionKey_, _YourContentKey_)`.

**<p>Feel free to reach out, if help is needed.</p>**

## Example screenshots
<p>These screenshots show the example pages, included in the project.<br/>Navigation is possible via ShellWindow menu or via buttons on the pages.</p>

![MainPage](./Screenshots/Screenshot1.PNG)
<br/>
![MainPage](./Screenshots/Screenshot2.PNG)

## Ideas
* Add events/callbacks.
* Add support for more Controls (Frames, ...).
## Third Party Software / Packages
Please have a look at [THIRD-PARTY-LICENSES](./THIRD-PARTY-LICENSES.md) for all the awesome packages used in this template.

## License
This template is [MIT licensed](./LICENSE.txt).
