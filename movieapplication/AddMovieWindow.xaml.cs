﻿using System;
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
    public partial class AddMovieWindow : Window
    {

        Movie _newMovie;

        public Movie NewMovie
        {
            get { return _newMovie; }
        }


        public AddMovieWindow()
        {
            InitializeComponent();
            comboBox.ItemsSource = Data.Genres;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
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

            if (int.Parse(textBoxYear.Text) < 1880 || int.Parse(textBoxYear.Text) > 2030)
            {
                MessageBox.Show("Введен неприемлимый год выпуска", "Ошибка!");
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
            _newMovie = new Movie(textBoxName.Text, year, textBoxCountry.Text, selectedGenre.Id);

            DialogResult = true;
        }
    }
}
