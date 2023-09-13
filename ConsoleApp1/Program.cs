//Librerias del ADO .NET
using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using ConsoleApp1;

class Program
{
    // Cadena de conexión a la base de datos
    public static string connectionString = "Data Source=LAB1504-13\\SQLEXPRESS;Initial Catalog=Tecsup2023;User ID=tecsup;Password=1234";


    static void Main()
    {
        /*
        #region FormaDesconectada
        //Datatable
        DataTable dataTable = ListarEmpleadosDataTable();
       
       
       Console.WriteLine("Lista de Empleados:");
       foreach (DataRow row in dataTable.Rows)
       {
           Console.WriteLine($"ID: {row["IdEmpleado"]}, Nombre: {row["Nombre"]}, Cargo: {row["Cargo"]}");
       }
        #endregion
       */



        #region FormaConectada
        //Datareader
        List<Student> estudiantes = ListarStudentListaObjetos();
        foreach (var item in estudiantes )
        {
            Console.WriteLine($"ID: {item.Id}, Nombre: {item.FirstName}, Apellido: {item.LastName}");
        }
        #endregion


    }

    //De forma conectada
    private static List<Student> ListarStudentListaObjetos()
    {
        List<Student> estudiantes = new List<Student>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT Id,FirstName,LastName FROM Estudiantes";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Verificar si hay filas
                    if (reader.HasRows)
                    {
                        Console.WriteLine("Lista de Estudiantes:");
                        while (reader.Read())
                        {
                            // Leer los datos de cada fila

                            estudiantes.Add(new Student
                            {
                                Id = (int)reader["Id"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            });

                        }
                    }
                }
            }

            // Cerrar la conexión
            connection.Close();


        }
        return estudiantes;

    }


}

