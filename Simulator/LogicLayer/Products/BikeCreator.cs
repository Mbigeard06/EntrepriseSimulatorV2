using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Products
{
    /// <summary>
    /// Clase de création d'un bike.
    /// </summary>
    public class BikeCreator : IProductCreator
    {
        /// <summary>
        /// Crée un nouveau bike.
        /// </summary>
        /// <returns>Renvoi un bike.</returns>
        public Product Creer()
        {
            return new Bike();
        }
    }
}
