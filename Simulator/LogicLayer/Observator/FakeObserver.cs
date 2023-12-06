using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Observator
{
    /// <summary>
    /// Fake observer to accomplish a test.
    /// </summary>
    public class FakeObserver : IObserver
    {
        private int materials = 0;
        public int Materials { get => materials; }
        private int money = 0;
        public int Money { get => money; }
        private int employees = 4;
        public int Employees { get => employees; } 
        public void EmployeesChange(int free, int total)
        {
            this.employees = total;
        }

        public void MaterialChange(int materials)
        {
            this.materials = materials;
        }

        public void MoneyChange(int money)
        {
            this.money = money;
        }
    }
}
