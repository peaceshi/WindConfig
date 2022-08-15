using Microsoft.Win32;

namespace Wind3Config.Model;

public static class Wind3Registry
{
    // The name of the key must include a valid root.
    private const string userRoot = "HKEY_CURRENT_USER";

    private const string subKey = "Wind3";

    private const string Wind3 = userRoot + "\\" + subKey;

    public static int GetIsFullscreenValue() => (int)(Registry.GetValue(Wind3, Key.IsFullscreen, defaultValue: 0x1) ?? 0x1);

    public static void SetIsFullscreenValue(int value)
    {
        Registry.SetValue(Wind3, Key.IsFullscreen, value, RegistryValueKind.DWord);
        Registry.SetValue(Wind3, Key.Version, 0xd3, RegistryValueKind.DWord);
    }

    public static void SetResolutionValue(int width, int height)
    {
        Registry.SetValue(Wind3, Key.CreationWidth, width, RegistryValueKind.DWord);
        Registry.SetValue(Wind3, Key.CreationHeight, height, RegistryValueKind.DWord);
        Registry.SetValue(Wind3, Key.Version, 0xd3, RegistryValueKind.DWord);
    }

    public static int GetResolutionValue() => (int)(Registry.GetValue(Wind3, Key.CreationHeight, defaultValue: 0x258) ?? 0x258);

    private static class Key
    {
        public const string CreationHeight = "CreationHeight";
        public const string CreationWidth = "CreationWidth";
        public const string Version = "Version";
        public const string AdapterFormat = "AdapterFormat";
        public const string RefreshRate = "RefreshRate";
        public const string TextureFormat = "TextureFormat";
        public const string IsFullscreen = "IsFullscreen";
        public const string IsPersonStrokeEnable = "IsPersonStrokeEnable";
    }
}
