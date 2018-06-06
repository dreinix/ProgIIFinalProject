using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;

namespace ProgIIFinalProject
{
    class Program
    {
        static SqlConnection con = new SqlConnection();
        static SqlCommand cmd = new SqlCommand();
        static void DBConnect()
        {
            try
            {
                con.Close();
            } catch (Exception) { };

            string cPath = System.IO.Path.GetFullPath(@"..\..\") + "ProgIIDB.mdf";
            String path = @"Data source = (localDB)\MSSQLLocalDB ; AttachDbFilename=" + cPath + ";Integrated Security=SSPI";
            con.ConnectionString = path;
            con.Open();
        }
        static List<string> ListaAreas = new List<string>()
        {
            "Ingeneria","Ciencias Basicas"
        };
        static void AddAlumnsToDataBase(int id,string idNacional, string nombre,string apellido,string estado,string carrera,string extran,string fecha)
        {
            DBConnect();
            using (SqlCommand cmd = new SqlCommand("Insert into Alumnos VALUES (" +
                       "@ID,@identificador,@nombre,@apellido,@estado,@carrera,@extran,@fecha)", con))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@identificador", idNacional);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@estado",estado);
                cmd.Parameters.AddWithValue("@carrera", carrera);
                cmd.Parameters.AddWithValue("@extran", extran);
                cmd.Parameters.AddWithValue("@fecha", fecha);


                int changes = cmd.ExecuteNonQuery();
            }

          /*      String query = "Insert into Alumnos values("+ id + "," + "'" + idNacional + "'," + "'" + nombre + "'," + "'" + apellido + "'," +
                "'" + estado + "'," + "'" + carrera + "'," + "'" + extran + "'," + "'" + fecha + "')";*/
            con.Close();
        }
        static void AddMateriasToDataBase(int id, string code, string nombre, string area)
        {
            DBConnect();
            using (SqlCommand cmd = new SqlCommand("Insert into Materias VALUES (" +
                       "@ID,@Code,@Nombre,@Area)", con))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Code", code);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Area", area);

                int changes = cmd.ExecuteNonQuery();
            }

            /*      String query = "Insert into Alumnos values("+ id + "," + "'" + idNacional + "'," + "'" + nombre + "'," + "'" + apellido + "'," +
                  "'" + estado + "'," + "'" + carrera + "'," + "'" + extran + "'," + "'" + fecha + "')";*/
            con.Close();
        }
        static int GenerarIDMateria(MateriasCS materia)
        {
            materia.GenerarID();
            int id = 0000000;
            id = materia.ID;
            try
            {
                DBConnect();
                using (cmd = new SqlCommand("select [ID] from Materias where [ID] = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        GenerarIDMateria(materia);
                    }
                }
            }
            catch
            {
                Console.WriteLine("ID error");
            }
            con.Close();
            return id;

        }
        static string GenerarCodigoMateria(MateriasCS materia)
        {
            materia.GenerarCodigo();
            string code = "";
            code = materia.codigo;
            try
            {
                DBConnect();
                using (cmd = new SqlCommand("select [Code] from Materias where [Code] = @code", con))
                {
                    cmd.Parameters.AddWithValue("@code", code);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        GenerarCodigoMateria(materia);
                    }
                }
            }
            catch
            {
                Console.WriteLine("code error");
            }
            con.Close();
            return code;

        }
        static int GenerarIDAlumno(User usuario)
        {
            usuario.GenerarID();
            int id = 0000000;
            id = usuario.ID;
            try
            {
                DBConnect();
                using (cmd = new SqlCommand("select [ID] from Alumnos where [ID] = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        GenerarIDAlumno(usuario);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Database error");
            }
            return id;
            
        }
        static void MenuGeneral()
        {
            byte opcion;
            Console.Clear();
            Console.SetCursorPosition(5, 0);
            Console.WriteLine("*****Menú*****");
            Console.WriteLine("1. Menu Materias \n" +
                "2. Menu alumnos \n"+
                "3. Salir");
            try
            {
                opcion = byte.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                opcion = 0;
            }
            switch(opcion)
            {
                case 1:
                    MenuMaterias();
                break;
                case 2:
                    MenuAlumnos();
                break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Gracias por utilizar nuestros servicios");
                    Console.ReadKey();
                    Environment.Exit(0);
                break;
            }

        }
        static void MenuMaterias()
        {
            byte opcion;
            string iD;
            Console.Clear();
            Console.SetCursorPosition(5, 0);
            Console.WriteLine("*****Menú*****");
            Console.WriteLine("1. Agregar materias \n" +
                "2. Visualizar materias \n" +
                "3. Buscar materias \n" +
                "4. Modificar materias \n" +
                "5. Eliminar materias \n" +
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
                    AgregarMateria();
                    break;
                case 2:
                    RevisarMaterias();
                    break;
                case 3:
                    Console.WriteLine("Ingrese el ID o el identificador nacional del usuario: ");
                    iD = Console.ReadLine();
                    BuscarMateria(iD);
                    break;
                case 4:
                    Console.WriteLine("Ingrese el ID del usuario al que desea modificar algun dato: \n");
                    iD = Console.ReadLine();
                    EditarMateria(iD);
                    break;
                case 5:
                    Console.WriteLine("Ingrese el ID del usuario al que desea eliminar: \n");
                    iD = Console.ReadLine();
                    EliminarMateria(iD);
                    break;
                case 6:
                    Console.Clear();
                    MenuGeneral();
                    //Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Opcion invalida,intente de nuevo");
                    Console.ReadKey();
                    Console.Clear();
                    MenuMaterias();
                break;
            }
            Console.ReadKey();

        }
        static void MenuAlumnos()
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
                    MenuGeneral();
                    break;
                default:
                    Console.WriteLine("Opcion invalida,intente de nuevo");
                    Console.ReadKey();
                    Console.Clear();
                    MenuAlumnos();
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
           // Console.ReadKey();
            MenuGeneral();


        }
        static void gotoXY(string word,int x,int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(word);
        }
        static void AgregarUsuario()
        {
            int xPosition = 0,option;
            Console.Clear();
            User usuario = new User();
            String nombre, apellido, estado, carrera, identificador;
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

            usuario.ID = GenerarIDAlumno(usuario);

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
                usuario.identificadorPersonal = identificador;
                usuario.extrangero = extrangero;
                AddAlumnsToDataBase(usuario.ID, identificador, nombre, apellido, estado, carrera, extrangero.ToString(), fechaNacimiento.ToShortDateString());
                Console.WriteLine("Usuario agregado con exito");
            }catch(Exception)
            {
                Console.WriteLine("El usuario no ha sido agregado. Regresando al menú principal");
            }
            
            Console.ReadKey();
            MenuAlumnos();
        }
        static void AgregarMateria()
        {
            try
            {

                int xPosition = 0, option;
                Console.Clear();
                MateriasCS materia = new MateriasCS();
                String nombre;
                gotoXY("-Nombre: ", xPosition, 1);
                xPosition += 8;
                Console.SetCursorPosition(xPosition, 1);
                nombre = Console.ReadLine();
                xPosition += nombre.Length + 1;

                gotoXY("-Area: ", xPosition, 1);
                int i = 2;
                bool space = false;
                string[] codes = new string[(ListaAreas.Count + 1)];
                string[] materiasT = new string[(ListaAreas.Count + 1)];
                foreach (string areas in ListaAreas)
                {
                    materiasT[(i - 1)] = areas;
                    string code = "";
                    gotoXY((i - 1) + ") " + areas, xPosition, i);
                    code += areas[0].ToString().ToUpper();
                    for (int a = 0; a < areas.Length - 1; a++)
                    {
                        if (areas[a].ToString() == " ")
                        {
                            space = true;
                            code += areas[a + 1].ToString().ToUpper();
                        }
                    }
                    if (!space)
                    {
                        code = areas.Substring(0, 3).ToUpper();
                    }

                    codes[(i - 1)] = code;
                    i += 1;
                }

                xPosition += 8;
                Console.SetCursorPosition(xPosition, 1);
                option = int.Parse(Console.ReadLine());
                materia.area = materiasT[option];
                materia.areaCode = codes[option];
                materia.ID = GenerarIDMateria(materia);
                materia.codigo = GenerarCodigoMateria(materia);

                AddMateriasToDataBase(materia.ID, materia.codigo, nombre, materia.area);
            }
            catch (Exception)
            {
                Console.WriteLine("Error en la creación");
            }
            
            Console.WriteLine("Materia agregada con exito");
            Console.ReadKey();
            MenuMaterias();
             
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
            MenuAlumnos();
        }
        static void RevisarMaterias()
        {
            Console.Clear();
            gotoXY("-ID: ", 0, 0);
            gotoXY("-Nombre: ", 16, 0);
            gotoXY("-Codigo: ", 26, 0);
            gotoXY("-Area: ", 35, 0);
            int i = 1;
            DBConnect();
            string query = "select * from Materias";
            cmd.CommandText = query;
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string id = reader["ID"].ToString();
                string nombre = reader["Nombre"].ToString();
                string code = reader["Code"].ToString();
                string area = reader["Area"].ToString();
                gotoXY(id, 0, i);
                gotoXY(nombre, 16, i);
                gotoXY(code, 26, i);
                gotoXY(area, 35, i);
                i++;
            }
            con.Close();
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            MenuMaterias();
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
            catch(Exception)
            {
                Console.WriteLine("Error inesperado");
            }
            
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            con.Close();
            MenuAlumnos();
        }
        static void BuscarMateria(string id)
        {
            Console.Clear();

            int i = 1;
            DBConnect();

            string query = "select * from Materias where [ID]="+int.Parse(id)+" or [Code]="+"'"+(id.ToString()+"'");
            cmd.CommandText = query;
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {   
                gotoXY("-ID: ", 0, 0);
                gotoXY("-Nombre: ", 16, 0);
                gotoXY("-Codigo: ", 26, 0);
                gotoXY("-Area: ", 35, 0);
                string cid = reader["ID"].ToString();
                string nombre = reader["Nombre"].ToString();
                string code = reader["Code"].ToString();
                string area = reader["Area"].ToString();
                gotoXY(cid, 0, i);
                gotoXY(nombre, 16, i);
                gotoXY(code, 26, i);
                gotoXY(area, 35, i);
                i++;

            }

            con.Close();
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            MenuMaterias();
        }
        static void EditarUsuario(String id)
        {
            int found=(0);
            try
            {
                DBConnect();
                using (cmd = new SqlCommand("select [ID] from Alumnos where [ID] = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", int.Parse(id));
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        found += 1;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Database error");
            }

            if (found > 0)
            {
                Console.Clear();
                byte opcion;
                string newInput;
                try
                {
                    DBConnect();
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
                                        MenuAlumnos();
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
                    MenuAlumnos();
                }
                catch (Exception)
                {
                    Console.WriteLine("El usuario no pudo ser modificado" +
                        "");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("El ID ingresado no coincide con ninguno de los usuarios agregados");
                Console.ReadKey();
                con.Close();
                MenuAlumnos();
            }
            
            
        }
        static void EditarMateria(string id)
        {
            int found = (0);
            try
            {
                DBConnect();
                using (cmd = new SqlCommand("select * from Materias where [ID]=" + int.Parse(id) + " or [Code]=" + "'" + (id.ToString() + "'"), con))
                {
                    cmd.Parameters.AddWithValue("@id", int.Parse(id));
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        found += 1;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Database error");
            }

            if (found > 0)
            {
                Console.Clear();
                byte opcion;
                string newInput;
                try
                {
                    DBConnect();
                    Console.WriteLine("Seleccione el atributo que desee modificar: \n 1. ID \n 2. Code \n 3. Nombre \n 4. Area");
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
                            Console.WriteLine("-ID: ");

                            Console.SetCursorPosition(8, 2);
                            newInput = Console.ReadLine();
                            cmd.CommandText = "update Materias set ID=" + "'" + newInput + "'" + " where [ID]=" + int.Parse(id) + " or [Code]=" + "'" + (id.ToString() + "'");
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                            break;
                        case 2:
                            Console.WriteLine("-Code: ");
                            Console.SetCursorPosition(10, 2);
                            newInput = Console.ReadLine();
                            cmd.CommandText = "update Materias set Code=" + "'" + newInput + "'" + " where [ID]=" + int.Parse(id) + " or [Code]=" + "'" + (id.ToString() + "'");
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                            break;
                        case 3:
                            Console.WriteLine("-Nombre: ");
                            Console.SetCursorPosition(9, 2);
                            newInput = Console.ReadLine();
                            cmd.CommandText = "update Materias set Nombre=" + "'" + newInput + "'" + " where [ID]=" + int.Parse(id) + " or [Code]=" + "'" + (id.ToString() + "'");
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                            break;
                        case 4:
                            int xPosition = 9,i=3;
                            int option;
                            string Area;
                            Console.WriteLine("-Area: ");
                            Console.SetCursorPosition(xPosition, 2);
                            string[] materiasT = new string[(ListaAreas.Count + 1)];
                            foreach (string areas in ListaAreas)
                            {
                                materiasT[(i - 2)] = areas;
                                gotoXY((i - 2) + ") " + areas, xPosition, i);
                                i += 1;
                            }
                            Console.SetCursorPosition(xPosition, 2);
                            option = int.Parse(Console.ReadLine());
                            Area = materiasT[option];
                            cmd.CommandText = "update Materias set Area=" + "'" + Area + "'" + " where [ID]=" + int.Parse(id) + " or [Code]=" + "'" + (id.ToString() + "'");
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                            break;

                    }

                    Console.Clear();
                    gotoXY("Atributo modificado correctamente", 0, 0);
                    Console.ReadKey();
                    con.Close();
                    MenuMaterias();
                }
                catch (Exception)
                {
                    Console.WriteLine("El usuario no pudo ser modificado");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    con.Close();
                    MenuMaterias();
                }
            }
            else
            {
                Console.WriteLine("El ID ingresado no coincide con ninguno de los usuarios agregados");
                Console.ReadKey();
                con.Close();
                MenuMaterias();
            }
        }
        static void EliminarUsuario(String id)
        {
            try
            {
                int found = 0;
                DBConnect();
                using (cmd = new SqlCommand("delete from Alumnos where ID = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    found = cmd.ExecuteNonQuery();
                }
                if (found > 0)
                {
                    Console.WriteLine("Usuario eliminado con exito");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                }
                else
                {
                    Console.WriteLine("El usuario no existente");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                }

                Console.ReadKey();
            }

            catch (Exception)
            {
                Console.WriteLine("El usuario no pudo ser eliminado");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }

            con.Close();
            MenuAlumnos();
        }
        static void EliminarMateria(String id)
        {
            try
            {
                int found = 0;
                DBConnect();
                using (cmd = new SqlCommand("delete from Materias where ID = @id or [Code] = @code", con))
                {
                    cmd.Parameters.AddWithValue("@id", int.Parse(id));
                    cmd.Parameters.AddWithValue("@code", id.ToString());
                    found = cmd.ExecuteNonQuery();
                }
                if (found > 0)
                {
                    Console.WriteLine("Usuario eliminado con exito");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                }
                else
                {
                    Console.WriteLine("El usuario no existente");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                }

                Console.ReadKey();
            }

            catch (Exception)
            {
                Console.WriteLine("El usuario no pudo ser eliminado");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }

            con.Close();
            MenuMaterias();
        }
    }
}
