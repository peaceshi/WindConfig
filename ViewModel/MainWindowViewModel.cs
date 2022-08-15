using System.ComponentModel;

using Wind3Config.Model;

namespace Wind3Config.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private static bool _IsFullScreenMode = true;
    private static bool _IsWindowMode;
    private static bool _IsResolution_1 = true;
    private static bool _IsResolution_2;

    public MainWindowViewModel()
    {
        if (Wind3Registry.GetIsFullscreenValue() != 0)
        {
            _IsFullScreenMode = true;
            _IsWindowMode = false;
        }
        else
        {
            _IsFullScreenMode = false;
            _IsWindowMode = true;
        }
        if (Wind3Registry.GetResolutionValue() == 600)
        {
            _IsResolution_1 = true;
            _IsResolution_2 = false;
        }
        else
        {
            _IsResolution_1 = false;
            _IsResolution_2 = true;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsFullScreenMode
    {
        get { return _IsFullScreenMode; }
        set
        {
            _IsFullScreenMode = value;
            OnPropertyChanged(nameof(IsFullScreenMode));
        }
    }

    public bool IsWindowMode
    {
        get { return _IsWindowMode; }
        set
        {
            _IsWindowMode = value;
            OnPropertyChanged(nameof(IsWindowMode));
        }
    }

    public bool IsResolution_1
    {
        get { return _IsResolution_1; }
        set
        {
            _IsResolution_1 = value;
            OnPropertyChanged(nameof(IsResolution_1));
        }
    }

    public bool IsResolution_2
    {
        get { return _IsResolution_2; }
        set
        {
            _IsResolution_2 = value;
            OnPropertyChanged(nameof(IsResolution_2));
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
