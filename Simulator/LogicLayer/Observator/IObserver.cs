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
    }
}   
