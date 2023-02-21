using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheMovies.MVVM.Views;
using TheMovies.MVVM.ViewModel;
using TheMovies.MVVM.Model;
using TheMovies.MVVM.ViewModel.Persistence;

namespace TheMovies.MVVM.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainViewModel mvm = new MainViewModel();   
        public MainWindow()
        {
            InitializeComponent();

            DataContext = mvm;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            FilmRepo.Instance.Save();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        
        }
    }
}
