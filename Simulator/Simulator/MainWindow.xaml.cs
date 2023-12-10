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
using static System.Net.Mime.MediaTypeNames;

namespace Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private LogicLayer.Enterprise enterprise;
        private Timer timerSecond;
        public MainWindow()
        {
            InitializeComponent();
            enterprise = new LogicLayer.Enterprise();
            DataContext = enterprise;
            timerSecond = new Timer(TimerSecondTick);
            timerSecond.Change(0, LogicLayer.Constants.TIME_SLICE); 
            //Subscription of the observer
            this.enterprise.Register(this);
            InitPanelBuild();
            InitPanelProd();
        }

        private void TimerSecondTick(object? data)
        {
            Dispatcher.Invoke(() =>
            {
                // every second, to update screen
                UpdateScreen();
            });
            
        }

        private void EndOfSimulation()
        {
            MessageBox.Show("END OF SIMULATION");
            Close();
        }

        private void UpdateScreen()
        {
            enterprise.UpdateBuying();

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
        /// <summary>
        /// Initialize main Window panel.
        /// </summary>
        private void InitPanelBuild()
        {
            //On récupère la liste des produits
            String[] products = this.enterprise.NamesOfProducts;
            foreach (string type in products) 
            {
                // create a button, with a static style
                Button button = new Button();
                button.Style = System.Windows.Application.Current.TryFindResource("resBtn") as Style;
                // when the button is clicked, we call BuildProduct with the good type
                button.Click += (sender, args) => { BuildProduct(type); };
                // create the stack panel inside the button
                var panel = new StackPanel();
                button.Content = panel;
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                string path = string.Format("pack://application:,,,/Simulator;component/Images/{0}.png", type);
                BitmapImage bmp = new BitmapImage(new Uri(path));
                image.Source = bmp;
                panel.Children.Add(image);
                // create a label, with the good style and add to the panel
                Label label = new Label();
                label.Content = "Build a " + type;
                label.Style = System.Windows.Application.Current.TryFindResource("legend") as Style;
                panel.Children.Add(label);
                // add the button to the parent panel
                panelBuild.Children.Add(button);
            }
        }
        /// <summary>
        /// Initialize the panel prod.
        /// </summary>
        public void InitPanelProd()
        {
            //On récupère la liste des produits
            String[] products = this.enterprise.NamesOfProducts;
            foreach (string type in products)
            {
                // Création de la bordure
                Border border = new Border();
                border.BorderBrush = System.Windows.Media.Brushes.Black;
                border.BorderThickness = new System.Windows.Thickness(1);
                border.Margin = new Thickness(2);

                // Création du StackPanel à l'intérieur de la bordure
                var panel = new StackPanel();

                // Création de l'image
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                // Spécifiez correctement la source de l'image
                image.Source = new BitmapImage(new Uri($"pack://application:,,,/Simulator;component/Images/{type}.png"));
                // Spécifiez éventuellement la taille de l'image
                image.Width = 40; // ajustez la taille selon vos besoins
                // Ajouter l'image au StackPanel
                panel.Children.Add(image);

                // Création d'un label avec le bon style et ajout au StackPanel
                Label label = new Label();
                label.Name = type + "sProd";
                label.Content = "0";
                label.Style = System.Windows.Application.Current.TryFindResource("legend") as Style;
                panel.Children.Add(label);
                // Ajouter le StackPanel à la bordure
                border.Child = panel;

                // Ajouter la bordure au panel parent (panelProd)
                panelProd.Children.Add(border);
            }
        }

        /// <summary>
        /// Update the corporate production when a product production is done.
        /// </summary>
        /// <param name="p">Product done.</param>
        public void ProductProductionDone(Product p)
        {
            string name = p.Name + "sProd";
            Dispatcher.Invoke(() =>
            {
                var test = UIChildFinder.FindChild(panelProd, name, typeof(Label));
                if (test is Label label)
                {
                    label.Content = enterprise.GetProduction(p.Name).ToString();
                }
            });
        }

        /// <summary>
        /// Update the corporate production when a product production has started.
        /// </summary>
        /// <param name="p">Product whose the production has started.</param>
        public void ProductProductionStart(Product p)
        {
            string name = p.Name + "sProd";
            Dispatcher.Invoke(() =>
            {
                var test = UIChildFinder.FindChild(panelProd, name, typeof(Label));
                if (test is Label label)
                {
                    label.Content = enterprise.GetProduction(p.Name).ToString();
                }
            });
        }
    }
}
