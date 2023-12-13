using LogicLayer.Fabric;
using LogicLayer.Observator;
using LogicLayer.Products;

namespace LogicLayer
{
    /// <summary>
    /// Enterprise simulation
    /// </summary>
    public class Enterprise : Subject, IObserver
    {
        #region associations
        private Workshop workshop;
        private Stock stock;
        private ClientService clients;
        private ProductFactory productFactory;
        private System.Threading.Timer timerMonth;
        private System.Threading.Timer timerSecond;
        #endregion

        #region Properties 
        private int money;
        /// <summary>
        /// Gets the amount of money that enterprise disposes
        /// </summary>
        public int Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
                //Notify the changes to the observator.
                NotifyMoneyChange(value);
            }
        }
        private int materials;
        /// <summary>
        /// Gets the amount of materials that enterprise disposes
        /// </summary>
        public int Materials
        {
            get
            {
                return materials;
            }
            set
            {
                materials = value;
                //Notify the changes to the observator.
                NotifyMaterialChange(value);
            }
        }
        private int employees;
        /// <summary>
        /// Gets the number of employees
        /// </summary>
        public int Employees
        {
            get
            {
                return employees;
            }
            set
            {
                employees = value;
                //Notify the changes to the observator.
                NotifyEmployeesChange(FreeEmployees, value);
            }
        }
        /// <summary>
        /// Gets the number of free employees (they can work)
        /// </summary>
        public int FreeEmployees
        {
            get => employees - EmployeesWorkshop;
        }

        /// <summary>
        /// Gets the number of employees working in the workshop
        /// </summary>
        public int EmployeesWorkshop { get => workshop.NbEmployees; }

        /// <summary>
        /// Gets the total amount of stock
        /// </summary>
        public int TotalStock { get => stock.TotalStock; }

        /// <summary>
        /// Returns the name of all the products.
        /// </summary>
        public string[] NamesOfProducts
        {
            get => this.productFactory.Products;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize the enterprise
        /// </summary>
        public Enterprise()
        {
            money = 300000;
            employees = 4;
            materials = 100;
            workshop = new Workshop();
            stock = new Stock();
            clients = new ClientService();
            clients.Register(this);
            productFactory = new ProductFactory();
            timerMonth = new Timer(EndOfMonth);
            timerMonth.Change(0, LogicLayer.Constants.MONTH_TIME);
            timerSecond = new Timer(TimerSecondTick);
            timerSecond.Change(0, LogicLayer.Constants.TIME_SLICE);
        }
        #endregion

        #region methods
        /// <summary>
        /// Update the buying status every second.
        /// </summary>
        /// <param name="data"></param>
        private void TimerSecondTick(object? data)
        {
            UpdateBuying();
        }

        /// <summary>
        /// Buy some materials
        /// </summary>
        /// <exception cref="NotEnoughMoney">If insufisant funds</exception>
        public void BuyMaterials()
        {
            int cost = Constants.MATERIALS * Constants.COST_MATERIALS;
            if (money < cost)
                throw new NotEnoughMoney();
            Money -= cost;
            Materials += Constants.MATERIALS;
        }

        /// <summary>
        /// Hire a new emloyee
        /// </summary>        
        public void Hire()
        {
            ++Employees;
        }

        /// <summary>
        /// DIsmiss an employee
        /// </summary>
        /// <exception cref="NoEmployee">If no employee to dismiss</exception>
        /// <exception cref="NotEnoughMoney">If not enough money to pay the bonus</exception>
        /// <exception cref="EmployeeWorking">If all employees worked, no dismiss is possible</exception>
        public void Dismiss()
        {
            if (employees < 1) throw new NoEmployee();
            int cost = Constants.BONUS;
            if (money < cost)
                throw new NotEnoughMoney();
            if (FreeEmployees < 1)
                throw new EmployeeWorking();
            Money -= cost;
            Employees--;
        }

        /// <summary>
        /// Start a product production
        /// </summary>
        /// <param name="type">a string identifying kind of product</param>
        /// <exception cref="ProductUnknown">the type is unknown</exception>
        /// <exception cref="NotEnoughMaterials">Not enough materials to build</exception>
        /// <exception cref="NoEmployee">Not enough employee to build</exception>
        public void MakeProduct(string type)
        {
            Product p;
            try
            {
                p = productFactory.Creer(type);
            }
            catch
            {
                throw new ProductUnknown();
            }
            // test if the product can be build
            if (Materials < p.MaterialsNeeded)
                throw new NotEnoughMaterials();
            if (Employees - EmployeesWorkshop < p.EmployeesNeeded)
                throw new NoEmployee();

            Materials -= p.MaterialsNeeded; // consume materials
            // start the building...
            workshop.StartProduction(p, this);
            NotifyProductionStart(p);
        }

        /// <summary>
        /// Update the productions & the stock
        /// </summary>
        /// <exception cref="UnableToStock">If stock is full</exception>
        public void UpdateProductions()
        {
            // update informations about productions
            var list = workshop.ProductsDone();
            // add finish products in stock
            foreach (var product in list)
            {
                stock.Add(product);
                workshop.Remove(product);
                //Update the product stock
                NotifyProductStockChange(product.Name);
            }
            //Mise à jour du stock total
            NotifyStockChange(this.TotalStock);
        }

        /// <summary>
        /// Get the numbers of products of a type workshop build
        /// </summary>
        /// <param name="v">kind of product</param>
        /// <returns>number of products building</returns>        
        public int GetProduction(string v)
        {
            return workshop.InProduction(v);
        }

        /// <summary>
        /// Gets the number of products stocked
        /// </summary>
        /// <param name="v">type of product</param>
        /// <returns>number stocked</returns>
        public int GetStock(string v)
        {
            return stock.GetNbOfType(v);
        }

        /// <summary>
        /// Pay all the employees
        /// </summary>
        /// <exception cref="NotEnoughMoney">if money is not enough !</exception>
        public void PayEmployees()
        {
            int cost = employees * Constants.SALARY;
            if (cost > money)
                throw new NotEnoughMoney();
            Money -= cost;
        }

        /// <summary>
        /// Update the buying status
        /// </summary>
        public void UpdateBuying()
        {
            foreach (string product in productFactory.Products)
            {
                if (clients.WantToBuy(product)) //Some clients want to buy the product.
                {
                    TrySell(product);
                }
            }
        }

        /// <summary>
        /// Try to sell a product of a certain type.
        /// </summary>
        /// <param name="type">Product type.</param>
        private void TrySell(string type)
        {
            Product? p = stock.Find(type);
            if (p != null)
            {
                stock.Remove(p);
                Money += p.Price;
                clients.Buy(type);
                //Notify the observers of the changes.
                NotifyStockChange(this.TotalStock);
                NotifyProductStockChange(type);
            }
        }

        /// <summary>
        /// update client needs
        /// </summary>
        public void UpdateClients()
        {
            clients.UpdateClients();
        }

        /// <summary>
        /// Get clients needs
        /// </summary>
        /// <param name="type">type of product clients wanted</param>
        /// <returns>number of potential clients</returns>
        /// <exception cref="ProductUnknown">If type unknown</exception>
        public int GetAskClients(string type)
        {
            return clients.GetAskFor(type);
        }

        /// <summary>
        /// Function trigered at every end of month.
        /// </summary>
        /// <param name="state"></param>
        private void EndOfMonth(object? state)
        {
            PayEmployees();
            UpdateClients();
        }

        /// <summary>
        /// Notify the corporate observers that the amount of money has changed.
        /// </summary>
        /// <param name="money"></param>
        public void MoneyChange(int money)
        {
            NotifyMoneyChange(money);
        }

        /// <summary>
        /// Notify the corporate observers that the amount of materiels has changed.
        /// </summary>
        /// <param name="materials"></param>
        public void MaterialChange(int materials)
        {
            MaterialChange(materials);
        }

        /// <summary>
        /// Notify the corporate observer that the number of employees has changed.
        /// </summary>
        /// <param name="free">Employee that are not working.</param>
        /// <param name="total">Total of employees.</param>
        public void EmployeesChange(int free, int total)
        {
            NotifyEmployeesChange(free, total);
        }

        /// <summary>
        /// Notify the corporate observer that the amount of money has changed.
        /// </summary>
        /// <param name="stock">New stock.</param>
        public void StockChange(int stock)
        {
            NotifyStockChange(stock);
        }

        /// <summary>
        /// Notify the corporate observers that the clients needs has changed.
        /// <param name="type">Type of the need that changed.</param>
        /// <param name="need">New need.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ClientNeedsChange(string type, int need)
        {
            NotifyClientNeedsChange(type, need);
        }

        /// <summary>
        /// Notify the corporate observers that the production of a product is done.
        /// </summary>
        /// <param name="product">Product producted.</param>
        public void ProductProductionDone(Product product)
        {
            // update informations about productions
            var list = workshop.ProductsDone();
            // add finish products in stock
            foreach (var prod in list)
            {
                stock.Add(prod);
                workshop.Remove(prod);
            }
            //Notify the observors that the stock has changed
            NotifyProductStockChange(product.Name);
            NotifyProductionDone(product);
        }

        /// <summary>
        /// Notify the corporate observers that the production of a product has just started.
        /// </summary>
        /// <param name="product">Product whose production started.</param>
        public void ProductProductionStart(Product product)
        {
            NotifyProductionStart(product);
        }

        /// <summary>
        /// Notify the corporate that the stock of a product changed.
        /// </summary>
        /// <param name="productType">Type of the product.</param>
        public void ProductStockChange(string productType)
        {
            NotifyProductStockChange(productType);
        }

        /// <summary>
        /// Destructor of the class.
        /// </summary>
        ~Enterprise()
        {
            timerMonth.Dispose();
            timerSecond.Dispose();
        }

        #endregion

    }
}