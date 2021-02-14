using NHibernate.Connection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DBEmployerManager.Commands;
using DBEmployerManager.Models;
using DBEmployerManager.Views;

namespace DBEmployerManager.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private EmployerModel selectedEmployer;
        private OrderModel selectedOrder;
        private UnitModel selectedUnit;
        private ProductModel selectedProduct;
        private ObservableCollection<EmployerModel> employers;
        private ObservableCollection<OrderModel> orders;
        private ObservableCollection<UnitModel> units;
        private ObservableCollection<ProductModel> products;
        private DBConnector connector;
        private ICommand commandAddEmployer;
        private ICommand commandUpdateEmployer;
        private ICommand commandAddUnit;
        private ICommand commandUpdateUnit;
        private ICommand commandAddOrder;
        private ICommand commandUpdateOrder;
        private ICommand commandAddProduct;
        private ICommand commandUpdateProduct;

        public EmployerModel SelectedEmployer { 
            get { return selectedEmployer; } 
            set {
                if (value != null)
                    selectedEmployer = new EmployerModel()
                    {
                        Id = value.Id,
                        Surname = value.Surname,
                        Name = value.Name,
                        Patronymic = value.Patronymic,
                        Born = value.Born,
                        EmployerGender = value.EmployerGender,
                        Unit = value.Unit
                    };
                OnPropertyChanged("SelectedEmployer");
            }
        }

        public OrderModel SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                if (value != null)
                    selectedOrder = new OrderModel()
                    {
                        Number = value.Number,
                        Contractor = value.Contractor,
                        Date = value.Date,
                        Employer = value.Employer
                    };
                OnPropertyChanged("SelectedOrder");
            }
        }

        public UnitModel SelectedUnit
        {
            get { return selectedUnit; }
            set
            {
                if (value != null)
                    selectedUnit = new UnitModel()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Employer = value.Employer
                    };
                OnPropertyChanged("SelectedUnit");
            }
        }

        public ProductModel SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                if (value != null)
                    selectedProduct = new ProductModel()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Order = value.Order,
                        Count = value.Count,
                        Price = value.Price
                    };
                OnPropertyChanged("SelectedProduct");
            }
        }

        public ObservableCollection<EmployerModel> Employers { get { return employers; } set { employers = value; OnPropertyChanged("Employers"); } }
        public ObservableCollection<OrderModel> Orders { get { return orders; } set { orders = value; OnPropertyChanged("Orders"); } }
        public ObservableCollection<UnitModel> Units { get { return units; } set { units = value; OnPropertyChanged("Units"); } }
        public ObservableCollection<ProductModel> Products { get { return products; } set { products = value; OnPropertyChanged("Products"); } }
        public ICommand CommandAddEmployer { get { return commandAddEmployer ?? (commandAddEmployer = new RelayCommand(AddEmployer, CanExecute)); } }
        public ICommand CommandUpdateEmployer { get { return commandUpdateEmployer ?? (commandUpdateEmployer = new RelayCommand(UpdateEmployer, CanExecute)); } }
        public ICommand CommandAddUnit { get { return commandAddUnit ?? (commandAddUnit = new RelayCommand(AddUnit, CanExecute)); } }
        public ICommand CommandUpdateUnit { get { return commandUpdateUnit ?? (commandUpdateUnit = new RelayCommand(UpdateUnit, CanExecute)); } }
        public ICommand CommandAddOrder { get { return commandAddOrder ?? (commandAddOrder = new RelayCommand(AddOrder, CanExecute)); } }
        public ICommand CommandUpdateOrder { get { return commandUpdateOrder ?? (commandUpdateOrder = new RelayCommand(UpdateOrder, CanExecute)); } }
        public ICommand CommandAddProduct { get { return commandAddProduct ?? (commandAddProduct = new RelayCommand(AddProduct, CanExecute)); } }
        public ICommand CommandUpdateProduct { get { return commandUpdateProduct ?? (commandUpdateProduct = new RelayCommand(UpdateProduct, CanExecute)); } }

        public MainViewModel()
        {
            Employers = new ObservableCollection<EmployerModel>();
            Units = new ObservableCollection<UnitModel>();
            Orders = new ObservableCollection<OrderModel>();
            Products = new ObservableCollection<ProductModel>();
            SelectedEmployer = new EmployerModel() { Born = DateTime.Now };
            SelectedUnit = new UnitModel();
            SelectedOrder = new OrderModel();
            SelectedProduct = new ProductModel();

            connector = new DBConnector();

            // Recover employers data from database
            //
            List<Employers> ems = connector.GetEmployers();
            foreach (Employers e in ems)
            {
                EmployerModel em = new EmployerModel()
                {
                    Id = e.Id,
                    Surname = e.LastName,
                    Name = e.FirstName,
                    Patronymic = e.Patronymic,
                    Born = DateTime.Parse(e.Born),
                    EmployerGender = (Gender)Enum.Parse(typeof(Gender), e.Gender),
                    Unit = new UnitModel() { Name = e.UnitName }
                };
                Employers.Add(em);
            }

            // Recover units data from database and Refer Unit's Employer to Employer
            //
            List<Units> uts = connector.GetUnits();
            foreach (Units u in uts)
            {
                UnitModel um = new UnitModel()
                {
                    Id = u.Id,
                    Name = u.UnitName,
                    Employer = null
                };
                foreach (EmployerModel em in Employers)
                {
                    if (em.Id == u.HeadId)
                    {
                        um.Employer = em;
                        break;
                    }
                }
                Units.Add(um);
            }

            // Refer Employer's Unit to Unit
            //
            foreach (EmployerModel em in Employers)
            {
                em.Unit = em.Unit.Name != "" ? Units.Single(u => em.Unit.Name == u.Name) : null;
            }
        }

        public void SetDBConnection()
        {
            DriverConnectionProvider DCP = new DriverConnectionProvider();
            DCP.GetConnectionString();

        }

        public void AddEmployer(object parameter)
        {
            EmployerView EV = (EmployerView)parameter;
            if (EV.Gender != "")
            {
                EmployerModel em = new EmployerModel()
                {
                    Surname = EV.Surname,
                    Name = EV.Name,
                    Patronymic = EV.Patronymic,
                    Born = EV.Born,
                    EmployerGender = (Gender)Enum.Parse(typeof(Gender), EV.Gender),
                    Unit = EV.Unit != null ? (UnitModel)EV.Unit : null
                };
                connector.AddEmployer(em);
                Employers.Add(em);
            }
        }

        public void UpdateEmployer(object parameter)
        {
            EmployerView EV = (EmployerView)parameter;
            if (EV.Gender != "")
            {
                EmployerModel uem = Employers.Single(e => e.Id == SelectedEmployer.Id);
                uem.Surname = EV.Surname;
                uem.Name = EV.Name;
                uem.Patronymic = EV.Patronymic;
                uem.Born = EV.Born;
                uem.EmployerGender = (Gender)Enum.Parse(typeof(Gender), EV.Gender);
                uem.Unit = EV.Unit != null ? (UnitModel)EV.Unit : null;
                connector.UpdateEmployer(uem);
            }
        }


        public void AddUnit(object parameter)
        {
            UnitView UV = (UnitView)parameter;
            if (UV.Name != "" && UV.Employer != null)
            {
                UnitModel UM = new UnitModel()
                {
                    Name = UV.Name,
                    Employer = (EmployerModel)UV.Employer
                };
                connector.AddUnit(UM);
                Units.Add(UM);
            }
        }

        public void UpdateUnit(object parameter)
        {
            UnitView UV = (UnitView)parameter;
            if (UV.Name != "" && UV.Employer != null)
            {
                UnitModel um = Units.Single(u => u.Id == SelectedUnit.Id);
                um.Name = UV.Name;
                um.Employer = (EmployerModel)UV.Employer;
                connector.UpdateUnit(um);
            }
        }

        public void AddOrder(object parameter)
        {
            OrderView OV = (OrderView)parameter;
            if (OV.Contractor != "" && SelectedEmployer != null)
            {
                OV.Employer = SelectedEmployer;
                OrderModel OM = new OrderModel()
                {
                    Contractor = OV.Contractor,
                    Date = OV.Date,
                    Employer = (EmployerModel)OV.Employer
                };
                connector.AddOrder(OM);
                Orders.Add(OM);
            }
        }

        public void UpdateOrder(object parameter)
        {
            OrderView OV = (OrderView)parameter;
            if (OV.Contractor != "" && SelectedEmployer != null)
            {
                OrderModel om = Orders.Single(o => o.Number == SelectedOrder.Number);
                om.Contractor = OV.Contractor;
                om.Date = OV.Date;
                connector.UpdateOrder(om);
            }
        }

        public void AddProduct(object parameter)
        {
            ProductView PV = (ProductView)parameter;
            if (PV.Name != "" && SelectedOrder != null)
            {
                PV.Order = SelectedOrder;
                ProductModel PM = new ProductModel()
                {
                    Name = PV.Name,
                    Order = (OrderModel)PV.Order,
                    Count = PV.Count,
                    Price = PV.Price
                };
                connector.AddProduct(PM);
                Products.Add(PM);
            }
        }

        public void UpdateProduct(object parameter)
        {
            ProductView PV = (ProductView)parameter;
            if (PV.Name != "" && SelectedOrder != null)
            {
                ProductModel pm = Products.Single(p => p.Id == SelectedProduct.Id);
                pm.Name = PV.Name;
                pm.Count = PV.Count;
                pm.Price = PV.Price;
                connector.UpdateProduct(pm);
            }
        }

        public void LoadOrders()
        {
            Orders.Clear();
            Products.Clear();
            if (SelectedEmployer != null)
            {
                List<Orders> ors = connector.GetOrdersByEntity(SelectedEmployer);
                foreach (Orders o in ors)
                {
                    OrderModel om = new OrderModel()
                    {
                        Number = o.OrderId,
                        Contractor = o.Contractor,
                        Date = DateTime.Parse(o.Date),
                        Employer = Employers.Single(e => e.Id == o.Employers_Id)
                    };
                    Orders.Add(om);
                }
            }
        }

        public void LoadProducts()
        {
            Products.Clear();
            if (SelectedOrder != null)
            {
                List<Products> pros = connector.GetProductsByEntity(SelectedOrder);
                foreach (Products p in pros)
                {
                    ProductModel pm = new ProductModel()
                    {
                        Id = p.ProductId,
                        Order = Orders.Single(o => o.Number == p.OrdersId),
                        Name = p.ProductName,
                        Count = p.Count,
                        Price = p.Price
                    };
                    Products.Add(pm);
                }
            }
        }

        private bool CanExecute()
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string pn)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(pn));
            }
        }
    }
}
