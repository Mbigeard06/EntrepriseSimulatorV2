using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Products
{
    /// <summary>
    /// Electric scooter creator.
    /// </summary>
    internal class TelecCreator : IProductCreator
    {
        /// <summary>
        /// Create an electric scooter.
        /// </summary>
        /// <returns></returns>
        public Product Creer()
        {
            return new Telec();
        }
    }
}
