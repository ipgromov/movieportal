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

        private List<Movie> dynamicMoviesSearch = new List<Movie>();

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
                Data.Movies.Add(window.NewMovie);
                Data.UpdateMoviesData();
                Data.IsSearched = false;
                buttonReset.IsEnabled = false;
                RefreshListBox();
                DisableButtons();
                Logger.Log($"Добавлен новый фильм: \"{window.NewMovie.Name}\"");
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                Movie selectedMovie = (Movie)listBox.SelectedItem;
                Data.Movies.RemoveAll(movie => movie.Id == selectedMovie.Id);
                if (Data.IsSearched)
                    Data.MoviesSearch.RemoveAll(movie => movie.Id == selectedMovie.Id);

                textBoxSearch.Text = "";
                Data.UpdateMoviesData();
                RefreshListBox();
                Logger.Log($"Удален фильм: \"{selectedMovie.Name}\"");
            }
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                Movie selectedMovie = (Movie)listBox.SelectedItem;
                int selectedListIndex = Data.Movies.FindIndex(movie => movie.Id == selectedMovie.Id);

                var window = new ChangeMovieWindow(Data.Movies[selectedListIndex]);
                if (window.ShowDialog().Value)
                {
                    Data.Movies[selectedListIndex] = window.ChangedMovie;
                    Data.UpdateMoviesData();
                    Data.IsSearched = false;
                    RefreshListBox();
                    DisableButtons();
                    Logger.Log(selectedMovie.Name == window.ChangedMovie.Name ? 
                        $"Изменён фильм: \"{window.ChangedMovie.Name}\"" : 
                        $"Изменён фильм: \"{selectedMovie.Name}\" -> \"{window.ChangedMovie.Name}\"");
                }
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                DisableButtons();
        }


        public void RefreshListBox()
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
                    if (movie.Name.ToLower().Contains(searchQuery.ToLower()))
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
                    buttonReset.IsEnabled = true;
                    textBoxSearch.Text = "";
                    RefreshListBox();
                }
                Logger.Log($"Поиск с запросом: \"{searchQuery}\"");
            }
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            Data.IsSearched = false;
            buttonReset.IsEnabled = false;
            textBoxSearch.Text = "";
            RefreshListBox();
            Logger.Log($"Отмена фильтрации по поисковому запросу");
        }

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            Pages.MainPage.UpdateSearchState();
            Logger.Log($"Выход из системы пользователя: \"{Data.LoggedUser.Username}\"");
            Data.LoggedUser = null;
            NavigationService.Navigate(Pages.MainPage);
            listBox.SelectedIndex = -1;
        }

        public void UpdateSearchState()
        {
            if (Data.IsSearched)
                buttonReset.IsEnabled = true;
            else
                buttonReset.IsEnabled = false;
            RefreshListBox();
        }


        private void DisableButtons()
        {
            buttonRemove.IsEnabled = listBox.SelectedIndex != -1;
            buttonUpdate.IsEnabled = listBox.SelectedIndex != -1;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.LoggedUser != null)
                textBlockGreeting.Text = $"Здравствуйте, {Data.LoggedUser.Username}!";
            textBoxSearch.Text = "";
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string dynamicSearchQuery = textBoxSearch.Text;
            dynamicMoviesSearch = new List<Movie>();
            foreach (Movie movie in Data.IsSearched ? Data.MoviesSearch : Data.Movies)
            {
                if (movie.Name.ToLower().Contains(dynamicSearchQuery.ToLower()))
                {
                    dynamicMoviesSearch.Add(movie);
                }
            }
            listBox.ItemsSource = null;
            listBox.ItemsSource = dynamicMoviesSearch;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.ControlPage);
        }
    }
}
