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
        //List of the observers.
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
        /// Notify the observators that the corporate money has changed.
        /// </summary>
        /// <param name="money"></param>
        protected void NotifyMoneyChange(int money)
        {
            foreach (IObserver observer in observers)
            {
                observer.MoneyChange(money);
            }
        }

        /// <summary>
        /// Notify the observators that the corporate stock of materials has changed
        /// </summary>
        /// <param name="material"></param>
        protected void NotifyMaterialChange(int material)
        {
            foreach (IObserver observer in observers)
            {
                observer.MaterialChange(material);
            }
        }

        /// <summary>
        /// Notify the observators that the number of employee has changed.
        /// </summary>
        /// <param name="material"></param>
        protected void NotifyEmployeesChange(int free, int total)
        {
            foreach (IObserver observer in observers)
            {
                observer.EmployeesChange(free, total);
            }
        }

        /// <summary>
        /// Notify the observators that the stock has changed.
        /// </summary>
        /// <param name="stock">New stock</param>
        protected void NotifyStockChange(int stock)
        {
            foreach(IObserver observer in observers)
            {
                observer.StockChange(stock);
            }
        }

        /// <summary>
        /// Notify the observators that the clients needs has changed.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="need"></param>
        protected void NotifyClientNeedsChange(string type, int need)
        {
            foreach(IObserver observer in observers)
            {
                observer.ClientNeedsChange(type, need);
            }
        }
    }
}
