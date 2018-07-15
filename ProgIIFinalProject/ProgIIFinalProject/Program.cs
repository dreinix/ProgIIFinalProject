using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace ProgIIFinalProject
{
    class Program
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        void DBConnect()
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

        List<string> ListaAreas = new List<string>()
        {
            "Ingeneria","Ciencias Basicas Y Ambientales","Ciencias de la Salud", "Ciencias Sociales y Humanidades", "Economía y Negocios"
        };

        void AddAlumnsToDataBase(int id,string idNacional, string nombre,string apellido,string estado,string carrera,string extran,string fecha)
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
        void AddMateriasToDataBase(int id, string code, string nombre, string area)
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

        int GenerarIDMateria(MateriasCS materia)
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
        string GenerarCodigoMateria(MateriasCS materia)
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
        int GenerarIDAlumno(User usuario)
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

        void MenuGeneral()
        {
            byte opcion;
            Console.Clear();
            Console.SetCursorPosition(5, 0);
            Console.WriteLine("*****Menú*****");
            Console.WriteLine("1. Menu Materias \n" +
                "2. Menu alumnos \n" + "3. Menu Programaciones \n" + "4. Menu reportes \n" + "5. Menu integracion \n" +
                "6. Salir");
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
                    MenuProgramaciones();
                    break;
                case 4:
                    MenuReportes();
                    break;
                case 5:
                    
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Gracias por utilizar nuestros servicios");
                    Console.ReadKey();
                    Environment.Exit(0);
                break;
            }

        }
        void MenuMaterias()
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
                    Console.WriteLine("Ingrese el nombre o el area de la materia: ");
                    iD = Console.ReadLine();
                    BuscarMateria(iD);
                    break;
                case 4:
                    Console.WriteLine("Ingrese el ID o el codigo de la materia al que desea modificar algun dato: \n");
                    iD = Console.ReadLine();
                    EditarMateria(iD);
                    break;
                case 5:
                    Console.WriteLine("Ingrese el ID o el codigo de la materia que desea eliminar: \n");
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
        void MenuAlumnos()
        {
            
            byte opcion;
            string iD;
            Console.Clear();
            Console.SetCursorPosition(5, 0);
            Console.WriteLine("*****Menú*****");
            Console.WriteLine("1. Agregar alummnos \n" +
                "2. Visualizar alumnos \n" +
                "3. Buscar \n" +
                "4. Modificar alumnos \n" +
                "5. Eliminar alumnos \n" +
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
                    Console.WriteLine("Ingrese el nombre o el identificador nacional del usuario: ");
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
        void MenuProgramaciones()
        {   
            byte opcion;
            string iD;
            Console.Clear();
            Console.SetCursorPosition(5, 0);
            Console.WriteLine("*****Menú*****");
            Console.WriteLine("1. Agregar programacion \n" +
                "2. Editar programacion \n" +
                "3. Eliminar programacion \n" +
                "4. Listar programacion \n" +
                "5. Buscar  \n" +
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
                    AgregarProgramacion();
                    break;
                case 2:
                    Console.WriteLine("Ingrese el ID de la programación: ");
                    iD = Console.ReadLine();
                    EditarProgramacion(iD);
                    break;
                case 3:
                    Console.WriteLine("Ingrese el id de la programacion: ");
                    iD = Console.ReadLine();
                    EliminarProgramacion(iD);
                    break;
                case 4:
                    RevisarProgramacion();
                    break;
                case 5:
                    byte op;
                    Console.WriteLine("1)Buscar seleccion del estudiante \n2) Buscar secciones de una materia");
                    try { 
                    op = byte.Parse(Console.ReadLine());
                    switch (op)
                    {
                        case 1:
                            Console.WriteLine("Ingrese el ID del estudiante a buscar \n");
                            iD = Console.ReadLine();
                            BuscarSeleccionUsuario(iD);
                            break;
                        case 2:
                            Console.WriteLine("Ingrese el ID de la materia a buscar \n");
                            iD = Console.ReadLine();
                            BuscarSeccionesMateria(iD);
                            break;
                        default:
                            Console.WriteLine("Opcion invalida");
                            Console.ReadKey();
                            Console.Clear();
                            MenuProgramaciones();
                            break;

                    }
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                        MenuProgramaciones();
                    }
                    
                    break;
                case 6:
                    Console.Clear();
                    MenuGeneral();
                    
                    break;
                default:
                    Console.WriteLine("Opcion invalida,intente de nuevo");
                    Console.ReadKey();
                    Console.Clear();
                    MenuProgramaciones();
                    break;
            }
            Console.ReadKey();


        }
        void MenuReportes()
        {
            byte opcion;
            string iD;
            Console.Clear();
            Console.SetCursorPosition(5, 0);
            Console.WriteLine("*****Menú*****");
            Console.WriteLine("1.Crear reporte \n2.Salir");
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
                    
                    Console.Clear();
                    Console.WriteLine("Seleccione el formato en el que desee generar el reporte: \n1.PDF\n2.Excel\n3.CSV\n");
                    try
                    {
                        opcion = byte.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        opcion = 0;
                    }
                    Console.WriteLine("Ingrese el ID de la programacion de la cual desea generar un reporte: ");
                    iD = Console.ReadLine();
                    switch (opcion)
                    {
                        case 1:
                            generarReportePDF(iD);
                            
                            
                            MenuReportes();
                            break;
                        case 2:

                            break;
                        case 3:

                            break;
                        default:
                            Console.WriteLine("Opcion invalida,intente de nuevo");
                            Console.ReadKey();
                            Console.Clear();
                            MenuReportes();
                            break;

                    }
                    break;
                    case 2:
                    MenuGeneral();
                        break;

                default:
                    Console.WriteLine("Opcion invalida,intente de nuevo");
                    Console.ReadKey();
                    Console.Clear();
                    MenuReportes();
                    break;



            }
            Console.ReadKey();
        }
        void MenuIntegracion()
        {
            Console.Clear();
            Console.SetCursorPosition(5, 0);
            Console.WriteLine("*****Menú*****");
            Console.WriteLine("1. Agregar usuarios \n" +
                "2. Visualizar usuarios \n" +
                "3. Buscar \n" +
                "4. Modificar usuario \n" +
                "5. Eliminar usuarios \n" +
                "6. Salir \n");
        }   

        void generarReportePDF(string id)
        {
            DBConnect();

            int found = (0);
            try
            {
                DBConnect();
                using (cmd = new SqlCommand("select * from Programacion where [ID] = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        found += 1;
                    }
                    con.Close();
                }
            }
            catch
            {
                Console.WriteLine("Database error");
            }

            if (found > 0)
            {
                DBConnect();
                string materia = "", maestro = "", aula = "", horario = "";
                string[] arrayHora = new string[20];
                string[] arrayDia = new string[20];
                using (cmd = new SqlCommand("select * from Programacion where [ID] = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        materia = reader["Materia"].ToString();
                        maestro = reader["Maestro"].ToString();
                        aula = reader["Aula"].ToString();
                        arrayHora = reader["hora"].ToString().Split('\n');
                        arrayDia = reader["dia"].ToString().Split('\n');
                    }
                    reader.Close();
                }
               
                Document reporte = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream("Reporte - " + id + ".pdf", FileMode.Create));

                reporte.AddTitle("Reporte - " + id);
                reporte.Open();

                iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                
                reporte.Add(new Paragraph("INSTITUTO TECNOLÓGICO DE SANTO DOMINGO\nDirección de registro\nAsistencia a Estudiantes\n\n" + "Asignatura: " + materia + "\nProfesor(a): " + maestro));
                reporte.Add(new Paragraph("Horario: "));
                for (int aux = 0; aux <= arrayDia.Length - 1; aux++)
                {

                    horario = arrayDia[aux]+": ";
                    if (aux < arrayHora.Length)
                    {
                        horario += arrayHora[aux];
                    }
                    reporte.Add(new Paragraph(horario));
                }

                reporte.Add(new Paragraph("Aula: " + aula));
                /*reporte.Add(new Paragraph("Horario: \t \t \t \t \t \t \t \t \t \t " + "Aula: " + aula + "\n Lunes\t \t Martes \t \t Miercoles \t \t Jueves \t \t Viernes"));
                Dictionary<String, int> DayValue = new Dictionary<string, int>();
                DayValue.Add("Lunes", 1);
                DayValue.Add("Martes", 2);
                DayValue.Add("Miercoles", 3);
                DayValue.Add("Jueves", 4);
                DayValue.Add("Viernes", 5);
                DayValue.Add("Sabado", 6);
                horario += "\t";
                for (int aux = 0; aux <= arrayDia.Length - 1; aux++)
                {
                    int number = DayValue[arrayDia[aux]];
                    for (int c = 0; c < number; c++)
                    {
                        horario += "\t";
                    }
                    if (aux < arrayHora.Length)
                    {
                        horario += arrayHora[aux];
                    }

                }
                reporte.Add(new Paragraph(horario));
                */
                reporte.Add(Chunk.NEWLINE);

                PdfPTable tblProgramacion = new PdfPTable(6);
                tblProgramacion.WidthPercentage = 100;

                PdfPCell clID = new PdfPCell(new Phrase("ID", font));
                clID.BorderWidth = 0;
                clID.BorderWidthBottom = 0.75f;
                PdfPCell clMatricula = new PdfPCell(new Phrase("Matrícula", font));
                clID.BorderWidth = 0;
                clID.BorderWidthBottom = 0.75f;
                PdfPCell clPrograma = new PdfPCell(new Phrase("Programa", font));
                clID.BorderWidth = 0;
                clID.BorderWidthBottom = 0.75f;
                PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", font));
                clID.BorderWidth = 0;
                clID.BorderWidthBottom = 0.75f;
                PdfPCell clAusencias = new PdfPCell(new Phrase("Ausencias", font));
                clID.BorderWidth = 0;
                clID.BorderWidthBottom = 0.75f;
                PdfPCell clTotal = new PdfPCell(new Phrase("Total", font));
                clID.BorderWidth = 0;
                clID.BorderWidthBottom = 0.75f;

                tblProgramacion.AddCell(clID);
                tblProgramacion.AddCell(clMatricula);
                tblProgramacion.AddCell(clPrograma);
                tblProgramacion.AddCell(clNombre);
                tblProgramacion.AddCell(clAusencias);
                tblProgramacion.AddCell(clTotal);

                found = 0;
                using (cmd = new SqlCommand("select * from Programacion where [ID] = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        found += 1;
                    }
                    reader.Close();
                }
                found = 0;
                using (cmd = new SqlCommand("Select * from Seleccion where [IDProgramacion]=@id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        found += 1;
                    }
                    reader.Close();
                }

                string[] idAlumno = new string[found + 1];
                int a = 0;
                using (cmd = new SqlCommand("Select * from Seleccion where [IDProgramacion]=@id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        idAlumno[a] = reader["IDAlumno"].ToString();
                        a++;
                    }
                    reader.Close();

                }

                a = 0;
                if (found > 0)
                {
                    int column = 1;
                    while ((column - 1) < found)
                    {
                        using (cmd = new SqlCommand("Select * from Alumnos where [ID]=@AlumnID ", con))
                        {
                            cmd.Parameters.AddWithValue("@AlumnID", idAlumno[(column - 1)]);
                            SqlDataReader reader = cmd.ExecuteReader();
                            string Nombre, Carrera;

                            while (reader.Read())
                            {
                                Nombre = reader["Nombre"].ToString() + " " + reader["Apellido"].ToString();
                                Carrera = reader["Carrera"].ToString();

                                clID = new PdfPCell(new Phrase(idAlumno[column - 1], font));
                                clID.BorderWidth = 0;
                                clMatricula = new PdfPCell(new Phrase("", font));
                                clMatricula.BorderWidth = 0;
                                clPrograma = new PdfPCell(new Phrase(Carrera, font));
                                clPrograma.BorderWidth = 0;
                                clNombre = new PdfPCell(new Phrase(Nombre, font));
                                clNombre.BorderWidth = 0;
                                clAusencias = new PdfPCell(new Phrase("", font));
                                clAusencias.BorderWidth = 0;
                                clTotal = new PdfPCell(new Phrase("", font));
                                clTotal.BorderWidth = 0;
                                
                                tblProgramacion.AddCell(clID);
                                tblProgramacion.AddCell(clMatricula);
                                tblProgramacion.AddCell(clPrograma);
                                tblProgramacion.AddCell(clNombre);
                                tblProgramacion.AddCell(clAusencias);
                                tblProgramacion.AddCell(clTotal);

                                
                            }
                            reader.Close();
                        }
                        column++;
                    }
                    reporte.Add(tblProgramacion);
                    reporte.Close();
                    writer.Close();
                }
            }
            else
            {
                Console.WriteLine("Programación no encontrada");
                Console.ReadKey();
                MenuReportes();
            }
            Console.Clear();
            Console.WriteLine("Reporte creado exitosamente");
            Console.ReadKey();
            MenuReportes();

        }

        static void Main(string[] args)
        {   
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("es-ES");
            Console.SetWindowSize(Convert.ToInt32(Console.LargestWindowWidth), Convert.ToInt32(Console.LargestWindowHeight));
            Console.WindowTop = 0;
            Console.SetWindowPosition(0, 0);
            // Console.ReadKey();
            Program p = new Program();
            p.MenuGeneral();
        }

        void GotoXY(string word,int x,int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(word);
        }

        void AgregarUsuario()
        {
            int xPosition = 0,option;
            Console.Clear();
            User usuario = new User();
            String nombre, apellido, estado, carrera, identificador;
            bool extrangero;
            DateTime fechaNacimiento;
            GotoXY("-Nombre: ", xPosition, 1);
            xPosition += 8;
            Console.SetCursorPosition(xPosition, 1);
            nombre  = Console.ReadLine();
            xPosition += nombre.Length + 1;

            GotoXY("-Apellido: ", xPosition, 1);
            xPosition += 10;
            Console.SetCursorPosition(xPosition, 1);
            apellido = Console.ReadLine();
            xPosition += apellido.Length + 1;

            usuario.ID = GenerarIDAlumno(usuario);

            GotoXY("-Carrera: ", xPosition, 1);
            xPosition += 9;
            Console.SetCursorPosition(xPosition, 1);
            carrera = Console.ReadLine();
            xPosition += carrera.Length + 1;

            GotoXY("-Identificador nacional: ", xPosition, 1);
            xPosition += 26;
            Console.SetCursorPosition(xPosition, 1);
            identificador = Console.ReadLine();
            xPosition =0;

            GotoXY("-Fecha de nacimiento dd/mm/yyyy: ", xPosition, 2);
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
            GotoXY("-Nacionalidad dominicana? \n" +
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
                        GotoXY("Error en la selección. El usuario será identificado como extranjero", 0, 6);
                        extrangero = true;
                        Console.ReadKey();
                        break;
                }
            }
            catch
            {
                GotoXY("Error en la selección. El usuario será identificado como extranjero", 0, 6);
                extrangero = true;
                Console.ReadKey();
            }
                

            Console.Clear();
            GotoXY("-Estados del usuario: \n" +
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
                        GotoXY("Error al seleccionar la opcion, el usuario será puesto como incompleto", 0, 6);
                           
                        estado = "Incompleto";
                        Console.ReadKey();
                        break;
                }
            }
            catch
            {
                GotoXY("Error al seleccionar la opcion, el usuario será puesto como incompleto", 0, 6);
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
        void AgregarMateria()
        {
            try
            {
                int xPosition = 0, option;
                Console.Clear();
                MateriasCS materia = new MateriasCS();
                String nombre;
                GotoXY("-Nombre: ", xPosition, 1);
                xPosition += 8;
                Console.SetCursorPosition(xPosition, 1);
                nombre = Console.ReadLine();
                xPosition += nombre.Length + 1;

                GotoXY("-Area: ", xPosition, 1);
                int i = 2;
                bool space = false;
                string[] codes = new string[(ListaAreas.Count + 1)];
                string[] materiasT = new string[(ListaAreas.Count + 1)];
                foreach (string areas in ListaAreas)
                {
                    materiasT[(i - 1)] = areas;
                    string code = "";
                    GotoXY((i - 1) + ") " + areas, xPosition, i);
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
                Console.Clear();
                Console.WriteLine("Materia agregada con exito");
                Console.ReadKey();
                MenuMaterias();

            }
            catch (Exception)
            {
                GotoXY("Error en la creación",0,8);
                Console.ReadKey();
                MenuMaterias();
            }
            
             
        }
        void AgregarProgramacion()
        {
            string idMateria="";
            string[] meridiano = new string[2] { "AM", "PM" };
            string[] horas = new string[12] {"1:00", "2:00", "3:00", "4:00", "5:00", "6:00", "7:00", "8:00", "9:00", "10:00", "11:00", "12:00" };
            string[] dias = new string[6] { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado" };
            string[] trimestres = new string[4] {"Agosto / Octubre", "Noviembre / Enero", "Febrero / Abril","Mayo / Julio" };
            int xPosition = 0;
            Console.Clear();
            Programaciones programacion = new Programaciones();
            MateriasCS materia = new MateriasCS();
            Horario horario = new Horario();
            String trimestre, aula, profesor;
            
           

            GotoXY("-Profesor: ", xPosition, 1);
            xPosition += 10;
            Console.SetCursorPosition(xPosition, 1);
            profesor = Console.ReadLine();
            xPosition += profesor.Length + 5;


            GotoXY("-Trimestre: ", xPosition, 1);
            xPosition += 12;
            int i = 1, auxiliar =0;
            foreach (string periodo in trimestres)
            {                
                GotoXY(i + ")" + periodo, xPosition, i);
                i++;               
            }
            try
            {
                xPosition += 20;
                Console.SetCursorPosition(xPosition, 1);
                auxiliar = Convert.ToInt32(Console.ReadLine());
                trimestre = trimestres[auxiliar - 1];
            }
            catch
            {
                trimestre = trimestres[0];
            }
            

            xPosition += 6;
            GotoXY("-Aula: ", xPosition, 1);
            Console.SetCursorPosition(xPosition + 6, 1);
            aula = Console.ReadLine();

            Console.Clear();
            xPosition = 0;
            try
            {
                GotoXY("Cuantos dias a la semana se impartira la materia?" , xPosition, 1);
                Console.SetCursorPosition(xPosition + 51, 1);
                auxiliar = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            
               bool first = true;
               for (int aux = 0; aux < auxiliar; aux++)
                {
                    int aux2;
                    xPosition = 0;
                    GotoXY("Seleccione el dia numero " + (aux + 1) + ": ", xPosition, 1);
                    xPosition = 28;
                    i = 1;
                    foreach (string dia in dias)
                    {
                        GotoXY(i + ")" + dia, xPosition, i);
                        i++;
                    }
                    xPosition += 8;
                    Console.SetCursorPosition(xPosition, 1);
                    aux2 = Convert.ToInt32(Console.ReadLine());
                    
                    if (first)
                    {
                        horario.dia = dias[aux2 - 1];
                        first = false;

                    } else { horario.dia = horario.dia + "\n" + dias[aux2 - 1]; }
                    

                    xPosition = 0;
                    GotoXY("Seleccione el horario para los " + dias[aux2 - 1] + ": ", xPosition, 10);

                    GotoXY("-Desde las: ", xPosition, 14);
                    xPosition += 14;
                    i = 14;
                    foreach (string hora in horas)
                    {
                        GotoXY((i - 13) + ")" + hora, xPosition, i);
                        i++;
                    }

                    xPosition += 9;
                    Console.SetCursorPosition(xPosition, 14);
                    i = Convert.ToInt32(Console.ReadLine());
                    horario.hora = horario.hora + horas[i - 1];
                    xPosition += 4;
                    GotoXY("1)AM", xPosition, 14);
                    GotoXY("2)PM", xPosition, 15);
                    xPosition += 8;
                    Console.SetCursorPosition(xPosition, 14);
                    i = Convert.ToInt32(Console.ReadLine());
                    horario.hora = horario.hora + meridiano[i - 1];
                    xPosition += 4;
                    GotoXY("-Hasta las: ", xPosition, 14);
                    xPosition += 14;
                    i = 14;
                    foreach (string hora in horas)
                    {
                        GotoXY((i - 13) + ")" + hora, xPosition, i);
                        i++;
                    }
                    xPosition += 9;
                    Console.SetCursorPosition(xPosition, 14);
                    i = Convert.ToInt32(Console.ReadLine());
                    horario.hora = horario.hora + " - " + horas[i - 1];
                    xPosition += 4;
                    GotoXY("1)AM", xPosition, 14);
                    GotoXY("2)PM", xPosition, 15);
                    xPosition += 8;
                    Console.SetCursorPosition(xPosition, 14);
                    i = Convert.ToInt32(Console.ReadLine());
                    horario.hora = horario.hora + meridiano[i - 1] + "\n";
                    Console.Clear();

                }
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); Console.ReadKey(); MenuProgramaciones(); }

            try
            {
                DBConnect();
                Console.Clear();
                xPosition = 0;
                Console.WriteLine("Seleccione el ID de la materia: ");
                GotoXY("ID    Materias", 0, 2);
                GotoXY("--------------", 0, 3);
                using (cmd = new SqlCommand("select * from Materias", con))
                {   
                    SqlDataReader reader = cmd.ExecuteReader();
                    int index = 4;
                    while (reader.Read())
                    {
                        Console.SetCursorPosition(xPosition, index); 
                        string Mid = reader["ID"].ToString();
                        Console.WriteLine(Mid);
                        xPosition += Mid.Length+2;
                        Console.SetCursorPosition(xPosition, index);
                        string NombreMateria = reader["Nombre"].ToString();
                        Console.WriteLine(NombreMateria);
                        xPosition = 0;
                        index+=1;
                    }
                    con.Close();
                }
                bool select = false;
                xPosition = 32;
                DBConnect();
                
                while (!select)
                {
                    Console.SetCursorPosition(xPosition, 0);
                    idMateria = Console.ReadLine();
                    using (cmd = new SqlCommand("select * from Materias where [ID] = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", idMateria);
                        SqlDataReader reader = cmd.ExecuteReader();
                        int index = 1;
                        while (reader.Read())
                        {
                            programacion.materia = reader["Nombre"].ToString();
                            select = true;
                        }
                        if (index == 0)
                        {
                            Console.WriteLine("La materia que seleccionó no existe");
                        }
                    }
                }             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            con.Close();
            try
            {
                programacion.profesor = profesor;
                programacion.horario = horario;
                programacion.GenerarID();
                programacion.aula = aula;
                programacion.trimestre = trimestre;
                DBConnect();
                using (cmd = new SqlCommand("Insert into Programacion values (@id,@trimestre,@materia,@dia,@hora,@aula,@maestro,@idMat)", con))
                {
                    cmd.Parameters.AddWithValue("@id",programacion.ID.ToString());
                    cmd.Parameters.AddWithValue("@trimestre", programacion.trimestre);
                    cmd.Parameters.AddWithValue("@materia", programacion.materia);
                    cmd.Parameters.AddWithValue("@dia", programacion.horario.dia.ToString());
                    cmd.Parameters.AddWithValue("@hora", programacion.horario.hora.ToString());
                    cmd.Parameters.AddWithValue("@aula", programacion.aula);
                    cmd.Parameters.AddWithValue("@maestro", programacion.profesor);
                    cmd.Parameters.AddWithValue("@idMat", idMateria);
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                Console.Clear();
                Console.WriteLine("Programacion agregada con exito");
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.WriteLine("La programacion no ha sido agregada. Regresando al menú principal");
            }
            Console.ReadKey();
            MenuProgramaciones();
        }
            
        void RevisarUsuarios()
        {
            
            Console.Clear();
            GotoXY("-Nombre: ", 0, 0);
            GotoXY("-Apellido: ", 20, 0);
            GotoXY("-ID: ", 40, 0);
            GotoXY("-Carrera: ",51, 0);
            GotoXY("-Identificador nacional: ", 70, 0);
            GotoXY("-Fecha de nacimiento ", 95, 0);
            GotoXY("-Dominicano?", 120, 0);
            GotoXY("-Estado", 133, 0);
            int i = 1;
            DBConnect();
            using(cmd = new SqlCommand("select * from Alumnos", con))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader["ID"].ToString();
                    string nombre = reader["Nombre"].ToString();
                    string apellido = reader["Apellido"].ToString();
                    string carrera = reader["Carrera"].ToString();
                    string identificadorPersonal = reader["Identificador"].ToString();
                    string estado = reader["Estado"].ToString();
                    string fecha = reader["Fecha"].ToString();
                    string extranjero = reader["Extranjero"].ToString();
                    GotoXY(nombre, 0, i);
                    GotoXY(apellido, 20, i);
                    GotoXY(id, 40, i);
                    GotoXY(carrera, 51, i);
                    GotoXY(identificadorPersonal, 70, i);
                    GotoXY(fecha, 95, i);
                    GotoXY(extranjero, 120, i);
                    GotoXY(estado, 133, i);
                    i++;
                }
            }
            
            con.Close();
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            MenuAlumnos();
        }
        void RevisarMaterias()
        {
            Console.Clear();
            GotoXY("-ID: ", 0, 0);
            GotoXY("-Nombre: ", 16, 0);
            GotoXY("-Codigo: ", 46, 0);
            GotoXY("-Area: ", 55, 0);
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
                GotoXY(id, 0, i);
                GotoXY(nombre, 16, i);
                GotoXY(code, 46, i);
                GotoXY(area, 55, i);
                i++;
            }
            con.Close();
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            MenuMaterias();
        }           
        void RevisarProgramacion()
        {
            Console.Clear();
            GotoXY("-Dia: ", 0, 0);
            GotoXY("-Hora: ", 15, 0);
            GotoXY("-ID: ", 35, 0);
            GotoXY("-Trimestre: ", 51, 0);
            GotoXY("-Aula: ", 81, 0);
            GotoXY("-Profesor: ", 90, 0);
            GotoXY("-Materia: ", 110, 0);
            GotoXY("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------", 0, 1);
            int i = 3;
            DBConnect();
            string query = "select * from Programacion";
            cmd.CommandText = query;
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string test = reader["hora"].ToString();
                string[] arrayDia = reader["dia"].ToString().Split('\n');
                string[] arrayHora = reader["hora"].ToString().Split('\n');
                string id = reader["id"].ToString();
                string trimestre = reader["trimestre"].ToString();
                string aula = reader["aula"].ToString();
                string maestro = reader["maestro"].ToString();
                string materia = reader["materia"].ToString();
                
                GotoXY(id, 35, i);
                GotoXY(trimestre, 51, i);
                GotoXY(aula, 81, i);
                GotoXY(maestro, 90, i);
                GotoXY(materia, 110, i);
                for (int aux =0; aux <= arrayDia.Length-1; aux++)
                {
                    
                    GotoXY(arrayDia[aux], 0, i);
                    
                    if (aux < arrayHora.Length)
                    {

                        GotoXY(arrayHora[aux], 15, i);
                    }
                    
                    i++;
                }

                GotoXY("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------", 0, i+1);
                i += + 3;
            }
            con.Close();
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            MenuProgramaciones();


        }
        //nada
        void BuscarUsuario(String id)
        {
            
            Console.Clear();
            try
            {
                int i = 1;
                bool aux = false;
                DBConnect();
                using (cmd = new SqlCommand("select * from Alumnos where [Identificador]= @id or [Nombre]=@id", con))
                {
                    cmd.Parameters.AddWithValue("@id", int.Parse(id));
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        aux = true;
                        GotoXY("-Nombre: ", 0, 0);
                        GotoXY("-Apellido: ", 20, 0);
                        GotoXY("-ID: ", 40, 0);
                        GotoXY("-Carrera: ", 51, 0);
                        GotoXY("-Identificador nacional: ", 70, 0);
                        GotoXY("-Fecha de nacimiento ", 95, 0);
                        GotoXY("-Dominicano?", 120, 0);
                        GotoXY("-Estado", 133, 0);
                        string nombre = reader["Nombre"].ToString();
                        string apellido = reader["Apellido"].ToString();
                        string carrera = reader["Carrera"].ToString();
                        string identificadorPersonal = reader["Identificador"].ToString();
                        string estado = reader["Estado"].ToString();
                        string fecha = reader["Fecha"].ToString();
                        string extranjero = reader["Extranjero"].ToString();
                        GotoXY(nombre, 0, i);
                        GotoXY(apellido, 20, i);
                        GotoXY(id, 40, i);
                        GotoXY(carrera, 51, i);
                        GotoXY(identificadorPersonal, 70, i);
                        GotoXY(fecha, 95, i);
                        GotoXY(extranjero, 120, i);
                        GotoXY(estado, 133, i);
                        i++;
                    }
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
        void BuscarMateria(string id)
        {   
            Console.Clear();
            try
            {   
                int i = 1;
                DBConnect();
                using (cmd = new SqlCommand("select * from Materias where [Area]= @id or [Nombre]=@id", con))
                {
                    cmd.Parameters.AddWithValue("@id", int.Parse(id));
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        GotoXY("-ID: ", 0, 0);
                        GotoXY("-Nombre: ", 16, 0);
                        GotoXY("-Codigo: ", 46, 0);
                        GotoXY("-Area: ", 55, 0);
                        string cid = reader["ID"].ToString();
                        string nombre = reader["Nombre"].ToString();
                        string code = reader["Code"].ToString();
                        string area = reader["Area"].ToString();
                        GotoXY(cid, 0, i);
                        GotoXY(nombre, 16, i);
                        GotoXY(code, 46, i);
                        GotoXY(area, 55, i);
                        i++;

                    }
                }
                
            }
            catch
            {
                Console.WriteLine("Error en la base de datos");
            }
            

            con.Close();
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            MenuMaterias();
        }
        void BuscarSeleccionUsuario(string idAlumno)
         {
            DBConnect();
            Console.Clear();
            Console.SetCursorPosition(33, 0);
            int found = 0;
            using (cmd = new SqlCommand("Select * from Seleccion where [IDAlumno]=@id", con))
            {
                cmd.Parameters.AddWithValue("@id", idAlumno);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    found += 1;
                }
                reader.Close();
            }
            string[] programaciones = new string[found];
            int a = 0;
            using (cmd = new SqlCommand("Select * from Seleccion where [IDAlumno]=@id", con))
            {
                cmd.Parameters.AddWithValue("@id", idAlumno);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    programaciones[a] = reader["IDProgramacion"].ToString();
                    a++;
                }
                reader.Close();

            }
            if (found > 0)
            {
                Console.Clear();
                GotoXY("Trimestre", 20-20, 0);
                GotoXY("Materia", 40 - 20, 0);
                GotoXY("Dia", 55 - 20, 0);
                GotoXY("Hora", 65 - 20, 0);
                GotoXY("Aula", 90 - 20, 0);
                int column = 1;
                int last = column+1;
                while ((column - 1) < found)
                {
                    Console.WriteLine("-------------------------------------------------------------------------------------------");
                    using (cmd = new SqlCommand("Select * from Programacion where [ID]=@progID ", con))
                    {
                        cmd.Parameters.AddWithValue("@progID", programaciones[column - 1]);
                        SqlDataReader reader = cmd.ExecuteReader();
                        string trimestre, mat, aula;
                        while (reader.Read())
                        {
                            trimestre = reader["Trimestre"].ToString();
                            mat = reader["Materia"].ToString();
                            string[] arrayDia = reader["dia"].ToString().Split('\n');
                            string[] arrayHora = reader["hora"].ToString().Split('\n');
                            aula = reader["Aula"].ToString();
                            GotoXY(trimestre, 20 - 20, last);
                            GotoXY(mat, 40 - 20, last);
                            GotoXY(aula, 90 - 20, last);
                            for (int aux = 0; aux <= arrayDia.Length - 1; aux++)
                            {

                                GotoXY(arrayDia[aux], 35, last);

                                if (aux < arrayHora.Length)
                                {

                                    GotoXY(arrayHora[aux], 45, last);
                                }

                                last++;
                            }
                            
                            last++;
                        }
                        
                        reader.Close();

                    }
                    column++;
                    last += column;
                }

            }
            else
            {
                Console.WriteLine("El alumno no posee ninguna programacion asignada");
                Console.ReadKey();
                MenuProgramaciones();
            }
            Console.ReadKey();
            MenuProgramaciones();
         }
        void BuscarSeccionesMateria(string id)
        {
            Console.Clear();
            int found = (0);
            try
            {
                DBConnect();
                using (cmd = new SqlCommand("select * from Programacion where [IDMateria]= @id", con))
                {
                    cmd.Parameters.AddWithValue("@id",id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        found += 1;
                    }
                    con.Close();
                }

                if (found > 0)
                {
                    Console.Clear();
                    GotoXY("Trimestre", 20 - 20, 0);
                    GotoXY("ID Programacion", 40 - 20, 0);
                    GotoXY("Dia", 60 - 20, 0);
                    GotoXY("Hora", 70 - 20, 0);
                    GotoXY("Aula", 95 - 20, 0);
                    int column = 1;
                    int last = column+1;
                    DBConnect();
                    
                    using (cmd = new SqlCommand("Select * from Programacion where [IDMateria]=@progID ", con))
                    {
                        cmd.Parameters.AddWithValue("@progID", id);
                        SqlDataReader reader = cmd.ExecuteReader();
                        string trimestre, ID, aula;
                        while (reader.Read())
                        {
                            Console.WriteLine("-------------------------------------------------------------------------------------------");
                            trimestre = reader["Trimestre"].ToString();
                            ID = reader["ID"].ToString();
                            string[] arrayDia = reader["dia"].ToString().Split('\n');
                            string[] arrayHora = reader["hora"].ToString().Split('\n');
                            aula = reader["Aula"].ToString();
                            GotoXY(trimestre, 20 - 20, last);
                            GotoXY(ID, 40 - 20, last);
                            GotoXY(aula, 95 - 20, last);
                            for (int aux = 0; aux <= arrayDia.Length - 1; aux++)
                            {

                                GotoXY(arrayDia[aux], 40, last);

                                if (aux < arrayHora.Length)
                                {

                                    GotoXY(arrayHora[aux], 50, last);
                                }

                                last++;
                            }

                            last++;
                        }

                        reader.Close();

                    }
                    column++;
                    last += column;
                }

                
                else { Console.WriteLine("No existe esta materia o bien no tiene ninguna programacion"); }
                con.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Console.WriteLine("Database error");
            }
            Console.ReadKey();
            MenuProgramaciones();

        }

        void EditarUsuario(String id)
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
                            using (cmd = new SqlCommand("update Alumnos set Nombre= @value where ID= @id", con))
                            {
                                cmd.Parameters.AddWithValue("@value", newInput);
                                cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                cmd.ExecuteNonQuery();
                            }
                            break;
                        case 2:
                            Console.WriteLine("-Apellido: ");
                            Console.SetCursorPosition(10, 2);
                            newInput = Console.ReadLine();
                            using (cmd = new SqlCommand("update Alumnos set Apellido= @value where ID= @id", con))
                            {
                                cmd.Parameters.AddWithValue("@value", newInput);
                                cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                cmd.ExecuteNonQuery();
                            }
                            break;
                        case 3:
                            Console.WriteLine("-Carrera: ");
                            Console.SetCursorPosition(9, 2);
                            newInput = Console.ReadLine();
                            using (cmd = new SqlCommand("update Alumnos set Carrera= @value where ID= @id", con))
                            {
                                cmd.Parameters.AddWithValue("@value", newInput);
                                cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                cmd.ExecuteNonQuery();
                            }
                            break;
                        case 4:
                            Console.WriteLine("-Iddentificador nacional: ");
                            Console.SetCursorPosition(26, 2);
                            newInput = Console.ReadLine();
                            using (cmd = new SqlCommand("update Alumnos set Identificador= @value where ID= @id", con))
                            {
                                cmd.Parameters.AddWithValue("@value", newInput);
                                cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                cmd.ExecuteNonQuery();
                            }
                            break;
                        case 5:
                            Console.WriteLine("-Fecha de nacimiento dd/mm/yyyy: ");


                            DateTime fechaNacimiento;

                            try
                            {
                                Console.SetCursorPosition(33, 2);
                                fechaNacimiento = DateTime.Parse(Console.ReadLine());
                                using (cmd = new SqlCommand("update Alumnos set Fecha= @value where ID= @id", con))
                                {
                                    cmd.Parameters.AddWithValue("@value", fechaNacimiento.ToShortDateString());
                                    cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                    cmd.ExecuteNonQuery();
                                }

                            }
                            catch (Exception)
                            {
                                using (cmd = new SqlCommand("update Alumnos set Fecha= @value where ID= @id", con))
                                {
                                    cmd.Parameters.AddWithValue("@value", "01/01/1999");
                                    cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                    cmd.ExecuteNonQuery();
                                }
                                Console.WriteLine("Formato de fecha invalido. Se establecera una fecha predeterminada");
                            }
                            ;
                            break;
                        case 6:
                            GotoXY("-Nacionalidad dominicana? \n" +
                            "1.Si \n" +
                            "2.No", 0, 2);
                            Console.SetCursorPosition(27, 2);
                            try
                            {
                                newInput = Console.ReadLine();
                                switch (newInput)
                                {
                                    case "1":
                                        using (cmd = new SqlCommand("update Alumnos set Extranjero= @value where ID= @id", con))
                                        {
                                            cmd.Parameters.AddWithValue("@value", "si");
                                            cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                            cmd.ExecuteNonQuery();
                                        }
                                        break;
                                    case "2":
                                        using (cmd = new SqlCommand("update Alumnos set Extranjero= @value where ID= @id", con))
                                        {
                                            cmd.Parameters.AddWithValue("@value", "no");
                                            cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                            cmd.ExecuteNonQuery();
                                        }
                                        break;
                                    default:
                                        GotoXY("Error en la selección. El usuario será identificado como extranjero", 0, 6);
                                        using (cmd = new SqlCommand("update Alumnos set Extranjero= @value where ID= @id", con))
                                        {
                                            cmd.Parameters.AddWithValue("@value", "si");
                                            cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                            cmd.ExecuteNonQuery();
                                        }
                                        break;
                                }
                            }
                            catch
                            {
                                GotoXY("Error en la selección. El usuario será identificado como extranjero", 0, 6);

                                using (cmd = new SqlCommand("update Alumnos set Extranjero= @value where ID= @id", con))
                                {
                                    cmd.Parameters.AddWithValue("@value", "si");
                                    cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            ;
                            break;
                        case 7:
                            GotoXY("-Estados del usuario: \n" +
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
                                        using (cmd = new SqlCommand("update Alumnos set Estado= @value where ID= @id", con))
                                        {
                                            cmd.Parameters.AddWithValue("@value", "INCOMPLETO");
                                            cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                            cmd.ExecuteNonQuery();
                                        }
                                        Console.ReadKey();
                                        break;
                                    case "2":
                                        using (cmd = new SqlCommand("update Alumnos set Estado= @value where ID= @id", con))
                                        {
                                            cmd.Parameters.AddWithValue("@value", "ACTIVO");
                                            cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                            cmd.ExecuteNonQuery();
                                        }
                                        Console.ReadKey();
                                        break;
                                    case "3":
                                        using (cmd = new SqlCommand("update Alumnos set Estado= @value where ID= @id", con))
                                        {
                                            cmd.Parameters.AddWithValue("@value", "INACTIVO");
                                            cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                            cmd.ExecuteNonQuery();
                                        }
                                        Console.ReadKey();
                                        break;
                                    case "4":
                                        using (cmd = new SqlCommand("update Alumnos set Estado= @value where ID= @id", con))
                                        {
                                            cmd.Parameters.AddWithValue("@value", "APA");
                                            cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                            cmd.ExecuteNonQuery();
                                        }
                                        Console.ReadKey();
                                        break;
                                    default:
                                        GotoXY("Error al seleccionar la opcion, el usuario será puesto como incompleto", 0, 7);
                                        using (cmd = new SqlCommand("update Alumnos set Estado= @value where ID= @id", con))
                                        {
                                            cmd.Parameters.AddWithValue("@value", "INCOMPLETO");
                                            cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                            cmd.ExecuteNonQuery();
                                        }
                                        Console.ReadKey();
                                        MenuAlumnos();
                                        break;
                                }
                            }
                            catch
                            {
                                GotoXY("Error al seleccionar la opcion, el usuario será puesto como incompleto", 0, 7);
                                using (cmd = new SqlCommand("update Alumnos set Estado= @value where ID= @id", con))
                                {
                                    cmd.Parameters.AddWithValue("@value", "INCOMPLETO");
                                    cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            break;

                    }

                    Console.Clear();
                    GotoXY("Atributo modificado correctamente", 0, 0);
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
        void EditarMateria(string id)
        {
            int found = (0);
            try
            {
                DBConnect();
                using (cmd = new SqlCommand("select * from Materias where [ID]= @id or [Code]=@id", con))
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

                            using (cmd = new SqlCommand("update Materias set ID= @value  where [ID]= @id or [Code]=@code", con))
                            {
                                cmd.Parameters.AddWithValue("@value", newInput);
                                cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                cmd.Parameters.AddWithValue("@code", id.ToString());
                                cmd.ExecuteNonQuery();
                            }
                        break;
                        case 2:
                            Console.WriteLine("-Code: ");
                            Console.SetCursorPosition(10, 2);
                            newInput = Console.ReadLine();
                            using (cmd = new SqlCommand("update Materias set Code= @value  where [ID]= @id or [Code]=@code", con))
                            {
                                cmd.Parameters.AddWithValue("@value", newInput);
                                cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                cmd.Parameters.AddWithValue("@code", id.ToString());
                                cmd.ExecuteNonQuery();
                            }
                            break;
                        case 3:
                            Console.WriteLine("-Nombre: ");
                            Console.SetCursorPosition(9, 2);
                            newInput = Console.ReadLine();
                            using (cmd = new SqlCommand("update Materias set Nombre= @value  where [ID]= @id or [Code]=@code", con))
                            {
                                cmd.Parameters.AddWithValue("@value", newInput);
                                cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                cmd.Parameters.AddWithValue("@code", id.ToString());
                                cmd.ExecuteNonQuery();
                            }
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
                                GotoXY((i - 2) + ") " + areas, xPosition, i);
                                i += 1;
                            }
                            Console.SetCursorPosition(xPosition, 2);
                            option = int.Parse(Console.ReadLine());
                            Area = materiasT[option];
                            using (cmd = new SqlCommand("update Materias set Area= @value  where [ID]= @id or [Code]=@code", con))
                            {
                                cmd.Parameters.AddWithValue("@value", Area);
                                cmd.Parameters.AddWithValue("@id", int.Parse(id));
                                cmd.Parameters.AddWithValue("@code", id.ToString());
                                cmd.ExecuteNonQuery();
                            }
                            break;

                    }

                    Console.Clear();
                    GotoXY("Atributo modificado correctamente", 0, 0);
                    Console.ReadKey();
                    con.Close();
                    MenuMaterias();
                }
                catch (Exception)
                {
                    GotoXY("El usuario no pudo ser modificado",0,8);
                    GotoXY("Presione cualquier tecla para continuar...", 0,9);
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
        void EditarProgramacion(String id)
        {
            int found = (0);
            try
            {
                DBConnect();
                using (cmd = new SqlCommand("select * from Programacion where [ID]= @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        found += 1;
                    }
                    con.Close();
                }
            }
            catch
            {
                Console.WriteLine("Database error");
            }

            if(found > 0)
            {
                DBConnect();
                byte op;
                Console.WriteLine("1)Asignar alumno a la programación \n2)Listar alumnos de la programación");
                op = byte.Parse(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        Console.Clear();
                        string idMateria="algo";
                        found = 0;
                        using (cmd = new SqlCommand("select * from Programacion where [ID] = @id", con))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                idMateria = reader["Materia"].ToString();
                                found += 1;
                                
                            }
                            reader.Close();
                            using (SqlCommand cmd = new SqlCommand("Select [ID] from Materias where [Nombre]=@id", con))
                            {
                                cmd.Parameters.AddWithValue("@id", idMateria);
                                SqlDataReader reader2 = cmd.ExecuteReader();
                                while (reader2.Read())
                                {
                                    idMateria = reader2["ID"].ToString();
                                }
                                reader2.Close();
                            }

                        }
                        if (found > 0)
                        {   
                            Console.WriteLine("Ingrese el id del estudiante: ");
                            Console.SetCursorPosition(33, 0);
                            string idAlm = Console.ReadLine();
                            found = 0;
                            using (cmd = new SqlCommand("Select * from Alumnos where [ID]=@id", con))
                            {
                                cmd.Parameters.AddWithValue("@id", idAlm);
                                SqlDataReader reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    found += 1;
                                }
                                reader.Close();
                            }
                            if (found > 0)
                            {
                                bool gotit = false;
                                using (cmd = new SqlCommand("Insert into [Seleccion] Values(@idProg, @idAlumno, @idMat)", con))
                                {
                                    cmd.Parameters.AddWithValue("@idProg", id);
                                    cmd.Parameters.AddWithValue("@idAlumno", idAlm);
                                    cmd.Parameters.AddWithValue("@idMat", idMateria);
                                    cmd.ExecuteNonQuery();
                                    gotit = true;
                                }
                                con.Close();
                                if (gotit)
                                {
                                    Console.WriteLine("Alumno agregado correctamente");
                                    Console.ReadKey();
                                    MenuProgramaciones();
                                }
                                else
                                {
                                    Console.WriteLine("Alumno no pudo ser agregado");
                                    Console.ReadKey();
                                    MenuProgramaciones();
                                }
                                
                            }
                            else
                            {
                                Console.WriteLine("El alumno no existe");
                                Console.ReadKey();
                                MenuProgramaciones();
                            }
                                
                        }
                        else
                        {
                            Console.WriteLine("La Programación no existe");
                            Console.ReadKey();
                            MenuProgramaciones();
                        }
                        break;
                    case 2:
                        Console.Clear();
                        found = 0;
                        using (cmd = new SqlCommand("select * from Programacion where [ID] = @id", con))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                found += 1;
                            }
                            reader.Close();
                        }
                        Console.SetCursorPosition(33, 0);
                        found = 0;
                        using (cmd = new SqlCommand("Select * from Seleccion where [IDProgramacion]=@id", con))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                found += 1;
                            }
                            reader.Close();
                        }
                        string[] idAlumno = new string[found+1];
                        int a = 0;
                        using (cmd = new SqlCommand("Select * from Seleccion where [IDProgramacion]=@id", con))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while(reader.Read()){
                                idAlumno[a] = reader["IDAlumno"].ToString();
                                a++;
                            }
                            reader.Close();
                            
                        }
                        a = 0;
                        if (found > 0)
                        {
                            Console.Clear();
                            GotoXY("ID Alumno", 0, 0);
                            GotoXY("Nombre", 20, 0);
                            GotoXY("Carrera", 55, 0);
                            int column = 1;
                            while ((column - 1) < found)
                            {
                                using (cmd = new SqlCommand("Select * from Alumnos where [ID]=@AlumnID ", con))
                                {
                                    cmd.Parameters.AddWithValue("@AlumnID", idAlumno[(column - 1)]);
                                    SqlDataReader reader = cmd.ExecuteReader();
                                    string Nombre, Carrera;
                                    while (reader.Read())
                                    {
                                        Nombre = reader["Nombre"].ToString() + " " + reader["Apellido"].ToString();
                                        Carrera = reader["Carrera"].ToString();
                                        GotoXY(idAlumno[column - 1], 0, column);
                                        GotoXY(Nombre, 20, column);
                                        GotoXY(Carrera, 55, column);
                                        
                                    }
                                    reader.Close();
                                }
                                column++;
                            }
                                 
                        }
                        else
                        {
                            Console.WriteLine("El alumno no posee ninguna programacion asignada");
                            Console.ReadKey();
                            MenuProgramaciones();
                        }
                        
                        break;
                }
            }
            else
            {
                Console.WriteLine("Programación no encontrada");
                Console.ReadKey();
                MenuProgramaciones();
            }
            Console.ReadKey();
            MenuProgramaciones();
        }

        void EliminarUsuario(String id)
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
        void EliminarMateria(String id)
        {
            try
            {
                int found = 0;
                DBConnect();
                using (cmd = new SqlCommand("delete from Materias where ID = @id or [Code] = @code", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@code", id.ToString());
                    found = cmd.ExecuteNonQuery();
                }
                if (found > 0)
                {
                    Console.WriteLine("Materia eliminada con exito");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                }
                else
                {
                    Console.WriteLine("Materia no existente");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                }

                Console.ReadKey();
            }

            catch (Exception)
            {
                Console.WriteLine("La materia no pudo ser eliminada");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }

            con.Close();
            MenuMaterias();
        }
        void EliminarProgramacion(String id)
        {

            try
            {
                int found = 0;
                DBConnect();
                using (cmd = new SqlCommand("delete from Programacion where ID = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    found = cmd.ExecuteNonQuery();
                }
                if (found > 0)
                {
                    Console.WriteLine("Programacion eliminada con exito");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                }
                else
                {
                    Console.WriteLine("Programacion no existente");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                }

                Console.ReadKey();
            }

            catch (Exception)
            {
                Console.WriteLine("La programacion no pudo ser eliminada");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }

            con.Close();
            MenuProgramaciones();

        } 
    }
}
