using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TheMovies.MVVM.Model;
using TheMovies.MVVM.ViewModel;
using TheMovies.MVVM.ViewModel.Persistence;
using TheMovies.MVVM.Views;

namespace TheMovies.Commands
{
    public class CreateMovieCommand : ICommand
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
                AddMovieWindow amw = new AddMovieWindow();

                if (amw.ShowDialog() == true)
                {
                    if (amw.DataContext is CreateFilmViewModel createFilmVM)
                    {
                        Film film = new(createFilmVM.EnterTitle, createFilmVM.EnterGenre, createFilmVM.EnterDuration, "", DateTime.Now);
                        mvm.FilmList.Add(FilmRepo.Instance.Create(film));
                        FilmRepo.Instance.Save();
                    }
                }
            }
        }
    }
}
