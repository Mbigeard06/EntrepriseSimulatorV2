using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Products
{
    /// <summary>
    /// Class of bike constructor.
    /// </summary>
    public class BikeCreator : IProductCreator
    {
        /// <summary>
        /// Create a new bike.
        /// </summary>
        /// <returns>Return a bike.</returns>
        public Product Creer()
        {
            return new Bike();
        }
    }
}
