using System.Windows;

using Wind3Config.Model;

namespace Wind3Config;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

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
}
