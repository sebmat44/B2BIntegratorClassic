using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace B2BIntegratorClassic.App.Core;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    //protected czyli widoczna tylko dla tej klasy i klas dziedziczacych
    // virtual czyli klasy dziedziczace moga modyfikowac (override)
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        // Jeśli ktokolwiek (WPF) nasłuchuje (PropertyChanged != null), 
        // to krzyczymy, wysyłając nazwę właściwości, która uległa zmianie.
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    //metoda pomocnicza
    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {
        // Krok A: Sprawdzamy, czy nowa wartość różni się od starej. 
        // Jeśli są takie same, wychodzimy natychmiast - nie ma sensu odświeżać UI.
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return false;
        }

        // Krok B: Nadpisujemy starą wartość nową.
        storage = value;

        // Krok C: Informujemy UI o zmianie.
        OnPropertyChanged(propertyName);

        return true;
    }
}

