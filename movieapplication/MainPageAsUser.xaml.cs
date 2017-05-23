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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPageAsUser : Page
    {
        private List<Movie> dynamicMoviesSearch = new List<Movie>();

        public MainPageAsUser()
        {
            InitializeComponent();
            Data.ReadMoviesData();
            RefreshListBox();
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
    }
}
