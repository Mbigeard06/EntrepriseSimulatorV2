using LogicLayer.Fabric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Part of company, who deal with clients needs
    /// </summary>
    public class ClientService
    {
        private Random r;
        private Dictionary<string, int> needs;
        private Dictionary<string, int> demandProbs;

        public ClientService()
        {
            needs = new Dictionary<string, int>();
            demandProbs = new Dictionary<string, int>();
            r = new Random();
            Initialiser.InitClients(this);
        }
        private int ProbaToClients(int proba)
        {
            return (int)(r.NextDouble() * proba);
        }
        /// <summary>
        /// Update clients demands.
        /// </summary>
        public void UpdateClients()
        {
            foreach(string type in demandProbs.Keys)
            {
                needs[type] += ProbaToClients(demandProbs[type]);
            }
        }
        /// <summary>
        /// Get clients needs
        /// </summary>
        /// <param name="type">type of product</param>
        /// <returns>number of potential clients</returns>
        /// <exception cref="ProductUnknown">If product is not known</exception>
        public int GetAskFor(string type)
        {            
            if (!needs.ContainsKey(type))
                throw new ProductUnknown();

            return needs[type];
        }

        /// <summary>
        /// Initialize the demand probabilities.
        /// </summary>
        /// <param name="type">type of the demand</param>
        /// <param name="prob">probability</param>
        public void InitProbs(string type, int prob)
        {
            demandProbs[type] = prob;
        }

        /// <summary>
        /// Tells if a client want to buy the product
        /// </summary>
        /// <param name="type">kind of product</param>
        /// <returns>true if one client want to buy (and can buy)</returns>
        /// <exception cref="ProductUnknown">If type unknown</exception>
        public bool WantToBuy(string type)
        {
            if (!needs.ContainsKey(type))
                throw new ProductUnknown();
            return (r.NextDouble() * needs[type])*10 > 1;
        }

        /// <summary>
        /// A product is bought, so a client do not want to buy anymore
        /// </summary>
        /// <param name="type"></param>
        public void Buy(string type)
        {
            needs[type] -= 10;
            if (needs[type] < 0) needs[type] = 0;
        }

        /// <summary>
        /// Initialize clients need.
        /// </summary>
        /// <param name="type">type of the product</param>
        /// <param name="need">demands</param>
        public void InitNeeds(string type, int need)
        {
            needs[type] = need;
        }

    }
}
