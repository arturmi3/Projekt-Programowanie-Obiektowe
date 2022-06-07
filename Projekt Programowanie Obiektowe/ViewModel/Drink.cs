using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Programowanie_Obiektowe.ViewModel
{
    public class Ingredient : INotifyPropertyChanged
    {
        int? id = null;
        string nazwa = null;
        string miara = null;

        public int? Id 
        { 
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        public string Nazwa
        {
            get { return nazwa; }
            set { nazwa = value; OnPropertyChanged(); }
        }
        public string Miara
        {
            get { return miara; }
            set { miara = value; OnPropertyChanged(); }
        }


        // źródło: https://docs.microsoft.com/pl-pl/dotnet/desktop/wpf/data/how-to-implement-property-change-notification?view=netframeworkdesktop-4.8
        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class Drink : INotifyPropertyChanged
    {
        int? id = null;
        string nazwa = null;
        string przepis = null;

        string idDrinkDB = null;

        string tagi = null;
        string kategoria = null;

        string miniatura = null;
        string grafika = null;

        public int? Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        public string Nazwa
        {
            get { return nazwa; }
            set { nazwa = value; OnPropertyChanged(); }
        }

        public string Przepis
        {
            get { return przepis; }
            set { przepis = value; OnPropertyChanged(); }
        }


        // identyfikator drinka w serwisie TheCoctails..
        public string IdDrinkDB
        {
            get { return idDrinkDB; }
            set { idDrinkDB = value; OnPropertyChanged(); }
        }

        public string Tagi
        {
            get { return tagi; }
            set { tagi = value; OnPropertyChanged(); }
        }

        public string Kategoria
        {
            get { return kategoria; }
            set { kategoria = value; OnPropertyChanged(); }
        }

        public string Miniatura
        {
            get { return miniatura; }
            set { miniatura = value; OnPropertyChanged(); }
        }
        public string Grafika 
        {
            get { return grafika; }
            set { grafika = value; OnPropertyChanged(); }
        }
        

        public ObservableCollection<Ingredient> Ingredients { get; set; } = new ObservableCollection<Ingredient>();


        public static void SkopiujDrinkProperties(ViewModel.Drink _src, ViewModel.Drink _dst)
        {
            _dst.Id = _src.Id;
            _dst.Nazwa = _src.Nazwa;
            _dst.Przepis = _src.Przepis;
            _dst.IdDrinkDB = _src.IdDrinkDB;
            _dst.Tagi = _src.Tagi;
            _dst.Kategoria = _src.Kategoria;
            _dst.Miniatura = _src.Miniatura;
            _dst.Grafika = _src.Grafika;
            _dst.Ingredients.Clear();
            foreach (var i in _src.Ingredients)
                _dst.Ingredients.Add(i);
        }


        // źródło: https://docs.microsoft.com/pl-pl/dotnet/desktop/wpf/data/how-to-implement-property-change-notification?view=netframeworkdesktop-4.8
        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;
        
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


}
