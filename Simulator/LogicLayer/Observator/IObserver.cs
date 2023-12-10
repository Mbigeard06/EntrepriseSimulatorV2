using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Observator
{
    /// <summary>
    /// Observer interface of the corporate money
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// Trigered when the corporate money changes.
        /// </summary>
        /// <param name="money">New amount of money.</param>
        public void MoneyChange(int money);

        /// <summary>
        /// Trigered when the corporate stock of materiels changes.
        /// </summary>
        /// <param name="materiels"></param>
        public void MaterialChange(int materials);

        /// <summary>
        /// Trigered when the numbrer of employees changes.
        /// </summary>
        /// <param name="free"></param>
        /// <param name="total"></param>
        public void EmployeesChange(int free, int total);

        /// <summary>
        /// Trigered when the stock changes.
        /// </summary>
        /// <param name="stock"></param>
        public void StockChange(int stock);

        /// <summary>
        /// Triggered when the clients needs changes.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="need"></param>
        public void ClientNeedsChange(string type, int need);

        /// <summary>
        /// Trigered when the production of a product is done.
        /// </summary>
        /// <returns>The Product</returns>
        public void ProductProductionDone(Product product);

        /// <summary>
        /// Trigered when the production of a product starts.
        /// </summary>
        /// <returns>The product</returns>
        public void ProductProductionStart(Product product);
    }
}   
