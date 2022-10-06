using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Windows.Win32;
using Windows.Win32.Foundation;

using static Windows.Win32.UI.WindowsAndMessaging.SET_WINDOW_POS_FLAGS;

namespace WindConfig.Model;

public static class Wind
{
    public const string Wind3ProcessName = "WIND3.EXE";
    public const string Wind4ProcessName = "WIND4.EXE";
    public const string Wind3RegistryKeyName = "Wind3";
    public const string Wind4RegistryKeyName = "Wind4";
    public const string Wind3ConfigTitleName = "风色幻想3 配置向导";
    public const string Wind4ConfigTitleName = "风色幻想4 配置向导";

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
}
