using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

using WindConfig.Model;
using WindConfig.View;

namespace WindConfig;

public partial class MainWindow : Window
{
    private readonly string ProcessName = string.Empty;

    public MainWindow(string title, string processName)
    {
        InitializeComponent();
        Title = title;
        ProcessName = processName;
    }

    private void Resolution_RadioButton1_Checked(object sender, RoutedEventArgs e)
    {
        if (ProcessName?.Length != 0)
        {
            WindRegistry.CreationWidth = 800;
            WindRegistry.CreationHeight = 600;
        }
    }

    private void Resolution_RadioButton2_Checked(object sender, RoutedEventArgs e)
    {
        if (ProcessName?.Length != 0)
        {
            WindRegistry.CreationWidth = 1024;
            WindRegistry.CreationHeight = 768;
        }
    }

    private void Window_WindowMode_Checked(object sender, RoutedEventArgs e)
    {
        if (ProcessName?.Length != 0)
        {
            WindRegistry.IsFullscreen = 0;
        }
    }

    private void Window_FullScreenMode_Checked(object sender, RoutedEventArgs e)
    {
        if (ProcessName?.Length != 0)
        {
            WindRegistry.IsFullscreen = 1;
        }
    }

    private void Start_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult CustomResolutionResult = MessageBoxResult.Cancel;
        if (CustomResolution.IsEnabled)
        {
            CustomResolutionResult = Wind.ShowCustomResolutionWarningMessage();
        }
        //1. File must exists.
        //2. File exists with IsCustomResolution == true. CustomResolutionResult == MessageBoxResult.OK must be IsCustomResolution == true.
        //3. File exists with !IsCustomResolution.
        if (File.Exists(ProcessName) && (CustomResolutionResult == MessageBoxResult.OK || !CustomResolution.IsEnabled))
        {
            WindRegistry.Path = $"{Path.GetDirectoryName(Path.GetFullPath(ProcessName))}\\";
            using Process WindProcess = new();
            {
                WindProcess.StartInfo.UseShellExecute = false;
                WindProcess.StartInfo.FileName = ProcessName;
                WindProcess.StartInfo.CreateNoWindow = false;
                WindProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                WindProcess.Start();
                if (WindRegistry.IsFullscreen == 0)
                {
                    Wind.SetWindowPosToCenter(WindProcess);
                }
            }
            Application.Current.Shutdown();
        }
        // if CustomResolutionResult is MessageBoxResult.Cancel, nothing needs to do.
        else if (!File.Exists(ProcessName))
        {
            Wind.ShowProcessNotFoundWarningMessage(ProcessName);
        }
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
        e.Handled = true;
    }

    private void Registry_Click(object sender, RoutedEventArgs e)
    {
        if (ProcessName?.Length == 0)
        {
            Wind.ShowProcessNotFoundWarningMessage(ProcessName);
        }
        else
        {
            WindRegistry.LastKey = WindRegistry.WindKey;
            using Process Regedit = new();
            {
                Regedit.StartInfo.FileName = "regedit";
                Regedit.StartInfo.UseShellExecute = true;
                Regedit.StartInfo.Verb = "runas";
                Regedit.Start();
            }
        }
    }

    private void FAQ_Click(object sender, RoutedEventArgs e)
    {
        Window FAQ = new FAQ()
        {
            Owner = this
        };

        FAQ.ShowDialog();
    }
}
