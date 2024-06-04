using ConsoleApp1.Database;
using System.Data.SqlClient;

namespace ConsoleApp1.Models
{
    public class Program
    {
        static void Main(string[] args)
        {
            GestorBaseDeDatos gestorBaseDeDatos = new GestorBaseDeDatos();

            try 
            {
                // if(gestorBaseDeDatos.DeleteUser(10))
                //{
                //  Console.WriteLine("usuario eliminado");
                //}
                Usuario newUser = new Usuario("Jonathan", "Rodriguez", "jona22", "123456789", "jonaro@gmail.com");
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}