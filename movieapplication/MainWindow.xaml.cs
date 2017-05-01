using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace movieapplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Movie> _movies = new List<Movie>();

        List<Movie> _moviesSearch = new List<Movie>();

        bool isSearched = false;

        public MainWindow()
        {
            InitializeComponent();
            Logger.Log("Запуск программы");
            LoadData();
            RefreshListBox();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddMovieWindow();
            if (window.ShowDialog().Value)
            {
                _movies.Add(window.newMovie);
                UpdateData();
                RefreshListBox();
                Logger.Log($"Добавлен новый фильм: \"{window.newMovie.name}\"");
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                Movie temp = _movies[listBox.SelectedIndex];
                _movies.RemoveAt(listBox.SelectedIndex);
                UpdateData();
                RefreshListBox();
                Logger.Log($"Удален фильм: \"{temp.name}\"");
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index = -1, we set IsEnabled to false
            if (!isSearched)
            {
                buttonRemove.IsEnabled = listBox.SelectedIndex != -1;
            }
        }

        private void LoadData()
        {
            if (File.Exists("data.xml"))
            {
                using (FileStream fs = new FileStream("data.xml", FileMode.Open))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Movie>));
                    _movies = (List<Movie>)xml.Deserialize(fs);
                }
            }
        }

        private void UpdateData()
        {
            using (FileStream fs = new FileStream("data.xml", FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Movie>));
                xml.Serialize(fs, _movies);
            }
        }

        private void RefreshListBox()
        {
            listBox.ItemsSource = null;
            listBox.ItemsSource = _movies;
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            var window = new SearchWindow();
            if (window.ShowDialog().Value)
            {
                isSearched = true;
                buttonRemove.IsEnabled = false;
                buttonReset.IsEnabled = true;
                string searchQuery = window.searchQuery;
                _moviesSearch = new List<Movie>();
                foreach (Movie movie in _movies)
                {
                    if (movie.name == searchQuery)
                    {
                        _moviesSearch.Add(movie);
                    }
                }
                listBox.ItemsSource = _moviesSearch;
                if (_moviesSearch.Count == 0)
                {
                    MessageBox.Show("Фильмов с таким названием нет", "Результат поиска");
                }
                Logger.Log($"Поиск с запросом: \"{searchQuery}\"");
            }
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            isSearched = false;
            buttonReset.IsEnabled = false;
            RefreshListBox();
            Logger.Log($"Отмена фильтрации по поисковому запросу");
        }
    }
}
