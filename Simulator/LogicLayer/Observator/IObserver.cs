using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Observator
{
    /// <summary>
    /// Interface d'bserver de l'argent de l'entreprise
    /// </summary>
    public interface IObserver
    { 
        ///
        public void MoneyChange(int money);
    }
}
