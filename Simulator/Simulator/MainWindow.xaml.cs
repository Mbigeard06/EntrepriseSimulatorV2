using LogicLayer;
using LogicLayer.Observator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private LogicLayer.Enterprise enterprise;
        private Timer timerSecond;
        private Timer timerWeek;
        public MainWindow()
        {
            InitializeComponent();
            enterprise = new LogicLayer.Enterprise();
            DataContext = enterprise;
            timerSecond = new Timer(TimerSecondTick);
            timerSecond.Change(0, LogicLayer.Constants.TIME_SLICE); 
            timerWeek = new Timer(TimerWeekTick);
            timerWeek.Change(0, LogicLayer.Constants.WEEK_TIME);
            //Subscription of the observer
            this.enterprise.Register(this);

        }

        private void TimerSecondTick(object? data)
        {
            Dispatcher.Invoke(() =>
            {
                // every second, to update screen
                UpdateScreen();
            });
            
        }

        private void TimerWeekTick(object? data)
        {
            Dispatcher.Invoke(() =>
            {
                // nothing to do every week...
            });
        }

        private void EndOfSimulation()
        {
            MessageBox.Show("END OF SIMULATION");
            Close();
        }

        private void UpdateScreen()
        {
            enterprise.UpdateProductions();
            enterprise.UpdateBuying();
            
            bikesProd.Content = enterprise.GetProduction("bike").ToString();
            scootsProd.Content = enterprise.GetProduction("scooter").ToString();
            carsProd.Content = enterprise.GetProduction("car").ToString();

            bikeStock.Content = enterprise.GetStock("bike").ToString();
            scootStock.Content = enterprise.GetStock("scooter").ToString();
            carStock.Content = enterprise.GetStock("car").ToString();

        }

        private void BuyMaterials(object sender, RoutedEventArgs e)
        {
            try
            {
                enterprise.BuyMaterials();
                UpdateScreen();
            }
            catch(LogicLayer.NotEnoughMoney)
            {
                MessageBox.Show("Not enough money to buy materials !");
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void Hire(object sender, RoutedEventArgs e)
        {
            try
            {
                enterprise.Hire();
                UpdateScreen();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void Dismiss(object sender, RoutedEventArgs e)
        {
            try
            {
                enterprise.Dismiss();
                UpdateScreen();
            }
            catch(LogicLayer.NoEmployee)
            {
                MessageBox.Show("There is no employee to dismiss");
            }
            catch(LogicLayer.NotEnoughMoney)
            {
                MessageBox.Show("There is not enough money to puy dismiss bonus");
            }
            catch(LogicLayer.EmployeeWorking)
            {
                MessageBox.Show("You can't dismiss no : employees working");
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void BuildProduct(string s)
        {
            try
            {
                enterprise.MakeProduct(s);
                UpdateScreen();
            }
            catch (LogicLayer.ProductUnknown)
            {
                MessageBox.Show("I don't know how to make " + s);
            }
            catch (LogicLayer.NotEnoughMaterials)
            {
                MessageBox.Show("You do not have suffisent materials to build a "+s);
            }
            catch (LogicLayer.NoEmployee)
            {
                MessageBox.Show("You do not have enough employees to build a "+s);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
        private void BuildBike(object sender, RoutedEventArgs e)
        {
            BuildProduct("bike");
        }

        private void BuildScooter(object sender, RoutedEventArgs e)
        {
            BuildProduct("scooter");
        }

        private void BuildCar(object sender, RoutedEventArgs e)
        {
            BuildProduct("car");
        }

        /// <summary>
        /// Update the corporate money.
        /// </summary>
        /// <param name="money">New amount of money of the the corporate</param>
        public void MoneyChange(int newMoney)
        {
            Dispatcher.Invoke(() =>
            {
                money.Content = newMoney.ToString("C");
            });
        }

        /// <summary>
        /// Update the corporate material.
        /// </summary>
        /// <param name="materials">New amount of materials</param>
        public void MaterialChange(int materials)
        {
            Dispatcher.Invoke(() =>
            {
                this.materials.Content = materials.ToString();
            });
        }

        /// <summary>
        /// Update the corporate number of employees.
        /// </summary>
        /// <param name="free"></param>
        /// <param name="total"></param>
        public void EmployeesChange(int free, int total)
        {
            Dispatcher.Invoke(() =>
            {
                employees.Content = free.ToString() + "/" + total.ToString();
            });
        }

        /// <summary>
        /// Update the corporate stock.
        /// </summary>
        /// <param name="stock">New Stock.</param>
        public void StockChange(int stock)
        {
            Dispatcher.Invoke(() =>
            {
                totalStock.Content = stock.ToString() + " %";
            });
        }

        /// <summary>
        /// Update the client needs.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="need"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ClientNeedsChange(string type, int need)
        {
            Dispatcher.Invoke(() =>
            {
                switch (type)
                {
                    case "bike":
                        bikeAsk.Content = need.ToString(); 
                        break;
                    case "scooter":
                        scootAsk.Content = need.ToString();
                        break;
                    default:
                        carAsk.Content = need.ToString();
                        break;
                }
            });
        }
    }
}
