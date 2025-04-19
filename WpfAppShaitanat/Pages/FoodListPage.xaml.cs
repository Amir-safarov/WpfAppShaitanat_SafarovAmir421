using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfAppShaitanat.DataBase;
using WpfAppShaitanat.UserControlls;

namespace WpfAppShaitanat.Pages
{
    /// <summary>
    /// Логика взаимодействия для FoodListPage.xaml
    /// </summary>
    public partial class FoodListPage : Page
    {
        private List<AnimalFood> allFood;
        private List<AnimalFood> filteredFood;

        private const int PageSize = 6;
        private int currentPage = 0;

        public FoodListPage()
        {
            InitializeComponent();
            allFood = App.DB.AnimalFood.ToList();
            DateFilterCB.SelectedIndex = 0;
            RefreshList();
        }

        private void RefreshList()
        {
            FoodsWp.Children.Clear();
            filteredFood = allFood;

            var foodToShow = filteredFood
                .Skip(currentPage * PageSize)
                .Take(PageSize)
                .ToList();

            switch (DateFilterCB.SelectedIndex)
            {
                case 1:
                    foodToShow = foodToShow.OrderByDescending(x => x.Name).ToList();
                    break;
                case 2:
                    foodToShow = foodToShow.OrderBy(x => x.Name).ToList();
                    break;
            }

            foreach (var suit in foodToShow)
            {
                FoodsWp.Children.Add(new FoodUserControlll(suit));
            }

            UpdatePageText(foodToShow.Count);
            UpdateNavigationButtons();
        }

        private void UpdatePageText(int shownCount)
        {
            int startItem = currentPage * PageSize + 1;
            int endItem = startItem + shownCount - 1;
            int totalItems = filteredFood.Count;

            PagesTb.Text = $"{startItem}-{endItem} из {totalItems}";
        }

        private void UpdateNavigationButtons()
        {
            ToLeftBtn.IsEnabled = currentPage > 0;
            ToRightBtn.IsEnabled = (currentPage + 1) * PageSize < allFood.Count();
        }

        private void PrefPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                DateFilterCB.SelectedIndex = 0;
                RefreshList();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if ((currentPage + 1) * PageSize < allFood.Count())
            {
                currentPage++;
                DateFilterCB.SelectedIndex = 0;
                RefreshList();
            }
        }

        private void DateFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshList();
        }

        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddNewFoodPage());
        }
    }
}

