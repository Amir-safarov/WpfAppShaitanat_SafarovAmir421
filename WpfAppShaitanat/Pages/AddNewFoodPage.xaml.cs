using Microsoft.Win32;
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
using WpfAppShaitanat.DataBase;

namespace WpfAppShaitanat.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddNewFoodPage.xaml
    /// </summary>
    public partial class AddNewFoodPage : Page
    {
        private byte[] imageBytes;

        public AddNewFoodPage()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(FoodPriceTB.Text))
                error.AppendLine("Цена пустая");
            if (string.IsNullOrWhiteSpace(FoodNameTB.Text))
                error.AppendLine("Наименование корма пустое");
            if (imageBytes == null || imageBytes.Length == 0)
                error.AppendLine("Фото отсутсвует");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }

            AnimalFood newiten = new AnimalFood()
            {
                LastEditDate = DateTime.Now,
                Price = int.Parse(FoodPriceTB.Text),
                Name = FoodNameTB.Text,
                ImageBinary = imageBytes,
            };
            App.DB.AnimalFood.Add(newiten);

            App.DB.SaveChanges();

            NavigationService.Navigate(new FoodListPage());

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FoodListPage());
        }

        private void FoodPriceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void EditImaeButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                tImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
