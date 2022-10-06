using System.ComponentModel;

using WindConfig.Model;

namespace WindConfig.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private static bool _IsFullScreenMode;
    private static bool _IsWindowMode;
    private static bool _IsResolution_1;
    private static bool _IsResolution_2;
    private static bool _IsCustomResolution;

    public MainWindowViewModel()
    {
        if (WindRegistry.IsFullscreen != 0)
        {
            _IsFullScreenMode = true;
        }
        else
        {
            _IsWindowMode = true;
        }
        if (WindRegistry.CreationWidth == 800 && WindRegistry.CreationHeight == 600)
        {
            _IsResolution_1 = true;
        }
        else if (WindRegistry.CreationWidth == 1024 && WindRegistry.CreationHeight == 768)
        {
            _IsResolution_2 = true;
        }
        else
        {
            _IsCustomResolution = true;
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

    public bool IsCustomResolution
    {
        get { return _IsCustomResolution; }
        set
        {
            _IsCustomResolution = value;
            OnPropertyChanged(nameof(IsCustomResolution));
        }
    }

    public int CustomCreationWidth
    {
        get { return WindRegistry.CreationWidth; }
        set
        {
            WindRegistry.CreationWidth = value;
            OnPropertyChanged(nameof(CustomCreationWidth));
        }
    }

    public int CustomCreationHeight
    {
        get { return WindRegistry.CreationHeight; }
        set
        {
            WindRegistry.CreationHeight = value;
            OnPropertyChanged(nameof(CustomCreationHeight));
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
