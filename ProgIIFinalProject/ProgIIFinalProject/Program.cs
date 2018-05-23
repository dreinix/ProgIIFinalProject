using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

namespace ProgIIFinalProject
{
    class Program
    {
        static string yearIdentiffier = DateTime.Today.Year.ToString();
        static string currentId = yearIdentiffier[2] +"0"+ yearIdentiffier[3];
        static List<User> userList = new List<User>();
        static List<string> idList = new List<string>();
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
                    //Console.ReadKey();
                    return;
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
            Console.WindowLeft = 0;
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
            string iD="";
            bool noExist = true;
            Random r1 = new Random();
            iD = currentId +""+ r1.Next(0, 10000);
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
                userList.Add(usuario);
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
            foreach (User estudent in userList)
            {
                gotoXY(estudent.nombre, 0, i);
                gotoXY(estudent.apellido, 20, i);
                gotoXY(estudent.ID.ToString(), 40, i);
                gotoXY(estudent.carrera, 51, i);
                gotoXY(estudent.identificadorPersonal, 70, i);
                gotoXY(estudent.fechaNacimiento.ToShortDateString(), 95, i);
                gotoXY(estudent.extrangero.ToString(), 120, i);
                gotoXY(estudent.estado, 133, i);
                i++;
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Menu();
        }
        static void BuscarUsuario(String id)
        {
            
            Console.Clear();
           
            int i = 1;
            bool aux = false;
            foreach (User estudent in userList)
            {
                if ((estudent.ID.ToString() == id)|| (estudent.identificadorPersonal == id)) 
                {
                    aux = true;
                    gotoXY("-Nombre: ", 0, 0);
                    gotoXY("-Apellido: ", 30, 0);
                    gotoXY("-ID: ", 60, 0);
                    gotoXY("-Carrera: ", 73, 0);
                    gotoXY("-Identificador nacional: ", 95, 0);
                    gotoXY("-Fecha de nacimiento ", 125, 0);
                    gotoXY("-Dominicano?", 150, 0);
                    gotoXY("-Estado", 165, 0);
                    gotoXY(estudent.nombre, 0, i);
                    gotoXY(estudent.apellido, 30, i);
                    gotoXY(estudent.ID.ToString(), 60, i);
                    gotoXY(estudent.carrera, 73, i);
                    gotoXY(estudent.identificadorPersonal, 95, i);
                    gotoXY(estudent.fechaNacimiento.ToShortDateString(), 125, i);
                    gotoXY(estudent.extrangero.ToString(), 150, i);
                    gotoXY(estudent.estado, 165, i);
                    i++;
                }
                
            }
            if (aux == false)
            {
                Console.WriteLine("El ID o Identificador nacional ingresado no coincide con ninguno de los usuarios agregados");

            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Menu();
        }
        static void EditarUsuario(String id)
        {
            Console.Clear();
            bool aux = false;
            byte opcion;
            string newInput;
            foreach (User estudent in userList)
            {
                if (estudent.ID == int.Parse(id))
                {
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
                            estudent.nombre = newInput;
                            
                            
                        break;
                        case 2:
                            Console.WriteLine("-Apellido: ");
                            Console.SetCursorPosition(10, 2);
                            newInput = Console.ReadLine();
                            estudent.apellido = newInput;
                            break;
                        case 3:
                            Console.WriteLine("-Carrera: ");
                            Console.SetCursorPosition(9, 2);
                            newInput = Console.ReadLine();
                            estudent.carrera = newInput;
                            ;
                            break;
                        case 4:
                            Console.WriteLine("-Iddentificador nacional: ");
                            Console.SetCursorPosition(26, 2);
                            newInput = Console.ReadLine();
                            estudent.identificadorPersonal = newInput;
                            ;
                            break;
                        case 5:
                            Console.WriteLine("-Fecha de nacimiento dd/mm/yyyy: ");


                            DateTime fechaNacimiento;

                            try
                            {
                                Console.SetCursorPosition(33, 2);
                                fechaNacimiento = DateTime.Parse(Console.ReadLine());
                                estudent.fechaNacimiento = fechaNacimiento;
                            }
                            catch (Exception)
                            {
                                fechaNacimiento = Convert.ToDateTime("01/01/1999");
                                Console.WriteLine("Formato de fecha invalido. Se establecera una fecha predeterminada");
                                Console.ReadKey();
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
                                        estudent.extrangero = true;
                                        break;
                                    case "2":
                                        estudent.extrangero = false;
                                        break;
                                    default:
                                        gotoXY("Error en la selección. El usuario será identificado como extranjero", 0, 6);
                                        estudent.extrangero = true;
                                        Console.ReadKey();
                                        Menu();
                                        break;
                                }
                            }
                            catch
                            {
                                gotoXY("Error en la selección. El usuario será identificado como extranjero", 0, 6);
                                
                                estudent.extrangero = true;
                                Console.ReadKey();
                                Menu();
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
                                        estudent.estado = "Incompleto";
                                        break;
                                    case "2":
                                        estudent.estado = "Activo";
                                        break;
                                    case "3":
                                        estudent.estado = "Inactivo";
                                        break;
                                    case "4":
                                        estudent.estado = "APA";
                                        break;
                                    default:
                                        gotoXY("Error al seleccionar la opcion, el usuario será puesto como incompleto", 0, 7);
                                        estudent.estado = "Incompleto";
                                        Console.ReadKey();
                                        Menu();
                                        break;
                                }
                            }
                            catch
                            {
                                gotoXY("Error al seleccionar la opcion, el usuario será puesto como incompleto", 0, 7);
                                estudent.estado = "Incompleto";
                                Console.ReadKey();
                                Menu();
                            }
                            break;

                    }
                    Console.Clear();
                    gotoXY ("Atributo modificado correctamente",0,0);
                    Console.ReadKey();
                    Menu();
                }
                
            }
                if (aux == false)
                {
                Console.Clear();
                Console.WriteLine("El ID ingresado no coincide con ninguno de los usuarios agregados");
                Console.ReadKey();
                Menu();
                }
        }
        static void EliminarUsuario(String id)
        {
            foreach (User estudent in userList)
            {
                if (estudent.ID.ToString() == id)
                {
                    userList.Remove(estudent);
                    break;
                }
            }
            Console.WriteLine("Usuario eliminado con exito");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Menu();
        }
    }
}
