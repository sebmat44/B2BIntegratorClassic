
using System.Windows.Input;

namespace B2BIntegratorClassic.App.Core;

public class RelayCommand : ICommand
{
    // 1. Delegaty. To są "wskaźniki" na metody, które przekażemy z ViewModelu.
    // Action to metoda, która nic nie zwraca (void).
    private readonly Action<object?> _execute;
    // Predicate to metoda, która zawsze zwraca bool (true/false).
    private readonly Predicate<object?>? _canExecute;

    // 2. Konstruktor. Wymaga podania metody do wykonania. Metoda sprawdzająca jest opcjonalna.
    public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    // 3. Zdarzenie, które informuje interfejs (np. przycisk), że powinien na nowo 
    // sprawdzić, czy jest włączony (IsEnabled), czy wyłączony.
    public event EventHandler? CanExecuteChanged
    {
        // CommandManager.RequerySuggested to sprytny mechanizm wbudowany w WPF.
        // Automatycznie odświeża stan przycisków, gdy użytkownik coś kliknie, wpisze tekst itp.
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    // 4. Metoda sprawdzająca, czy komendę MOŻNA w tym momencie wykonać.
    // Jeśli zwróci false, WPF automatycznie zablokuje (wyszarzy) przycisk.
    public bool CanExecute(object? parameter)
    {
        // Jeśli nie przekazaliśmy metody sprawdzającej (_canExecute == null), to zawsze można klikać.
        // W przeciwnym razie wywołujemy przekazaną metodę i zwracamy jej wynik.
        return _canExecute == null || _canExecute(parameter);
    }

    // 5. Metoda właściwa. To ona odpala się, gdy faktycznie klikniesz przycisk.
    public void Execute(object? parameter)
    {
        _execute(parameter);
    }
}
