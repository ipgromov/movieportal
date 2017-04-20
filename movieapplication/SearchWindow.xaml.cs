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
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {

        private string _searchQuery;

        public string searchQuery
        {
            get { return _searchQuery; }
        }

        public SearchWindow()
        {
            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameSearch.Text))
            {
                MessageBox.Show("Необходимо ввести запрос", "Ошибка!");
                nameSearch.Focus();
                return;
            }

            _searchQuery = nameSearch.Text;

            // Close current window
            DialogResult = true;
        }
    }
}
