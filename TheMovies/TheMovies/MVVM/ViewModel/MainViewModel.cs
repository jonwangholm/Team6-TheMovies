using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheMovies.Commands;
using TheMovies.MVVM.Model;
using TheMovies.MVVM.ViewModel.Persistence;

namespace TheMovies.MVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Commands
        public CreateMovieCommand CreateMovieCommand { get; set; } = new();
        public UpdateMoviesCommand UpdateMoviesCommand { get; set; } = new();
        public DeleteMovieCommand DeleteMovieCommand { get; set; } = new();
        #endregion

        public ObservableCollection<Film> FilmList { get; set; }
        private Film _s1;
        public Film S1
        {
            get
            {
                return _s1;
            }

            set
            {
                _s1 = value;
                OnPropertyChanged(nameof(S1));
            }
        }

        public MainViewModel()
        {
            FilmRepo.Instance.Load();

            FilmList = new ObservableCollection<Film>(FilmRepo.Instance.RetrieveAll());

            FilmList.CollectionChanged += OnFilmListChanged;
        }

        #region OnChanged events
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        private void OnFilmListChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Trace.Write("Film collection changed!");
        }
        #endregion

        public void CreateNewMovie(MainViewModel mvm, string title, int duration, string genre)
        {
            //mvm.filmList.Add(new Film(title, duration, genre));

        }
    }
}
