using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEmployerManager.Models
{
    class DBConnector
    {
        private Configuration conf;
        private ISessionFactory sessionFact;
        private ISession session;

        public DBConnector()
        {
            conf = new Configuration();
            conf.Configure();
            sessionFact = conf.BuildSessionFactory();
        }


        public List<Employers> GetEmployers()
        {
            List<Employers> employers = new List<Employers>();
            using (session = sessionFact.OpenSession())
            {
                employers = session.Query<Employers>().ToList<Employers>();
            }
            return employers;
        }

        public List<Units> GetUnits()
        {
            List<Units> units = new List<Units>();
            using (session = sessionFact.OpenSession())
            {
                units = session.Query<Units>().ToList<Units>();
            }
            return units;
        }

        public List<Orders> GetOrders()
        {
            List<Orders> orders = new List<Orders>();
            using (session = sessionFact.OpenSession())
            {
                orders = session.Query<Orders>().ToList<Orders>();
            }
            return orders;
        }

        public List<Orders> GetOrdersByEntity(EmployerModel em)
        {
            List<Orders> orders = new List<Orders>();
            using (session = sessionFact.OpenSession())
            {
                orders = (List<Orders>)session.QueryOver<Orders>().Where(o => o.Employers_Id == em.Id).List<Orders>();
            }
            return orders;
        }

        public List<Products> GetProducts()
        {
            List<Products> products = new List<Products>();
            return products;
        }

        public List<Products> GetProductsByEntity(OrderModel om)
        {
            List<Products> products = new List<Products>();
            using (session = sessionFact.OpenSession())
            {
                products = (List<Products>)session.QueryOver<Products>().Where(p => p.OrdersId == om.Number).List<Products>();
            }
            return products;
        }

        public void AddEmployer(EmployerModel em)
        {
            Employers e = new Employers()
            {
                LastName = em.Surname,
                FirstName = em.Name,
                Patronymic = em.Patronymic,
                Born = em.Born.ToString("yyyy-MM-dd"),
                Gender = em.EmployerGender.ToString(),
                UnitName = em.Unit != null ? em.Unit.Name : ""
            };
            using (session = sessionFact.OpenSession())
            {
                session.Save(e);
                using (var trans = session.BeginTransaction())
                {
                    trans.Commit();
                }
            }
            em.Id = e.Id;
        }

        public void UpdateEmployer(EmployerModel em)
        {
            Employers e = new Employers()
            {
                Id = em.Id,
                LastName = em.Surname,
                FirstName = em.Name,
                Patronymic = em.Patronymic,
                Born = em.Born.ToString("yyyy-MM-dd"),
                Gender = em.EmployerGender.ToString(),
                UnitName = em.Unit.Name
            };
            using (session = sessionFact.OpenSession())
            {
                session.Update(e);
                using (var trans = session.BeginTransaction())
                {
                    trans.Commit();
                }
            }
        }

        public void AddUnit(UnitModel um)
        {
            Units u = new Units()
            {
                UnitName = um.Name,
                HeadId = um.Employer.Id
            };
            using (session = sessionFact.OpenSession())
            {
                session.Save(u);
                using (var trans = session.BeginTransaction())
                {
                    trans.Commit();
                }
            }
            um.Id = u.Id;
        }

        public void UpdateUnit(UnitModel um)
        {
            Units u = new Units()
            {
                Id = um.Id,
                UnitName = um.Name,
                HeadId = um.Employer.Id
            };
            using (session = sessionFact.OpenSession())
            {
                session.Update(u);
                using (var trans = session.BeginTransaction())
                {
                    trans.Commit();
                }
            }
        }

        public void AddOrder(OrderModel om)
        {
            Orders o = new Orders()
            {
                Contractor = om.Contractor,
                Date = om.Date.ToString("yyyy-MM-dd"),
                Employers_Id = om.Employer.Id
            };
            using (session = sessionFact.OpenSession())
            {
                session.Save(o);
                using (var trans = session.BeginTransaction())
                {
                    trans.Commit();
                }
            }
            om.Number = o.OrderId;
        }

        public void UpdateOrder(OrderModel om)
        {
            Orders o = new Orders()
            {
                OrderId = om.Number,
                Contractor = om.Contractor,
                Date = om.Date.ToString("yyyy-MM-dd"),
                Employers_Id = om.Employer.Id
            };
            using (session = sessionFact.OpenSession())
            {
                session.Update(o);
                using (var trans = session.BeginTransaction())
                {
                    trans.Commit();
                }
            }
        }

        public void AddProduct(ProductModel pm)
        {
            Products p = new Products()
            {
                ProductName = pm.Name,
                OrdersId = pm.Order.Number,
                Count = pm.Count,
                Price = pm.Price
            };
            using (session = sessionFact.OpenSession())
            {
                session.Save(p);
                using (var trans = session.BeginTransaction())
                {
                    trans.Commit();
                }
            }
            pm.Id = p.ProductId;
        }

        public void UpdateProduct(ProductModel pm)
        {
            Products p = new Products()
            {
                ProductId = pm.Id,
                OrdersId = pm.Order.Number,
                ProductName = pm.Name,
                Count = pm.Count,
                Price = pm.Price
            };
            using (session = sessionFact.OpenSession())
            {
                session.Update(p);
                using (var trans = session.BeginTransaction())
                {
                    trans.Commit();
                }
            }
        }
    }
}
