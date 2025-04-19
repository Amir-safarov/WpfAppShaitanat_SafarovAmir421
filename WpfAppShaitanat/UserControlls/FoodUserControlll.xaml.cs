using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfAppShaitanat.DataBase;

namespace WpfAppShaitanat.UserControlls
{
    /// <summary>
    /// Логика взаимодействия для FoodUserControlll.xaml
    /// </summary>
    public partial class FoodUserControlll : UserControl
    {
        private AnimalFood food;
        public FoodUserControlll(AnimalFood food)
        {
            InitializeComponent();
            this.food = food;
            FoodNameTB.Text = food.Name;
            FoodPriceTB.Text = food.Price.ToString();
            FoodLastEditDateTB.Text = food.LastEditDate.ToString();
            FoodImg.Source = GetimageSources(food.ImageBinary);
        }

        private BitmapImage GetimageSources(byte[] byteImage)
        {
            if (byteImage != null)
            {
                MemoryStream memoryStream = new MemoryStream(byteImage);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = memoryStream;
                image.EndInit();
                return image;
            }
            return null;
        }

        private void FoodNameTB_LostFocus(object sender, RoutedEventArgs e)
        {
            food.Name = FoodNameTB.Text;
            food.LastEditDate = DateTime.Now;
            App.DB.SaveChanges();
        }

        private void FoodPriceTB_LostFocus(object sender, RoutedEventArgs e)
        {
            food.Price = int.Parse(FoodPriceTB.Text);
            food.LastEditDate = DateTime.Now;
            App.DB.SaveChanges();
        }

        private void FoodPriceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }
    }
}
