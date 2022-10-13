using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

using Windows.Win32;
using Windows.Win32.Foundation;

using static Windows.Win32.UI.WindowsAndMessaging.SET_WINDOW_POS_FLAGS;

using MessageBox = System.Windows.MessageBox;
using Point = System.Drawing.Point;

namespace WindConfig.Model;

public static class Wind
{
    public const string Wind3ProcessName = "WIND3.EXE";
    public const string Wind4ProcessName = "WIND4.EXE";
    public const string Wind3RegistryKeyName = "Wind3";
    public const string Wind4RegistryKeyName = "Wind4";
    public const string Wind3ConfigTitleName = "风色幻想3 配置向导";
    public const string Wind4ConfigTitleName = "风色幻想4 配置向导";
    public const string DefaultConfigTitleName = "WindConfig";
    public const int Wind3Version = 211;
    public const int Wind4Version = 110;

    public static void SetWindowPosToCenter(Process process)
    {
        while (process.MainWindowHandle == IntPtr.Zero)
            process.Refresh();
        HWND hWnd = (HWND)process.MainWindowHandle;
        RECT rect = new();
        unsafe
        {
            PInvoke.GetWindowRect(hWnd, &rect);
        }
        Rectangle screen = Screen.FromHandle(hWnd).Bounds;
        Point point = new(screen.Left + (screen.Width / 2) - ((rect.right - rect.left) / 2), screen.Top + (screen.Height / 2) - ((rect.bottom - rect.top) / 2));
        PInvoke.SetWindowPos(hWnd, (HWND)IntPtr.Zero, point.X, point.Y, 0, 0, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);
    }

    public static MessageBoxResult ShowProcessNotFoundWarningMessage(string? ProcessName)
    {
        if (ProcessName?.Length == 0)
        {
            return MessageBox.Show("未找到游戏 \nCan not find game", "警告 (Warning)", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        else
        {
            return MessageBox.Show($"未找到 {ProcessName} \nCan not find {ProcessName}", "警告 (Warning)", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    public static MessageBoxResult ShowCustomResolutionWarningMessage()
    {
        const string caption = "警告 (Warning)";
        const string message = "自定义分辨率可能产生预期之外的错误. 甚至损坏你的硬件.\nCustom resolutions can produce unexpected errors. Even damage your hardware.";
        const MessageBoxResult defaultResult = MessageBoxResult.Cancel;
        return MessageBox.Show(message, caption, MessageBoxButton.OKCancel, MessageBoxImage.Warning, defaultResult);
    }
}
