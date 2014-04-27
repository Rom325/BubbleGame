using System;
using System.Windows.Input;

namespace ViewModelClassLibrary
{
    public class RelayCommand : ICommand
	{
		private readonly Action<object> action;

		public RelayCommand(Action<object> action)
		{
			this.action = action;
		}

		// Игнорируем CanExecute
		public bool CanExecute(object parameter)
		{
			return true;
		}

		/// <summary>
		/// Вызывает внутренний делегат, при исполнении комманды.
		/// </summary>
		/// <param name="parameter">Единственный параметр, передаваемый в команду</param>
		public void Execute(object parameter)
		{
			action(parameter);
		}

		public event EventHandler CanExecuteChanged;
	}
}