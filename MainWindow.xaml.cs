using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;

using Wind3Config.Model;

using Application = System.Windows.Application;
using Point = System.Windows.Point;

namespace Wind3Config;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const int SWP_NOSIZE = 0x0001;

    private const int SWP_NOZORDER = 0x0004;

    private const int SWP_SHOWWINDOW = 0x0040;

    public MainWindow()
    {
        InitializeComponent();
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

    private void Resolution_RadioButton1_Checked(object sender, RoutedEventArgs e)
    {
        Wind3Registry.SetResolutionValue(800, 600);
    }

    private void Resolution_RadioButton2_Checked(object sender, RoutedEventArgs e)
    {
        Wind3Registry.SetResolutionValue(1024, 768);
    }

    private void Window_WindowMode_Checked(object sender, RoutedEventArgs e)
    {
        Wind3Registry.SetIsFullscreenValue(0);
    }

    private void Window_FullScreenMode_Checked(object sender, RoutedEventArgs e)
    {
        Wind3Registry.SetIsFullscreenValue(1);
    }

    private void Start_Click(object sender, RoutedEventArgs e)
    {
        using Process Wind3Process = new();
        {
            Wind3Process.StartInfo.UseShellExecute = false;
            Wind3Process.StartInfo.FileName = "WIND3.EXE";
            Wind3Process.StartInfo.CreateNoWindow = false;
            Wind3Process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            Wind3Process.Start();
            if (Wind3Registry.GetIsFullscreenValue() == 0)
            {
                while (Wind3Process.MainWindowHandle == IntPtr.Zero)
                    Wind3Process.Refresh();
                IntPtr handle = Wind3Process.MainWindowHandle;

                GetWindowRect(handle, out RECT rct);
                Rectangle screen = Screen.FromHandle(handle).Bounds;
                Point pt = new(screen.Left + (screen.Width / 2) - ((rct.Right - rct.Left) / 2), screen.Top + (screen.Height / 2) - ((rct.Bottom - rct.Top) / 2));
                SetWindowPos(handle, IntPtr.Zero, (int)pt.X, (int)pt.Y, 0, 0, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);
            }
        }
        Application.Current.Shutdown();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }
}
