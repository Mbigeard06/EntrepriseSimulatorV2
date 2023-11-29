using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Products
{
    /// <summary>
    /// Class of scooter constructor.
    /// </summary>
    public class ScooterCreator : IProductCreator
    {
        /// <summary>
        /// Create a new scooter.
        /// </summary>
        /// <returns>Renturn a new scooter.</returns>
        public Product Creer()
        {
            return new Scooter();
        }
    }
}
