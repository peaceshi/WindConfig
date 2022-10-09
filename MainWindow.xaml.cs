using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

using WindConfig.Model;

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
        WindRegistry.CreationWidth = 800;
        WindRegistry.CreationHeight = 600;
    }

    private void Resolution_RadioButton2_Checked(object sender, RoutedEventArgs e)
    {
        WindRegistry.CreationWidth = 1024;
        WindRegistry.CreationHeight = 768;
    }

    private void Window_WindowMode_Checked(object sender, RoutedEventArgs e)
    {
        WindRegistry.IsFullscreen = 0;
    }

    private void Window_FullScreenMode_Checked(object sender, RoutedEventArgs e)
    {
        WindRegistry.IsFullscreen = 1;
    }

    private void Start_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult CustomResolutionResult = MessageBoxResult.Cancel;
        if (CustomResolution.IsEnabled)
        {
            const string caption = "警告 (Warning)";
            const string message = "自定义分辨率可能产生预期之外的错误. 甚至损坏你的硬件.\nCustom resolutions can produce unexpected errors. Even damage your hardware.";
            const MessageBoxResult defaultResult = MessageBoxResult.Cancel;
            CustomResolutionResult = MessageBox.Show(message, caption, MessageBoxButton.OKCancel, MessageBoxImage.Warning, defaultResult);
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
            MessageBox.Show($"未找到 {ProcessName} \n Can not find {ProcessName}", "警告 (Warning)");
        }
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
        e.Handled = true;
    }
}
