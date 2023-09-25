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
namespace Listado
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
            DateTime fechaInicio = new DateTime(2023, 1, 1); // Cambia esta fecha según tus necesidades
            DateTime fechaFin = new DateTime(2023, 12, 31); // Cambia esta fecha según tus necesidades

            Pedidos pedidos = ListarPedidos(string fechaInicio, string fechaFin);

            // Asigna la lista de detalles de pedido a tu DataGrid
            McDataGrid.ItemsSource = pedidos.Detalles;
        }

        private static List<Pedidos> ListarPedidos(string fechainicio, string fechafin)
        {
            List<Pedidos> pedidos = new List<Pedidos>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();
                string query = "usp_ListarDetallesPedidosPorIntervaloFechas";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        SqlParameter parameter = new SqlParameter("@FechaInicio", fechainicio);
                        SqlParameter parameter2 = new SqlParameter("@FechaFin", fechafin);
                        command.Parameters.Add(parameter);
                        command.Parameters.Add(parameter2);
                        command.CommandType = CommandType.StoredProcedure;
                        // Verificar si hay filas
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Leer los datos de cada fila
                                DetallePedido detalle = new DetallePedido
                                {
                                    IdPedido = reader.GetInt32(reader.GetOrdinal("idpedido")),
                                    IdProducto = reader.GetInt32(reader.GetOrdinal("idproducto")),
                                    PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("preciounidad")),
                                    Cantidad = reader.GetInt32(reader.GetOrdinal("cantidad")),
                                    Descuento = reader.GetDecimal(reader.GetOrdinal("descuento"))
                                };

                                pedidos.Detalles.Add(detalle);
                            }
                        }
                    }
                }

                // Cerrar la conexión
                connection.Close();
            }
            return pedidos;

        }
    }
}
