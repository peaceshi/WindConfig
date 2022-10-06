using Microsoft.Win32;

namespace WindConfig.Model;

public static class WindRegistry
{
    private const string HKCU = "HKEY_CURRENT_USER";

    //211
    private const int defaultVersion = 0xd3;

    //800
    private const int defaultCreationWidth = 0x320;

    //600
    private const int defaultCreationHeight = 0x258;

    //1
    private const int defaultFullscreenValue = 0x1;

    private static string _WindKey = HKCU + "\\" + Wind.Wind3RegistryKeyName;

    public static int CreationHeight
    {
        get => (int)(Registry.GetValue(_WindKey, Key.CreationHeight, defaultValue: defaultCreationHeight) ?? defaultCreationHeight);
        set => Registry.SetValue(_WindKey, Key.CreationHeight, value, RegistryValueKind.DWord);
    }

    public static int CreationWidth
    {
        get => (int)(Registry.GetValue(_WindKey, Key.CreationWidth, defaultValue: defaultCreationWidth) ?? defaultCreationWidth);
        set => Registry.SetValue(_WindKey, Key.CreationWidth, value, RegistryValueKind.DWord);
    }

    public static string WindKey
    {
        get => _WindKey;
        set => _WindKey = HKCU + "\\" + value;
    }

    public static int Version
    {
        get => (int)(Registry.GetValue(_WindKey, Key.Version, defaultValue: defaultVersion) ?? defaultVersion);
        set => Registry.SetValue(_WindKey, Key.Version, value, RegistryValueKind.DWord);
    }

    public static int IsFullscreen
    {
        get => (int)(Registry.GetValue(_WindKey, Key.IsFullscreen, defaultValue: defaultFullscreenValue) ?? defaultFullscreenValue);
        set => Registry.SetValue(_WindKey, Key.IsFullscreen, value, RegistryValueKind.DWord);
    }

    public static string Path
    {
        get => (string)(Registry.GetValue(_WindKey, Key.Path, defaultValue: string.Empty) ?? string.Empty);
        set => Registry.SetValue(_WindKey, Key.Path, value, RegistryValueKind.String);
    }

    private static class Key
    {
        public const string CreationHeight = "CreationHeight";
        public const string CreationWidth = "CreationWidth";
        public const string Version = "Version";
        public const string Path = "Path";
        public const string AdapterFormat = "AdapterFormat";
        public const string RefreshRate = "RefreshRate";
        public const string TextureFormat = "TextureFormat";
        public const string IsFullscreen = "IsFullscreen";
        public const string IsPersonStrokeEnable = "IsPersonStrokeEnable";
    }
}
