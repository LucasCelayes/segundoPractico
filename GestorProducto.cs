using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Database
{
    internal class GestorProducto
    {
        private string connectionString;

        public GestorProducto()
        {
            connectionString = @"Server=localhost\SQLEXPRESS;Database=SistemaLogistica;Trusted_Connection=true;";
        }
        public bool DeleteProduct(int Id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Producto WHERE ID = @id";
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                command.Parameters.AddWithValue("id", Id);
                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool CreateProduct(Producto Producto)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string query = "INSERT INTO Producto(Descripciones,Costo,PrecioVenta,Stock,IdUsuario) values(@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("Descripciones", Producto.Descripciones);
                command.Parameters.AddWithValue("Costo", Producto.Costo);
                command.Parameters.AddWithValue("PrecioVenta", Producto.PrecioVenta);
                command.Parameters.AddWithValue("Stock", Producto.Stock);
                command.Parameters.AddWithValue("IdUsuario", Producto.IdUsuario);

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }


        }
        public Producto GetUserById(int id)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string query = "SELECT * FROM Producto WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int Id = Convert.ToInt32(reader["id"]);
                    string descripciones = reader["descripciones"].ToString();
                    int costo = Convert.ToInt32(reader["costo"]);
                    int precioVenta = Convert.ToInt32(reader["precioVenta"]);
                    double stock = Convert.ToDouble(reader["stock"]);
                    int idUsuario = Convert.ToInt32(reader["IdUsuario"]);

                    Producto producto = new Producto(Id, descripciones, costo, precioVenta, stock, idUsuario);
                
                    return producto;
                }
                throw new Exception("Id no encontrado");
            }
        
        }

        public bool UpdateProduct(int id, Producto producto)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string query = "UPDATE Producto SET Descripciones = @descripciones,  = @Costo,costo = @Precioventa, precioVenta = @Stock,stock = ,@IdUsuario, idUsuario WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("Descripciones", producto.Descripciones);
                command.Parameters.AddWithValue("Costo", producto.Costo);
                command.Parameters.AddWithValue("PrecioVenta", producto.PrecioVenta);
                command.Parameters.AddWithValue("Stock", producto.Stock);
                command.Parameters.AddWithValue("IdUsuario", producto.IdUsuario);
                command.Parameters.AddWithValue("id", id);

                connection.Open();
                return command.ExecuteNonQuery() > 0;

            }
        }
    

            public List<Producto> ListaProductos()
            {
                List<Producto> listaProductos = new List<Producto>();
                string query = "SELECT * FROM Producto";
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                
                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Descripciones = dataReader["Descripiones"].ToString();
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Stock = Convert.ToDouble(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                listaProductos.Add(producto);
                            }
                        }
                    }
                }
                return listaProductos;
            }
        }
    }
