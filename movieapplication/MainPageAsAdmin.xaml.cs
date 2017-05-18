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
using System.IO;
using System.Xml.Serialization;

namespace movieapplication
{
    /// <summary>
    /// Логика взаимодействия для MainPageAsAdmin.xaml
    /// </summary>
    public partial class MainPageAsAdmin : Page
    {

        public MainPageAsAdmin()
        {
            InitializeComponent();
            Data.ReadMoviesData();
            RefreshListBox();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddMovieWindow();
            if (window.ShowDialog().Value)
            {
                Data.Movies.Add(window.newMovie);
                Data.UpdateMoviesData();
                RefreshListBox();
                Data.IsSearched = false;
                Logger.Log($"Добавлен новый фильм: \"{window.newMovie.name}\"");
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                Movie temp = Data.Movies[listBox.SelectedIndex];
                Data.Movies.RemoveAt(listBox.SelectedIndex);
                Data.UpdateMoviesData();
                RefreshListBox();
                Logger.Log($"Удален фильм: \"{temp.name}\"");
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index = -1, we set IsEnabled to false
            if (!Data.IsSearched)
            {
                buttonRemove.IsEnabled = listBox.SelectedIndex != -1;
            }
        }


        private void RefreshListBox()
        {
            listBox.ItemsSource = null;
            listBox.ItemsSource = !Data.IsSearched ? Data.Movies : Data.MoviesSearch;
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            var window = new SearchWindow();
            if (window.ShowDialog().Value)
            {
                string searchQuery = window.SearchQuery;
                Data.MoviesSearch = new List<Movie>();
                foreach (Movie movie in Data.Movies)
                {
                    if (movie.name.ToLower().Contains(searchQuery.ToLower()))
                    {
                        Data.MoviesSearch.Add(movie);
                    }
                }
                if (Data.MoviesSearch.Count == 0)
                {
                    MessageBox.Show("Фильмов с таким названием нет", "Результат поиска");
                }
                else
                {
                    Data.IsSearched = true;
                    buttonRemove.IsEnabled = false;
                    buttonReset.IsEnabled = true;
                    RefreshListBox();
                    Pages.MainPage.EnterSearchState();
                }
                Logger.Log($"Поиск с запросом: \"{searchQuery}\"");
            }
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            Data.IsSearched = false;
            buttonReset.IsEnabled = false;
            RefreshListBox();
            Pages.MainPage.LeaveSearchState();
            Logger.Log($"Отмена фильтрации по поисковому запросу");
        }

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            if (Data.IsSearched)
                Pages.MainPage.EnterSearchState();
            Logger.Log($"Выход из системы пользователя: \"{Data.LoggedUser.Username}\"");
            Data.LoggedUser = null;
            NavigationService.Navigate(Pages.MainPage);
        }

        public void EnterSearchState()
        {
            buttonRemove.IsEnabled = false;
            buttonReset.IsEnabled = true;
            RefreshListBox();
        }

        public void LeaveSearchState()
        {
            buttonReset.IsEnabled = false;
            RefreshListBox();
        }
    }
}
