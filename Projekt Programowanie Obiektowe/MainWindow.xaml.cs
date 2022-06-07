using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ViewModel.Drink Drink { get; set; } = new ViewModel.Drink(); // musi być utworzony, bo Binding się do niego "zwiąże"

        public ObservableCollection<ViewModel.Drink> MyDrinks { get; set; } = new ObservableCollection<ViewModel.Drink>();


        public MainWindow()
        {
            InitializeComponent();


            // odczyt z bazy drinków
            var drinkiContext = new ModelDanych.DrinkiContext();

            // jeśli nie ma to utworzenie bazy
            drinkiContext.Database.EnsureCreated();

            WczytajListęZBazy(drinkiContext);
            if (MyDrinks.Count > 0)
            {
                // wybierz pierwszy na liście 
                //Drink = MyDrinks[0];
                ViewModel.Drink.SkopiujDrinkProperties(MyDrinks[0], this.Drink);
            }

            this.DataContext = this;
        }

        private void WczytajListęZBazy(ModelDanych.DrinkiContext drinkiContext)
        {
            MyDrinks.Clear();
            foreach (var d in drinkiContext.Drinki.Include(d => d.DrinkSkladniki))
            {
                var _drink = new ViewModel.Drink { Id = d.Id, Nazwa = d.Nazwa, Przepis = d.Przepis, Kategoria = d.Kategoria, Miniatura = d.Miniatura, Grafika = d.Grafika, Tagi = d.Tagi, IdDrinkDB = d.IdDrinkDB };
                foreach (var i in d.DrinkSkladniki)
                {
                    // jawne ładowanie powiązanej jednostki
                    drinkiContext.Entry(i).Reference(p => p.Skladnik).Load();
                    _drink.Ingredients.Add(new ViewModel.Ingredient { Id = i.Skladnik.Id, Nazwa = i.Skladnik.Nazwa, Miara = i.Miara });
                }
                MyDrinks.Add(_drink);
            };
        }

        private void Button_SzukajWAPI_Click(object sender, RoutedEventArgs e)
        {
            var explorer = new ExplorerWindow();
            if (explorer.ShowDialog() == true)
            {
                var _drink = explorer.SelectedDrink;

                // przepisanie do Drink
                this.Drink.Id = null;
                this.Drink.Nazwa = _drink.DrinkName;
                this.Drink.IdDrinkDB = _drink.DrinkId;
                this.Drink.Kategoria = _drink.Category;
                this.Drink.Miniatura = _drink.DrinkThumb;
                this.Drink.Grafika = _drink.ImageSource;
                this.Drink.Przepis = _drink.Instructions;
                this.Drink.Tagi = _drink.Tags;

                this.Drink.Ingredients.Clear();

                string _ingredient;
                string _measure;

                _ingredient = _drink.Ingredient1;
                _measure = _drink.Measure1;
                if (_ingredient != null) { this.Drink.Ingredients.Add(new ViewModel.Ingredient() { Nazwa = _ingredient, Miara = _measure }); };

                _ingredient = _drink.Ingredient2;
                _measure = _drink.Measure2;
                if (_ingredient != null) { this.Drink.Ingredients.Add(new ViewModel.Ingredient() { Nazwa = _ingredient, Miara = _measure }); };

                _ingredient = _drink.Ingredient3;
                _measure = _drink.Measure3;
                if (_ingredient != null) { this.Drink.Ingredients.Add(new ViewModel.Ingredient() { Nazwa = _ingredient, Miara = _measure }); };

                _ingredient = _drink.Ingredient4;
                _measure = _drink.Measure4;
                if (_ingredient != null) { this.Drink.Ingredients.Add(new ViewModel.Ingredient() { Nazwa = _ingredient, Miara = _measure }); };

                _ingredient = _drink.Ingredient5;
                _measure = _drink.Measure5;
                if (_ingredient != null) { this.Drink.Ingredients.Add(new ViewModel.Ingredient() { Nazwa = _ingredient, Miara = _measure }); };

                _ingredient = _drink.Ingredient6;
                _measure = _drink.Measure6;
                if (_ingredient != null) { this.Drink.Ingredients.Add(new ViewModel.Ingredient() { Nazwa = _ingredient, Miara = _measure }); };

                _ingredient = _drink.Ingredient7;
                _measure = _drink.Measure7;
                if (_ingredient != null) { this.Drink.Ingredients.Add(new ViewModel.Ingredient() { Nazwa = _ingredient, Miara = _measure }); };

                _ingredient = _drink.Ingredient8;
                _measure = _drink.Measure8;
                if (_ingredient != null) { this.Drink.Ingredients.Add(new ViewModel.Ingredient() { Nazwa = _ingredient, Miara = _measure }); };

                _ingredient = _drink.Ingredient9;
                _measure = _drink.Measure9;
                if (_ingredient != null) { this.Drink.Ingredients.Add(new ViewModel.Ingredient() { Nazwa = _ingredient, Miara = _measure }); };

                _ingredient = _drink.Ingredient10;
                _measure = _drink.Measure10;
                if (_ingredient != null) { this.Drink.Ingredients.Add(new ViewModel.Ingredient() { Nazwa = _ingredient, Miara = _measure }); };
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Zamknąć program?", "Tytuł", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        private void Button_ZapiszDrinkaWBazie_Click(object sender, RoutedEventArgs e)
        {
            if (Drink != null)
            {
                // zapisz do bazy 
                var drinkiContext = new ModelDanych.DrinkiContext();

                ModelDanych.Drink? _drink = null;

                // nowy czy już zapisany?
                /*
                if (Drink.Id != null)
                {
                    _drink = drinkiContext.Drinki
                        .Include(x => x.DrinkSkladniki)
                        .First(x => x.Id == Drink.Id);
                }
                else
                {
                */
                _drink = new ModelDanych.Drink();
                _drink.DrinkSkladniki = new List<ModelDanych.DrinkSkladnik>();
                //}
                _drink.Nazwa = this.Drink.Nazwa;
                _drink.IdDrinkDB = this.Drink.IdDrinkDB;
                _drink.Kategoria = this.Drink.Kategoria;
                _drink.Miniatura = this.Drink.Miniatura;
                _drink.Grafika = this.Drink.Grafika;
                _drink.Przepis = this.Drink.Przepis;
                _drink.Tagi = this.Drink.Tagi;

                // czy są nowe składniki?
                foreach (var i in this.Drink.Ingredients)
                {
                    ModelDanych.Skladnik? _skladnik = null;
                    if (i.Id == null)
                    {
                        // nowy składnik?
                        _skladnik = drinkiContext.Skladniki.FirstOrDefault(x => x.Nazwa == i.Nazwa);
                        if (_skladnik == null)
                        {
                            _skladnik = new ModelDanych.Skladnik { Nazwa = i.Nazwa };
                            //drinkiContext.Skladniki.Add(_skladnik);
                            //drinkiContext.SaveChanges(); // dostanie id!
                        }
                    }
                    else
                        _skladnik = drinkiContext.Skladniki.First(x => x.Id == i.Id);

                    _drink.DrinkSkladniki.Add(new ModelDanych.DrinkSkladnik { Skladnik = _skladnik, Miara = i.Miara });
                }

                //if (Drink.Id == null)
                //{
                drinkiContext.Drinki.Add(_drink);
                //}

                drinkiContext.SaveChanges();

                MessageBox.Show("Zapisany!");

                WczytajListęZBazy(drinkiContext);
            }
        }

        private void ListDrinks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // czy jest coś wybrane?
            if (e.AddedItems.Count > 0)
            {
                var _drink = (ViewModel.Drink)e.AddedItems[0];

                // przepisanie
                ViewModel.Drink.SkopiujDrinkProperties(_drink, this.Drink);
            }
        }

    }
}
