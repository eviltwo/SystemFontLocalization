# SystemFontLocalization
 Auto generate fallback font asset by system font in runtime.
 
 ![SystemFont](https://github.com/eviltwo/SystemFontLocalization/assets/7721151/8e203dbf-ea21-445d-a4d9-b8484028bf6d)

# Dependency
When you import SystemFontLocalization with UPM, these packages are automatically imported.
- Text Mesh Pro `com.unity.textmeshpro 3.2.0-pre.6`
  - Required 3.2.0-pre.6 for function `TMP_FontAsset.CreateFontAsset(fontPath)`
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
- Recommend: Dynamic fonts cause diffs on git every time you build, so use static fonts instead.
  - Set the default language to "en".
  - Set the base font asset to "Static".
  - Generate a Font atlas with the character set set to "ASCII".
