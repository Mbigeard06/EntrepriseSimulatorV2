using LogicLayer.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestLogicLayer
{
    public class TestProductFactory
    {
        [Fact]
        public void TestFactory()
        {
            ProductFactory.Instance.Register("bike", new BikeCreator());
            ProductFactory.Instance.Register("car", new CarCreator());
            ProductFactory.Instance.Register("scooter", new ScooterCreator());
            //On vérifie que les 3 éléments ont bien été ajouté.
            Assert.Equal(3, ProductFactory.Instance.Products.Count());
            //On vérifie les bons objets sont crée dans la méthode Créer
            Assert.True(ProductFactory.Instance.Creer("bike").Equals(new Bike()));
            Assert.True(ProductFactory.Instance.Creer("car").Equals(new Car()));
            Assert.True(ProductFactory.Instance.Creer("scooter").Equals(new Scooter()));
        }
    }
}
