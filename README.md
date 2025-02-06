# SystemFontLocalization
 Auto generate fallback font asset by system font in runtime.
 
 ![SystemFont](https://github.com/eviltwo/SystemFontLocalization/assets/7721151/8e203dbf-ea21-445d-a4d9-b8484028bf6d)

 Verified working on: Unity 2022.3.39, Unity 6

# Dependency
When you import SystemFontLocalization with UPM, these packages are automatically imported.
- Text Mesh Pro `com.unity.textmeshpro 3.2.0-pre.6`
  - Required 3.2.0-pre.6 for function `TMP_FontAsset.CreateFontAsset(fontPath)`
  - Since Unity 6 includes an appropriate version of TMP, manual import is not necessary.
- Localization `com.unity.localization`

# Install with UPM
```
https://github.com/eviltwo/SystemFontLocalization.git?path=src/SystemFontLocalization/Assets/SystemFontLocalization
```

# Getting started
- Localize the text using the [Localization package](https://docs.unity3d.com/Packages/com.unity.localization@1.3).
- Create empty GameObject and attach SystemFontReplacer component to it.
- Set font asset and font name list in SystemFontReplacer component.
- When you play the game, the font assets will be swapped to match the localization language.

# Recommend settings
- Use static fonts. Dynamic fonts cause diffs on git every time you build.
  - Set the default language to "en".
  - Set the base font asset to "Static".
  - Generate a Font atlas with the character set set to "ASCII".
- Disable "kern" in text. Prevents the problem of Cyrillic characters being line breaks.
- Setup default TextMeshPro settings in ProjectSettings.

# Support My Work
As a solo developer, your financial support would be greatly appreciated and helps me continue working on this project.
- [Asset Store](https://assetstore.unity.com/publishers/12117)
- [Steam](https://store.steampowered.com/curator/45066588)
- [GitHub Sponsors](https://github.com/sponsors/eviltwo)
