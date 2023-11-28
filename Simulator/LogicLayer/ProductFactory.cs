using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Fabric de product Factory
    /// </summary>
    public class ProductFactory
    {
        /// <summary>
        /// Instance privée du singleton
        /// </summary>
        private static ProductFactory instance;
        /// <summary>
        /// Instance public du singleton
        /// </summary>
        public static ProductFactory Instance
        {
            get{
            
                if (instance == null)
                {
                    instance = new ProductFactory();
                }
                return instance;
            }
        }

        /// <summary>
        /// Dictionnaire qui associe le nom des produits à leurs objets.
        /// </summary>
        private Dictionary<string, IProductCreator> products = new Dictionary<string, IProductCreator>();

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
            if(products.ContainsKey(productName))
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
        private ProductFactory() { }
        
    }

}
