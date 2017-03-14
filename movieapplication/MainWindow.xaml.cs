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

namespace movieapplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Movie> _movies = new List<Movie>();

        public MainWindow()
        {
            InitializeComponent();

            _movies.Add(new Movie("Под покровом ночи", 2016, "США", "Триллер"));
            _movies.Add(new Movie("Ла-Ла Ленд", 2016, "США", "Мюзикл"));
            RefreshListBox();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddMovieWindow();
            if (window.ShowDialog().Value)
            {
                _movies.Add(window.newMovie);
                RefreshListBox();
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                _movies.RemoveAt(listBox.SelectedIndex);
                RefreshListBox();
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index = -1, we set IsEnabled to false
            buttonRemove.IsEnabled = listBox.SelectedIndex != -1;
        }

        private void RefreshListBox()
        {
            listBox.ItemsSource = null;
            listBox.ItemsSource = _movies;
        }
    }
}
