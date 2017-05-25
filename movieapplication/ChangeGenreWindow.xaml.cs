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
    public partial class ChangeGenreWindow : Window
    {

        string _changedName;

        public string ChangedName
        {
            get { return _changedName; }
        }

        public ChangeGenreWindow(Genre receivedGenre)
        {
            InitializeComponent();
            textBoxName.Text = receivedGenre.Name;
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

            _changedName = textBoxName.Text;
        }
    }
}
