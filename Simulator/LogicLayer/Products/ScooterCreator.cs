using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Products
{
    /// <summary>
    /// Classe de création d'un scooter.
    /// </summary>
    public class ScooterCreator : IProductCreator
    {
        /// <summary>
        /// Crée un nouveau scooter.
        /// </summary>
        /// <returns>Renvoi un nouveau scooter.</returns>
        public Product Creer()
        {
            return new Scooter();
        }
    }
}
