using System.ComponentModel;

public class MainViewModel : INotifyPropertyChanged
{
    private string _name;
    private string _initial;

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                UpdateInitial();
            }
        }
    }

    public string Initial
    {
        get => _initial;
        private set
        {
            if (_initial != value)
            {
                _initial = value;
                OnPropertyChanged(nameof(Initial));
            }
        }
    }

    private void UpdateInitial()
    {
        Initial = string.IsNullOrEmpty(Name) ? string.Empty : Name[0].ToString().ToUpper();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
