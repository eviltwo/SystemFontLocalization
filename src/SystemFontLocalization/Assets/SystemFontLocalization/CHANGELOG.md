# Changelog

## [0.7.4] - Develop
### Fixed
- Added start and end characters to the regex in DefaultSystemFontNameList.asset.  
  - This fixes the issue where unintended Chinese fonts were being selected.

## [0.7.3] - 2024-10-31
### Added
- Added Japanese font (Yu Gothic)
### Fixed
- Fixed a bug where characters would disappear when switching to a different language while displaying the same string in the same font.
- Fixed an issue where font replacement was skipped in the built game.
- Fixed the issue where the text was not updating when the same text exists in multiple languages. Force refresh the canvas.
- Used FindObjectsByType() instead of FindObjectOfType().

## [0.7.2] - 2024-07-15
### Added
- Added Korean font.

## [0.7.1] - 2024-05-13
### Fixed
- Fixed an issue where old fonts remained.

## [0.7.0] - 2024-05-13
### Change
- Support regex for language code and font name. Format of SystemFontNameList has been updated.

## [0.6.0] - 2024-03-25
### Fixed
- Move internal process to static class.
  - Scene transition supported.

## [0.5.0] - 2024-02-21
### Added
- Initial assets
