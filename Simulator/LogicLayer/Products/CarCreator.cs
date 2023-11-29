using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Products
{
    /// <summary>
    /// Class of car constructor.
    /// </summary>
    public class CarCreator : IProductCreator
    {
        /// <summary>
        /// Create a new car.
        /// </summary>
        /// <returns>Return a car</returns>
        public Product Creer()
        {
            return new Car();
        }
    }
}
