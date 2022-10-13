using System.IO;
using System.Windows;

using WindConfig.Model;

namespace WindConfig;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        string Title = Wind.DefaultConfigTitleName;
        string ProcessName = string.Empty;
        if (File.Exists(Wind.Wind3ProcessName))
        {
            Title = Wind.Wind3ConfigTitleName;
            ProcessName = Wind.Wind3ProcessName;
            WindRegistry.WindKey = Wind.Wind3RegistryKeyName;
            WindRegistry.Version = Wind.Wind3Version;
        }
        else if (File.Exists(Wind.Wind4ProcessName))
        {
            Title = Wind.Wind4ConfigTitleName;
            ProcessName = Wind.Wind4ProcessName;
            WindRegistry.WindKey = Wind.Wind4RegistryKeyName;
            WindRegistry.Version = Wind.Wind4Version;
        }
        //else
        //{
        //    Current.Shutdown();
        //}
        MainWindow = new MainWindow(Title, ProcessName);
        MainWindow.Show();
    }
}
