using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Products;

namespace LogicLayer.Fabric
{
    /// <summary>
    /// Fabric de product Factory
    /// </summary>
    public class ProductFactory
    {
        /// <summary>
        /// Dictionnaire qui associe le nom des produits à leurs objets.
        /// </summary>
        private Dictionary<string, IProductCreator> products;

        /// <summary>
        /// Enregistre un produit dans la factory.
        /// </summary>
        /// <param name="productName">Nom du produit.</param>
        /// <param name="productCreator">Createur du produit.</param>
        public void Register(string productName, IProductCreator productCreator)
        {
            products[productName] = productCreator;
        }

        /// <summary>
        /// Renvoi le produit crée, sil a déja été enregistré.
        /// </summary>
        /// <param name="productName">produit à crée</param>
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
        /// Renvoi la liste des noms de jeu enregistrés.
        /// </summary>
        public string[] Products { get { return products.Keys.ToArray(); } }

        /// <summary>
        /// Constructeur du singleton.
        /// </summary>
        public ProductFactory() 
        {
            products = new Dictionary<string, IProductCreator>();
            Initialiser.InitFactory(this);
        }

    }

}
