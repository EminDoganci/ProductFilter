using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConsoleApp1

{
    public interface IProductDal
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        List<Product> Find(string productName);
        void Create(Product p);
        void Update(Product p);
        void Delete(int productId);
    }

    public class MySQLProductDal : IProductDal
    {
        private MySqlConnection GetMySqlConnection()
        {
            string connectionString = @"server = localhost;port = 3306; database= northwind; user = root; password = mysql1234;";

            return new MySqlConnection(connectionString);

        }
        public void Create(Product p)
        {
            throw new NotImplementedException();
        }

        public void Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = null;
            using (var connection = GetMySqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "select*from products";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    products = new List<Product>();

                    while (reader.Read())
                    {
                        products.Add(
                            new Product
                            {
                                ProductId = int.Parse(reader["id"].ToString()),
                                Name = reader["product_name"].ToString(),
                                Price = double.Parse(reader["list_price"].ToString()),
                            }



                            );


                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
                finally
                {
                    connection.Close();
                }
            }

            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = null;
            using (var connection = GetMySqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "select*from products where id=@productid";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.Add("@productid", MySqlDbType.Int32).Value=id;
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        product = new Product()
                        {
                            ProductId = int.Parse(reader["id"].ToString()),
                            Name = reader["product_name"].ToString(),
                            Price = double.Parse(reader["list_price"].ToString()),
                        };
                    }


                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
                finally
                {
                    connection.Close();
                }
            }

            return product;
        }

        public void Update(Product p)
        {
            throw new NotImplementedException();
        }

        public List<Product> Find(string productName)
        {
            throw new NotImplementedException();
        }
    }
    public class MsSQLProductDal : IProductDal
    {
        private SqlConnection GetMsSqlConnection()
        {
            string connectionString = @"Data Source = .\SQLEXPRESS; Initial Catalog = Northwind; Integrated Security=SSPI";

            return new SqlConnection(connectionString);

        }
        public void Create(Product p)
        {
            throw new NotImplementedException();
        }

        public void Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = null;
            using (var connection = GetMsSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "select*from products";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    products = new List<Product>();

                    while (reader.Read())
                    {
                        products.Add(
                            new Product
                            {
                                ProductId = int.Parse(reader["ProductID"].ToString()),
                                Name = reader["ProductName"].ToString(),
                                Price = double.Parse(reader["UnitPrice"].ToString()),
                            }



                            );


                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
                finally
                {
                    connection.Close();
                }
            }

            return products;
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product p)
        {
            throw new NotImplementedException();
        }

        public List<Product> Find(string productName)
        {
            throw new NotImplementedException();
        }
    }
    public class ProductManeger : IProductDal
    {
        IProductDal _productDal;
        public ProductManeger(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Create(Product p)
        {
            throw new NotImplementedException();
        }

        public void Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public List<Product> Find(string productName)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            return _productDal.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return _productDal.GetProductById(id);
        }

        public void Update(Product p)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ProductDal = new ProductManeger(new MySQLProductDal());
            var product = ProductDal.GetProductById(4);
            Console.WriteLine($"{product.Name}");
            Console.ReadLine();
        }






    }


}
