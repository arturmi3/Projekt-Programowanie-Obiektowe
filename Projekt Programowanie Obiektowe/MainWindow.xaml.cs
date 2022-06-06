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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt_Programowanie_Obiektowe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tekstSzukaj = this.TekstSzukaj.Text;
            if (string.IsNullOrEmpty(tekstSzukaj)) return;

            var client = new CocktailDbAPI.CocktailAPI();
            List<CocktailDbAPI.Models.Drink.Drink>? lista = await client.GetDrinksByNameAsync(tekstSzukaj);

            string[] drinks = lista.Select(drink => drink.DrinkName).ToArray();

            this.Drinks.ItemsSource = drinks;
        }
    }
}
