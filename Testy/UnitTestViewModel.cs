using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;
using System.Diagnostics;

using Projekt_Programowanie_Obiektowe;

using Projekt_Programowanie_Obiektowe.ViewModel;

namespace Testy
{
    [TestClass]
    public class UnitTestViewModel
    {
        [TestMethod]
        public void KopiowanieDrink()
        {
            var src = new Drink
            {
                Id = 1,
                Nazwa = "AAA",
                Ingredients = new System.Collections.ObjectModel.ObservableCollection<Ingredient>
                {
                    new Ingredient { Id = 1, Nazwa = "AAAA" },
                    new Ingredient { Id = 2, Nazwa = "BBBB", Miara = "1/1" }
                }
            };

            var dst = new Drink();

            Drink.SkopiujDrinkProperties(src, dst);

            Assert.IsTrue(src.Nazwa == dst.Nazwa);
            Assert.IsTrue(src.Ingredients.Count() == dst.Ingredients.Count());
        }
    }
}