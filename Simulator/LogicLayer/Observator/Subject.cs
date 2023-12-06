using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Observator
{
    /// <summary>
    /// Subject class of the observator
    /// </summary>
    public class Subject
    {
        private List<IObserver> observers;
        public Subject() 
        {
            observers = new List<IObserver>();
        }
        /// <summary>
        /// Register a new observer
        /// </summary>
        /// <param name="observer"></param>
        public void Register(IObserver observer)
        {
            observers.Add(observer);
        }
        /// <summary>
        /// Unregistred an observer.
        /// </summary>
        /// <param name="observer"></param>
        public void Unregister(IObserver observer)
        {
            observers.Remove(observer);
        }

        /// <summary>
        /// Notify the observator that the corporate money has changed.
        /// </summary>
        /// <param name="money"></param>
        protected void NotifyMoneyChange(int money)
        {
            foreach (IObserver observer in observers)
            {
                observer.MoneyChange(money);
            }
        }
    }
}
