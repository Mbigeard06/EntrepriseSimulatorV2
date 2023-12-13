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
        /// <param name="materiels">New number of materiels</param>
        public void MaterialChange(int materials);

        /// <summary>
        /// Trigered when the numbrer of employees changes.
        /// </summary>
        /// <param name="free">Employees free</param>
        /// <param name="total">Total of employees</param>
        public void EmployeesChange(int free, int total);

        /// <summary>
        /// Trigered when the global stock changes.
        /// </summary>
        /// <param name="stock">New stock</param>
        public void StockChange(int stock);

        /// <summary>
        /// Triggered when the clients needs changes.
        /// </summary>
        /// <param name="type">Type of the need</param>
        /// <param name="need">New need</param>
        public void ClientNeedsChange(string type, int need);

        /// <summary>
        /// Triggered when the production of a product is done
        /// </summary>
        /// <param name="product">Product whose production is done</param>
        /// <returns>The Product</returns>
        public void ProductProductionDone(Product product);

        /// <summary>
        /// Trigered when the production of a product starts.
        /// </summary>
        /// <param name="product">Product whose production has started.</param>
        /// <returns>The product</returns>
        public void ProductProductionStart(Product product);

        /// <summary>
        /// Trigered when the stock of a product changes.
        /// </summary>
        /// <param name="productType">Product type whose stock changed.</param>
        public void ProductStockChange(string productType);
    }
}
