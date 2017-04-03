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

        public MainWindow()
        {
            InitializeComponent();
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
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                _movies.RemoveAt(listBox.SelectedIndex);
                UpdateData();
                RefreshListBox();
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index = -1, we set IsEnabled to false
            buttonRemove.IsEnabled = listBox.SelectedIndex != -1;
        }

        private void LoadData()
        {
            using (FileStream fs = new FileStream("data.xml", FileMode.Open))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Movie>));
                _movies = (List<Movie>)xml.Deserialize(fs);
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
    }
}
