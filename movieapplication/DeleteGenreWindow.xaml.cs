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
    /// Логика взаимодействия для AddGenreWindow.xaml
    /// </summary>
    public partial class DeleteGenreWindow : Window
    {

        private Genre _replacement;

        private List<Genre> remainingGenres = new List<Genre>();

        public Genre Replacement
        {
            get { return _replacement; }
        }

        public DeleteGenreWindow(Genre removedGenre)
        {
            InitializeComponent();
            foreach (Genre genre in Data.Genres)
            {
                if (genre.Id != removedGenre.Id)
                    remainingGenres.Add(genre);
            }
            comboBox.ItemsSource = remainingGenres;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выбрать замену", "Ошибка!");
                comboBox.Focus();
                return;
            }
            _replacement = (Genre)comboBox.SelectedItem;
            DialogResult = true;
        }

    }
}
