using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;
using System.Diagnostics;

using Projekt_Programowanie_Obiektowe;


namespace Testy
{
    [TestClass]
    public class UnitTestModelDanych
    {
        [TestMethod]
        public void TworzenieBazy()
        {
            using (var kontekst = new ModelDanych.DrinkiContext())
            {
                bool dbCreated = kontekst.Database.EnsureCreated();
                Assert.IsTrue(dbCreated);

                bool canConnect = kontekst.Database.CanConnect();
                Assert.IsTrue(canConnect);

                int n = kontekst.Drinki.Count();
                Assert.IsTrue(n >= 0);
            }
        }
    }
}