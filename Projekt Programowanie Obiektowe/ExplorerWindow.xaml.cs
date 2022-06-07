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

namespace Projekt_Programowanie_Obiektowe
{
    /// <summary>
    /// Interaction logic for ExplorerWindow.xaml
    /// </summary>
    public partial class ExplorerWindow : Window
    {
        // w tej wpłaściwości exlorer zwróci wybrany obiekt
        public CocktailDbAPI.Models.Drink.Drink? SelectedDrink { get; set; }



        public ExplorerWindow()
        {
            InitializeComponent();

            // można tak!
            this.DataContext = this;
        }


        private async void SzukajWgNazwy_Click(object sender, RoutedEventArgs e)
        {
            string tekstSzukaj = this.TekstSzukaj.Text;
            if (string.IsNullOrEmpty(tekstSzukaj)) return;

            var client = new CocktailDbAPI.CocktailAPI();
            List<CocktailDbAPI.Models.Drink.Drink>? lista = await client.GetDrinksByNameAsync(tekstSzukaj);

            //string[] drinks = lista.Select(drink => drink.DrinkName).ToArray();

            // bez bindingu!
            this.Lista.ItemsSource = lista;
        }

        private void Button_Wybierz_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDrink != null)
            {
                this.DialogResult = true;
            }
        }
    }
}
