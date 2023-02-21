using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.MVVM.ViewModel
{
    public class CreateFilmViewModel : INotifyPropertyChanged
    {
        private string _enterTitle;
        public string EnterTitle
        {
            get
            {
                return _enterTitle;
            }

            set
            {
                _enterTitle = value;
                OnPropertyChanged(nameof(EnterTitle));
            }
        }
        private int _enterDuration;
        public int EnterDuration
        {
            get
            {
                return _enterDuration;
            }

            set
            {
                _enterDuration = value;
                OnPropertyChanged(nameof(EnterDuration));
            }
        }
        private string _enterGenre;
        public string EnterGenre
        {
            get
            {
                return _enterGenre;
            }

            set
            {
                _enterGenre = value;
                OnPropertyChanged(nameof(EnterGenre));
            }
        }

        #region OnChanged events
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        #endregion
    }
}
