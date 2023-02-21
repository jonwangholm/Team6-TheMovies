using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheMovies.MVVM.Model;
using TheMovies.MVVM.ViewModel.Persistence;
using TheMovies.MVVM.ViewModel;
using TheMovies.MVVM.Views;

namespace TheMovies.Commands
{
    public class UpdateMoviesCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is MainViewModel mvm)
            {
                foreach (Film film in mvm.FilmList)
                {
                    FilmRepo.Instance.Update(film);
                }
            }
        }
    }
}
