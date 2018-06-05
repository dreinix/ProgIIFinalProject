using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;

namespace ProgIIFinalProject
{
    class Program
    {
        static SqlConnection con = new SqlConnection();
        static SqlCommand cmd = new SqlCommand();
        static string yearIdentiffier = DateTime.Today.Year.ToString(),currentId = yearIdentiffier[2] +"0"+ yearIdentiffier[3];
        static List<User> userList = new List<User>();
        static List<string> idList = new List<string>();

        static void DBConnect()
        {
           String path = @"Data Source=localhost\inix;Initial Catalog=ProgIIDB;Integrated Security=SSPI;";
           con.ConnectionString = path;
           con.Open();
        }

        static void AddAlumnsToDataBase(int id,string idNacional, string nombre,string apellido,string estado,string carrera,string extran,string fecha)
        {
            DBConnect();
            String query = "Insert into Alumnos values("+ id + "," + "'" + idNacional + "'," + "'" + nombre + "'," + "'" + apellido + "'," +
                "'" + estado + "'," + "'" + carrera + "'," + "'" + extran + "'," + "'" + fecha + "')";
            cmd.CommandText = query;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        static void Menu()
        {
            
            byte opcion;
            string iD;
            Console.Clear();
            Console.SetCursorPosition(5, 0);
            Console.WriteLine("*****Menú*****");
            Console.WriteLine("1. Agregar usuarios \n" +
                "2. Visualizar usuarios \n" +
                "3. Buscar \n" +
                "4. Modificar usuario \n" +
                "5. Eliminar usuarios \n" +
                "6. Salir \n");
            try
            {
                opcion = byte.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                opcion = 0;
            }
            switch (opcion)
            {
                case 1:
                    AgregarUsuario();
                    break;
                case 2:
                    RevisarUsuarios();
                    break;
                case 3:
                    Console.WriteLine("Ingrese el ID o el identificador nacional del usuario: ");
                    iD = Console.ReadLine();
                    BuscarUsuario(iD);
                    break;
                case 4:
                    Console.WriteLine("Ingrese el ID del usuario al que desea modificar algun dato: \n");
                    iD = Console.ReadLine();
                    EditarUsuario(iD);
                    break;
                case 5:
                    Console.WriteLine("Ingrese el ID del usuario al que desea eliminar: \n");
                    iD = Console.ReadLine();
                    EliminarUsuario(iD);
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Gracias por utilizar nuestros servicios");
                    Environment.Exit(0);
                    //Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Opcion invalida,intente de nuevo");
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                    break;
            }
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("es-ES");
            Console.SetWindowSize(Convert.ToInt32(Console.LargestWindowWidth), Convert.ToInt32(Console.LargestWindowHeight));
            Console.WindowTop = 0;
            Console.SetWindowPosition(0, 0);
            Menu();
        }
        static void gotoXY(string word,int x,int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(word);
        }
        static string GenerarID()
        {   
            string iD = "";
            Random r1 = new Random();
            iD = currentId + "" + r1.Next(0, 10000);
            foreach (string cid in idList)
            {
                if (cid == iD)
                {
                    GenerarID();
                    break;
                }
            }
            return iD;
        }
        static void AgregarUsuario()
        {
            int xPosition = 0,option;
            Console.Clear();
            User usuario = new User();
            String nombre, apellido, estado, ID="000000", carrera, identificador;
            bool extrangero;
            DateTime fechaNacimiento;
            gotoXY("-Nombre: ", xPosition, 1);
            xPosition += 8;
            Console.SetCursorPosition(xPosition, 1);
            nombre  = Console.ReadLine();
            xPosition += nombre.Length + 1;

            gotoXY("-Apellido: ", xPosition, 1);
            xPosition += 10;
            Console.SetCursorPosition(xPosition, 1);
            apellido = Console.ReadLine();
            xPosition += apellido.Length + 1;

            ID = GenerarID();

            gotoXY("-Carrera: ", xPosition, 1);
            xPosition += 9;
            Console.SetCursorPosition(xPosition, 1);
            carrera = Console.ReadLine();
            xPosition += carrera.Length + 1;

            gotoXY("-Identificador nacional: ", xPosition, 1);
            xPosition += 26;
            Console.SetCursorPosition(xPosition, 1);
            identificador = Console.ReadLine();
            xPosition =0;

            gotoXY("-Fecha de nacimiento dd/mm/yyyy: ", xPosition, 2);
            xPosition += 33;
            Console.SetCursorPosition(xPosition, 2);
            try { fechaNacimiento = DateTime.Parse(Console.ReadLine()); }
            catch (Exception)
            {
               fechaNacimiento = Convert.ToDateTime("01/01/1999");
                Console.WriteLine("Formato de fecha invalido. Se establecera una fecha predeterminada");
                Console.ReadKey();
            }
            xPosition += fechaNacimiento.ToString().Length + 1;

            Console.Clear();
            gotoXY("-Nacionalidad dominicana? \n" +
                "1.Si \n" +
                "2.No",0,1);
            Console.SetCursorPosition(27, 1);
            try
            {
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        extrangero = true;
                        break;
                    case 2:
                        extrangero = false;
                        break;
                    default:
                        gotoXY("Error en la selección. El usuario será identificado como extranjero", 0, 6);
                        extrangero = true;
                        Console.ReadKey();
                        break;
                }
            }
            catch
            {
                gotoXY("Error en la selección. El usuario será identificado como extranjero", 0, 6);
                extrangero = true;
                Console.ReadKey();
            }
                

            Console.Clear();
            gotoXY("-Estados del usuario: \n" +
                "1. Incompleto \n" +
                "2. Activo \n" +
                "3. Inactivo \n" +
                "4. AP ", 0, 1);
            Console.SetCursorPosition(24, 1);
            
            try
            {
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        estado = "Incompleto";
                        break;
                    case 2:
                        estado = "Activo";
                        break;
                    case 3:
                        estado = "Inactivo";
                        break;
                    case 4:
                        estado = "APA";
                        break;
                    default:
                        gotoXY("Error al seleccionar la opcion, el usuario será puesto como incompleto", 0, 6);
                           
                        estado = "Incompleto";
                        Console.ReadKey();
                        break;
                }
            }
            catch
            {
                gotoXY("Error al seleccionar la opcion, el usuario será puesto como incompleto", 0, 6);
                estado = "Incompleto";
                Console.ReadKey();
            }

            Console.Clear();
            try
            {   
                usuario.nombre = nombre;
                usuario.apellido = apellido;
                usuario.carrera = carrera;
                usuario.estado = estado;
                usuario.fechaNacimiento = fechaNacimiento;
                usuario.ID = int.Parse(ID);
                usuario.identificadorPersonal = identificador;
                usuario.extrangero = extrangero;

                AddAlumnsToDataBase(usuario.ID, identificador, nombre, apellido, estado, carrera, extrangero.ToString(), fechaNacimiento.ToShortDateString());
                Console.WriteLine("Usuario agregado con exito");
            }catch(Exception)
            {
                Console.WriteLine("El usuario no ha sido agregado. Regresando al menú principal");
            }
            
            Console.ReadKey();
            Menu();
        }
        static void RevisarUsuarios()
        {
            
            Console.Clear();
            gotoXY("-Nombre: ", 0, 0);
            gotoXY("-Apellido: ", 20, 0);
            gotoXY("-ID: ", 40, 0);
            gotoXY("-Carrera: ",51, 0);
            gotoXY("-Identificador nacional: ", 70, 0);
            gotoXY("-Fecha de nacimiento ", 95, 0);
            gotoXY("-Dominicano?", 120, 0);
            gotoXY("-Estado", 133, 0);
            int i = 1;
            DBConnect();
            string query = "select * from Alumnos";
            cmd.CommandText = query;
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string id = reader["ID"].ToString();
                string nombre = reader["Nombre"].ToString();
                string apellido = reader["Apellido"].ToString();
                string carrera= reader["Carrera"].ToString();
                string identificadorPersonal = reader["Identificador"].ToString();
                string estado= reader["Estado"].ToString();
                string fecha = reader["Fecha"].ToString();
                string  extranjero= reader["Extranjero"].ToString();
                gotoXY(nombre, 0, i);
                gotoXY(apellido, 20, i);
                gotoXY(id, 40, i);
                gotoXY(carrera, 51, i);
                gotoXY(identificadorPersonal, 70, i);
                gotoXY(fecha, 95, i);
                gotoXY(extranjero, 120, i);
                gotoXY(estado, 133, i);
                i++;
            }
            con.Close();
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Menu();
        }
        static void BuscarUsuario(String id)
        {
            
            Console.Clear();
            try
            {
                int i = 1;
                bool aux = false;
                DBConnect();
                string query = "select * from Alumnos where ID=" + int.Parse(id) + "";
                cmd.CommandText = query;
                cmd.Connection = con;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    aux = true;
                    gotoXY("-Nombre: ", 0, 0);
                    gotoXY("-Apellido: ", 20, 0);
                    gotoXY("-ID: ", 40, 0);
                    gotoXY("-Carrera: ", 51, 0);
                    gotoXY("-Identificador nacional: ", 70, 0);
                    gotoXY("-Fecha de nacimiento ", 95, 0);
                    gotoXY("-Dominicano?", 120, 0);
                    gotoXY("-Estado", 133, 0);
                    string nombre = reader["Nombre"].ToString();
                    string apellido = reader["Apellido"].ToString();
                    string carrera = reader["Carrera"].ToString();
                    string identificadorPersonal = reader["Identificador"].ToString();
                    string estado = reader["Estado"].ToString();
                    string fecha = reader["Fecha"].ToString();
                    string extranjero = reader["Extranjero"].ToString();
                    gotoXY(nombre, 0, i);
                    gotoXY(apellido, 20, i);
                    gotoXY(id, 40, i);
                    gotoXY(carrera, 51, i);
                    gotoXY(identificadorPersonal, 70, i);
                    gotoXY(fecha, 95, i);
                    gotoXY(extranjero, 120, i);
                    gotoXY(estado, 133, i);
                    i++;
                }
                if (aux == false)
                {
                    Console.WriteLine("El ID o Identificador nacional ingresado no coincide con ninguno de los usuarios agregados");

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error inesperado");
            }
            
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            con.Close();
            Menu();
        }
        static void EditarUsuario(String id)
        {
            Console.Clear();
            bool aux = false;
            byte opcion;
            string newInput;
            try
            {
                DBConnect();
                aux = true;
                Console.WriteLine("Seleccione el atributo que desee modificar: \n 1. Nombre \n 2. Apellido \n 3. Carrera \n 4. Identificador nacional \n 5. Fecha de nacimiento \n 6. Nacionalidad \n 7. Estado del usuario ");
                try
                {
                    opcion = byte.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    opcion = 0;
                }

                Console.Clear();
                Console.WriteLine("Ingrese el nuevo valor del atributo que desea modificar: \n");

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("-Nombre: ");

                        Console.SetCursorPosition(8, 2);
                        newInput = Console.ReadLine();
                        cmd.CommandText = "update Alumnos set Nombre=" + "'" + newInput + "'" + " where ID=" + int.Parse(id) + "";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        break;
                    case 2:
                        Console.WriteLine("-Apellido: ");
                        Console.SetCursorPosition(10, 2);
                        newInput = Console.ReadLine();
                        cmd.CommandText = "update Alumnos set Apellido=" + "'" + newInput + "'" + " where ID=" + int.Parse(id) + "";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        break;
                    case 3:
                        Console.WriteLine("-Carrera: ");
                        Console.SetCursorPosition(9, 2);
                        newInput = Console.ReadLine();
                        cmd.CommandText = "update Alumnos set Carrera=" + "'" + newInput + "'" + " where ID=" + int.Parse(id) + "";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        break;
                    case 4:
                        Console.WriteLine("-Iddentificador nacional: ");
                        Console.SetCursorPosition(26, 2);
                        newInput = Console.ReadLine();
                        cmd.CommandText = "update Alumnos set Identificador=" + "'" + newInput + "'" + " where ID=" + int.Parse(id) + "";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        break;
                    case 5:
                        Console.WriteLine("-Fecha de nacimiento dd/mm/yyyy: ");


                        DateTime fechaNacimiento;

                        try
                        {
                            Console.SetCursorPosition(33, 2);
                            fechaNacimiento = DateTime.Parse(Console.ReadLine());
                            cmd.CommandText = "update Alumnos set Fecha=" + "'" + fechaNacimiento.ToShortDateString() + "'" + " where ID=" + int.Parse(id) + "";
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();

                        }
                        catch (Exception)
                        {
                            cmd.CommandText = "update Alumnos set Fecha=" + "'01/01/1999'" + " where ID=" + int.Parse(id) + "";
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Formato de fecha invalido. Se establecera una fecha predeterminada");
                        }
                        ;
                        break;
                    case 6:
                        gotoXY("-Nacionalidad dominicana? \n" +
                        "1.Si \n" +
                        "2.No", 0, 2);
                        Console.SetCursorPosition(27, 2);
                        try
                        {
                            newInput = Console.ReadLine();
                            switch (newInput)
                            {
                                case "1":
                                    cmd.CommandText = "update Alumnos set Extranjero=" + "'si'" + " where ID=" + int.Parse(id) + "";
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "2":
                                    cmd.CommandText = "update Alumnos set Extranjero=" + "'no'" + " where ID=" + int.Parse(id) + "";
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();
                                    break;
                                default:
                                    gotoXY("Error en la selección. El usuario será identificado como extranjero", 0, 6);
                                    cmd.CommandText = "update Alumnos set Extranjero=" + "'si'" + " where ID=" + int.Parse(id) + "";
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();
                                    break;
                            }
                        }
                        catch
                        {
                            gotoXY("Error en la selección. El usuario será identificado como extranjero", 0, 6);

                            cmd.CommandText = "update Alumnos set Extranjero=" + "'si'" + " where ID=" + int.Parse(id) + "";
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                        }
                        ;
                        break;
                    case 7:
                        gotoXY("-Estados del usuario: \n" +
                        "1. Incompleto \n" +
                        "2. Activo \n" +
                        "3. Inactivo \n" +
                        "4. AP ", 0, 2);
                        Console.SetCursorPosition(24, 2);

                        try
                        {
                            newInput = Console.ReadLine();
                            switch (newInput)
                            {   
                                case "1":
                                    cmd.CommandText = "update Alumnos set Estado=" + "'Incompleto'" + " where ID=" + int.Parse(id) + "";
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();
                                    Console.ReadKey();
                                    break;
                                case "2":
                                    cmd.CommandText = "update Alumnos set Estado=" + "'ACTIVO'" + " where ID=" + int.Parse(id) + "";
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();
                                    Console.ReadKey();
                                    break;
                                case "3":
                                    cmd.CommandText = "update Alumnos set Estado=" + "'INACTIVO'" + " where ID=" + int.Parse(id) + "";
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();
                                    Console.ReadKey();
                                    break;
                                case "4":
                                    cmd.CommandText = "update Alumnos set Estado=" + "'APA'" + " where ID=" + int.Parse(id) + "";
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();
                                    Console.ReadKey();
                                    break;
                                default:
                                    gotoXY("Error al seleccionar la opcion, el usuario será puesto como incompleto", 0, 7);
                                    cmd.CommandText = "update Alumnos set Estado=" + "'Incompleto'" + " where ID=" + int.Parse(id) + "";
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();
                                    Console.ReadKey();
                                    Menu();
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        catch
                        {
                            gotoXY("Error al seleccionar la opcion, el usuario será puesto como incompleto", 0, 7);
                            cmd.CommandText = "update Alumnos set Estado=" + "'Incompleto'" + " where ID=" + int.Parse(id) + "";
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                        }
                        break;

                }

                Console.Clear();
                gotoXY("Atributo modificado correctamente", 0, 0);
                Console.ReadKey();
                con.Close();
                Menu();
            }
            catch (Exception)
            {
                Console.WriteLine("El usuario no pudo ser modificado" +
                    "");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
                if (aux == false)
                {
                Console.Clear();
                Console.WriteLine("El ID ingresado no coincide con ninguno de los usuarios agregados");
                Console.ReadKey();
                con.Close();
                Menu();
                }
        }
        static void EliminarUsuario(String id)
        {
            try
            {
                DBConnect();
                string query = "delete from Alumnos where ID=" + int.Parse(id) + "";
                cmd.CommandText = query;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                Console.WriteLine("Usuario eliminado con exito");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("El usuario no pudo ser eliminado");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            
            Menu();
        }
    }
}
