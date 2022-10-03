using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

using Wind3Config.Model;

namespace Wind3Config;

public partial class MainWindow : Window
{
    private const string ProcessName = Wind.Wind3ProcessName;

    public MainWindow()
    {
        WindRegistry.WindKey = Wind.Wind3RegistryKeyName;
        WindRegistry.Version = 211;
        InitializeComponent();
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
        if (File.Exists(ProcessName))
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
        }
        else
        {
            MessageBox.Show($"未找到 {ProcessName} \n Can not find {ProcessName}", "警告 (Warning)");
        }
        App.Current.Shutdown();
    }

    private void Resolution_RadioButton3_Checked(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("自定义分辨率可能产生预期之外的错误. 甚至损坏你的硬件.\nCustom resolutions can produce unexpected errors. Even damage your hardware.", "警告 (Warning)");
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
        e.Handled = true;
    }
}
