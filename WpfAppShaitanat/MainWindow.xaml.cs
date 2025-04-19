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
using WpfAppShaitanat.Pages;

namespace WpfAppShaitanat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance;

        public MainWindow()
        {
            InitializeComponent();
            if (Instance == null)
                Instance = this;

            NavigateTo(new FoodListPage());

            //List<AnimalFood> cosplaytype = App.DB.AnimalFood.ToList();
            //for (int i = 0; i < cosplaytype.Count(); i++)
            //{
            //    try
            //    {
            //        cosplaytype[i].ImageBinary = File.ReadAllBytes($"C:\\Users\\Amir\\Desktop\\Exam\\WpfAppShaitanat\\WpfAppShaitanat\\Resources\\Pr{i}.jpg");
            //    }
            //    finally { }
            //}
            //App.DB.SaveChanges();
        }

        public void NavigateTo(Page page)
        {
            MainFrame.NavigationService.Navigate(page);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void AppExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Закрыть приложение?", "Подтверждение", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
