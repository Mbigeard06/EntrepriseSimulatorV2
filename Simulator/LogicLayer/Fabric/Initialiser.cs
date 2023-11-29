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
        /// <summary>
        /// Registre the product of the factory.
        /// </summary>
        /// <param name="productFactory"></param>
        public static void InitFactory(ProductFactory productFactory)
        {
            //Initialise les produtis 
            productFactory.Register("bike", new BikeCreator());
            productFactory.Register("car", new CarCreator());
            productFactory.Register("scooter", new ScooterCreator());
        }

        public static void InitClients(ClientService clientService)
        {
            clientService.InitProbs("car", 10);
            clientService.InitProbs("scooter", 14);
            clientService.InitProbs("bike", 20);
            clientService.InitNeeds("car", 0);
            clientService.InitNeeds("bike", 0);
            clientService.InitNeeds("scooter", 0);
        }
    }
}
