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
    public partial class AddGenreWindow : Window
    {

        Genre _newGenre;

        public Genre NewGenre
        {
            get { return _newGenre; }
        }

        public AddGenreWindow()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Необходимо ввести название", "Ошибка!");
                textBoxName.Focus();
                return;
            }

            DialogResult = true;

            _newGenre = new Genre(textBoxName.Text);
        }
    }
}
