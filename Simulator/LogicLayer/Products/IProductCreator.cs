using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Products
{
    /// <summary>
    /// Interface of product creators.
    /// </summary>
    public interface IProductCreator
    {
        /// <summary>
        /// Return a product.
        /// </summary>
        /// <returns></returns>
        Product Creer();
    }
}

