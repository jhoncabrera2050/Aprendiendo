using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Data.SqlClient;
using System.Data;

namespace Aprendiendo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string connectionString = "Data Source=DESKTOP-DKU7QGM\\SQLEXPRESS;Initial Catalog=Neptuno;User ID=jhon;Password=123456";
        public MainWindow()
        {
            InitializeComponent();
            McDataGrid.ItemsSource = ListarProveedores();
        }
        private static List<Proveedor> ListarProveedores()
        {
            List<Proveedor> proveedores = new List<Proveedor>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();
                string query = "usp_ListarProveedores";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Verificar si hay filas
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Leer los datos de cada fila
                                proveedores.Add(new Proveedor
                                {
                                    idProveedor = reader.GetInt32(reader.GetOrdinal("idProveedor")),
                                    nombreCompañia = reader["nombreCompañia"].ToString(),
                                    nombrecontacto = reader["nombrecontacto"].ToString(),
                                    cargocontacto = reader["cargocontacto"].ToString(),
                                    direccion = reader["direccion"].ToString(),
                                    ciudad = reader["ciudad"].ToString(),
                                    region = reader["region"].ToString(),
                                    codPostal = reader["codPostal"].ToString(),
                                    pais = reader["pais"].ToString(),
                                    telefono = reader["telefono"].ToString(),
                                    fax = reader["fax"].ToString(),
                                });
                            }
                        }
                    }
                }

                // Cerrar la conexión
                connection.Close();
            }
            return proveedores;

        }
    }
}
