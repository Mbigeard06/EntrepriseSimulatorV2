using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Observator;

namespace TestLogicLayer
{
    public class TestObserver
    {
        [Fact]
        public void testObserverCorporate()
        {
            //Test for the Materials
            Enterprise e = new Enterprise();
            FakeObserver obs = new FakeObserver();
            e.Register(obs);
            int matos = e.Materials;
            e.BuyMaterials();
            //check that the observator is targeted of the changes
            Assert.True(obs.Materials > matos);
            Assert.Equal(obs.Materials, e.Materials);

            //Test for the Materials
            int money = e.Money;
            e.Dismiss();
            //check that the observator is targeted of the changes
            Assert.True(obs.Money < money);
            Assert.Equal(obs.Money, e.Money);

            //Test for the employees
            int employees = e.Employees;
            e.Hire();
            //check that the observator is targeted of the changes
            Assert.True(obs.Employees > employees);
            Assert.Equal(obs.Employees, e.Employees);

            //Test for the stock
            int stock = e.TotalStock;
            e.MakeProduct("bike");
            Thread.Sleep(LogicLayer.Constants.WEEK_TIME);
            e.UpdateProductions();
            //check that the observator is targeted of the changes
            Assert.True(obs.TotalStock > stock);
            Assert.Equal(obs.TotalStock, e.TotalStock);

        }
    }
}
