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
using System.Windows.Shapes;

namespace movieapplication
{
    /// <summary>
    /// Логика взаимодействия для AddMovieWindow.xaml
    /// </summary>
    /// 
    public partial class ChangeMovieWindow : Window
    {

        private Movie _changedMovie;

        public Movie changedMovie
        {
            get { return _changedMovie; }
        }

        private Movie _receivedMovie;


        public ChangeMovieWindow(Movie receivedMovie)
        {
            InitializeComponent();
            _receivedMovie = receivedMovie;
            ReceiveMovieInfo();
            comboBox.ItemsSource = Data.Genres;
        }

        public void ReceiveMovieInfo()
        {
            textBoxName.Text = _receivedMovie.Name;
            textBoxCountry.Text = _receivedMovie.Country;
            textBoxYear.Text = _receivedMovie.Year.ToString();
            Genre receivedGenre = null;
            foreach (Genre genre in Data.Genres)
            {
                if (genre.Id == _receivedMovie.GenreId)
                {
                    receivedGenre = genre;
                    break;
                }
            }
            if (receivedGenre != null)
                comboBox.SelectedItem = receivedGenre;
            else
                comboBox.SelectedIndex = 0;
        }

        private void buttonChange_Click(object sender, RoutedEventArgs e)
        {
            int year;
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Необходимо ввести название", "Ошибка!");
                textBoxName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxCountry.Text))
            {
                MessageBox.Show("Необходимо ввести страну", "Ошибка!");
                textBoxName.Focus();
                return;
            }

            if (!int.TryParse(textBoxYear.Text, out year))
            {
                MessageBox.Show("Некорректное значение года выпуска", "Ошибка!");
                textBoxYear.Focus();
                return;
            }

            if (comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выбрать жанр", "Ошибка!");
                comboBox.Focus();
                return;
            }

            Genre selectedGenre = (Genre)comboBox.SelectedItem;
            _changedMovie = new Movie(_receivedMovie.Id, textBoxName.Text, year, textBoxCountry.Text, selectedGenre.Id);

            // Close current window
            DialogResult = true;
        }
    }
}
