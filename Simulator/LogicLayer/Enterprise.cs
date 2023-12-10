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
        private System.Threading.Timer timer;
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
                NotifyEmployeesChange(FreeEmployees,value);
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
            timer = new Timer(EndOfMonth);
            timer.Change(0, LogicLayer.Constants.MONTH_TIME);
        }
        #endregion

        #region methods
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
            ProductProductionStart(p);

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
            }
            //Mise à jour du stock
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
                if (clients.WantToBuy(product)) //Des clients veulent acheter le produit
                {
                    TrySell(product);
                }
            }
        }

        private void TrySell(string type)
        {
            Product? p = stock.Find(type);
            if (p != null)
            {
                stock.Remove(p);
                Money += p.Price;
                clients.Buy(type);
            }
            //Mise à jour du stock
            NotifyStockChange(this.TotalStock);

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

        private void EndOfMonth(object? state)
        {
            PayEmployees();
            UpdateClients();
        }

        /// <summary>
        /// Notify the corporate observer that the amount of money has changed.
        /// </summary>
        /// <param name="money"></param>
        public void MoneyChange(int money)
        {
            NotifyMoneyChange(money);
        }

        /// <summary>
        /// Notify the corporate observer that the amount of materiels has changed.
        /// </summary>
        /// <param name="materials"></param>
        public void MaterialChange(int materials)
        {
            MaterialChange(materials);
        }

        /// <summary>
        /// Notify the corporate observer that the number of employees has changed.
        /// </summary>
        /// <param name="free"></param>
        /// <param name="total"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void EmployeesChange(int free, int total)
        {
            NotifyEmployeesChange(free, total);
        }

        /// <summary>
        /// Notify the corporate observer that the amount of money has changed.
        /// </summary>
        /// <param name="stock"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void StockChange(int stock)
        {
            NotifyStockChange(stock);
        }

        /// <summary>
        /// Notify the corporate observers that the clients needs has changed.
        /// <param name="type"></param>
        /// <param name="need"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ClientNeedsChange(string type, int need)
        {
            NotifyClientNeedsChange(type, need);
        }

        /// <summary>
        /// Notify the corporate observers that the production of a product is done.
        /// </summary>
        /// <param name="product"></param>
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
            NotifyProductionDone(product);
        }

        /// <summary>
        /// Notify the corporate observers that the production of a product has just started.
        /// </summary>
        /// <param name="product"></param>
        public void ProductProductionStart(Product product)
        {
            NotifyProductionStart(product);
        }

        /// <summary>
        /// Destructor of the class.
        /// </summary>
        ~Enterprise()
        {
            timer.Dispose();
        }

        #endregion



    }
}