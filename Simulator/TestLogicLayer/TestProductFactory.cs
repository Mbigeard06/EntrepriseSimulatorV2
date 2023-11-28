using LogicLayer.Fabric;
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
        /// <summary>
        /// Test sur FabricProduct.
        /// </summary>
        [Fact]
        public void TestFactory()
        {
            ProductFactory productFactory = new ProductFactory();
            //On vérifie que les 3 éléments ont bien été ajouté à l'initialisation.
            Assert.Equal(3, productFactory.Products.Count());
            //On vérifie les bons objets sont crée dans la méthode Créer
            Assert.True(productFactory.Creer("bike").Equals(new Bike()));
            Assert.True(productFactory.Creer("car").Equals(new Car()));
            Assert.True(productFactory.Creer("scooter").Equals(new Scooter()));
        }
    }
}
