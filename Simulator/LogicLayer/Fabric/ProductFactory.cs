using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Products;

namespace LogicLayer.Fabric
{
    /// <summary>
    /// Product factory
    /// </summary>
    public class ProductFactory
    {
        /// <summary>
        /// Dictionnary that associates a product name and its constructor.
        /// </summary>
        private Dictionary<string, IProductCreator> products;

        /// <summary>
        /// Reguster a product in the factory
        /// </summary>
        /// <param name="productName">Product name.</param>
        /// <param name="productCreator">Product constructor.</param>
        public void Register(string productName, IProductCreator productCreator)
        {
            products[productName] = productCreator;
        }

        /// <summary>
        /// Return the product if it is registred.
        /// </summary>
        /// <param name="productName">product to create</param>
        /// <returns></returns>
        public Product Creer(string productName)
        {
            Product product = null;
            if (products.ContainsKey(productName))
            {
                product = products[productName].Creer();
            }
            return product;
        }

        /// <summary>
        /// Return the products registred.
        /// </summary>
        /// <returns>List of the products.</returns>
        public string[] Products { get { return products.Keys.ToArray(); } }

        /// <summary>
        /// Constructor of the fabric.
        /// </summary>
        public ProductFactory() 
        {
            products = new Dictionary<string, IProductCreator>();
            Initialiser.InitFactory(this);
        }
    }
}
