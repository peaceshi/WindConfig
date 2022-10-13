using System;
using System.IO;
using System.Windows;

namespace WindConfig.View;

/// <summary>
/// FAQ.xaml 的交互逻辑
/// </summary>
public partial class FAQ : Window
{
    private readonly Stream source = Application.GetResourceStream(new Uri("pack://application:,,,/Resources/FAQ.html")).Stream;

    public FAQ()
    {
        InitializeComponent();
        WebBrowser.NavigateToStream(source);
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
