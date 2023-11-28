using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Products
{
    /// <summary>
    /// Classe de création d'une voiture
    /// </summary>
    public class CarCreator : IProductCreator
    {
        /// <summary>
        /// Crée une nouvelle voiture.
        /// </summary>
        /// <returns>Renvoi la voiture</returns>
        public Product Creer()
        {
            return new Car();
        }
    }
}
