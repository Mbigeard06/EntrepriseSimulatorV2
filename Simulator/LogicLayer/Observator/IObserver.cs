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
        /// Trigered when the corporate money evoluate.
        /// </summary>
        /// <param name="money">New amount of money.</param>
        public void MoneyChange(int money);
    }
}
