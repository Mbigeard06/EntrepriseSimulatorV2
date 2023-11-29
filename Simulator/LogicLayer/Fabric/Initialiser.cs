using LogicLayer.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Fabric
{
    /// <summary>
    /// Classe to initialize the fabric.
    /// </summary>
    public class Initialiser
    {
        public static void InitFactory(ProductFactory productFactory)
        {
            //Initialise les produtis 
            productFactory.Register("bike", new BikeCreator());
            productFactory.Register("car", new CarCreator());
            productFactory.Register("scooter", new ScooterCreator());
        }
    }
}
