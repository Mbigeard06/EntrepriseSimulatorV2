using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Interface des créateurs de produit
    /// </summary>
    public interface IProductCreator
    {   
        /// <summary>
        /// Renvoi le produit
        /// </summary>
        /// <returns></returns>
        Product Creer();
    }
}

