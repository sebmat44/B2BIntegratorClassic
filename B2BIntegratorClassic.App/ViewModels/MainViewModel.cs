

using B2BIntegratorClassic.App.Core;
using System.Windows.Input;

namespace B2BIntegratorClassic.App.ViewModels;

public class MainViewModel : Core.ViewModelBase
{
    // 2. Ręcznie tworzymy ukryte pole (tzw. backing field)
    private string _statusMessage = string.Empty;

    // 3. Pełna definicja właściwości publicznej
    public string StatusMessage
    {
        get { return _statusMessage; }
        set
        {
            // 4. Używamy naszej metody pomocniczej. 
            // Dzięki [CallerMemberName], nie musimy pisać SetProperty(ref _statusMessage, value, "StatusMessage")
            SetProperty(ref _statusMessage, value);
        }
    }

    public ICommand ZmienTekstCommand { get; }

    public MainViewModel()
    {
        // Podpinamy prostą metodę pod komendę
        ZmienTekstCommand = new RelayCommand(ExecuteZmienTekst);
    }

    private void ExecuteZmienTekst(object? parameter)
    {
        // To wywoła ukryte OnPropertyChanged("StatusMessage")
        StatusMessage = "Klasyczny INotifyPropertyChanged działa idealnie!";
    }
}
