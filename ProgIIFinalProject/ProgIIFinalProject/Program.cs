using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgIIFinalProject
{
    class Program
    {
        static List<User> userList = new List<User>();
        static void Menu()
        {
            byte opcion;

            Console.Clear();
            Console.SetCursorPosition(5, 0);
            Console.WriteLine("*****Menú*****");
            Console.WriteLine("1. Agregar usuarios \n" +
                "2. Visualizar usuarios \n" +
                "3. Buscar \n" +
                "4. Modificar usuarios \n" +
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
                    string iD = Console.ReadLine();
                    BuscarUsuario(iD);
                    break;
                case 4:

                    break;
                case 5:

                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Gracias por utilizar nuestros servicios");
                    Console.ReadKey();
                    return;
                default:
                    Console.WriteLine("Opcion invalida,intenté de nuevo");
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                    break;
            }
            Console.ReadKey();
        }
        static void Main(string[] args)
        {

            Console.SetWindowSize(Convert.ToInt32(Console.LargestWindowWidth * 0.8), Convert.ToInt32(Console.LargestWindowHeight*0.8));
            Console.SetWindowPosition(0, 0);
            Menu();
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
            String nombre, apellido, estado, ID, carrera, identificador;
            bool extrangero;
            DateTime fechaNacimiento;

            gotoXY("Nombre: ", xPosition, 1);
            xPosition += 8;
            Console.SetCursorPosition(xPosition, 1);
            nombre  = Console.ReadLine();
            xPosition += nombre.Length + 1;

            gotoXY("|Apellido: ", xPosition, 1);
            xPosition += 10;
            Console.SetCursorPosition(xPosition, 1);
            apellido = Console.ReadLine();
            xPosition += apellido.Length + 1;

            gotoXY("|ID: ", xPosition, 1);
            xPosition += 5;
            Console.SetCursorPosition(xPosition, 1);
            ID = Console.ReadLine();
            xPosition += ID.Length + 1;

            gotoXY("|Carrera: ", xPosition, 1);
            xPosition += 9;
            Console.SetCursorPosition(xPosition, 1);
            carrera = Console.ReadLine();
            xPosition += carrera.Length + 1;

            gotoXY("|Identificador nacional: ", xPosition, 1);
            xPosition += 26;
            Console.SetCursorPosition(xPosition, 1);
            identificador = Console.ReadLine();
            xPosition =0;

            gotoXY("|Fecha de nacimiento mm/dd/yyyy: ", xPosition, 2);
            xPosition += 33;
            Console.SetCursorPosition(xPosition, 2);
            fechaNacimiento = DateTime.Parse(Console.ReadLine());
            xPosition += fechaNacimiento.ToString().Length + 1;

            Console.Clear();
            gotoXY("|Nacionalidad dominicana? \n" +
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
                        Console.WriteLine("Error al seleccionar la opcion, el usuario será puesto como extrangero");
                        extrangero = true;
                        Console.ReadKey();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Error en la selección. El usuario será identificado como extrangero");
                extrangero = true;
                Console.ReadKey();
            }
                

            Console.Clear();
            gotoXY("|Estados del usuario: \n" +
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
                        Console.WriteLine("Error al seleccionar la opcion, el usuario será puesto como incompleto");
                        estado = "Incompleto";
                        Console.ReadKey();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Error al seleccionar la opcion, el usuario será puesto como incompleto");
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
                usuario.ID = ID;
                usuario.identificadorPersonal = identificador;
                usuario.extrangero = extrangero;
                userList.Add(usuario);
                Console.WriteLine("usuario agregado con exito");
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
            gotoXY("Nombre: ", 0, 0);
            gotoXY("|Apellido: ", 30, 0);
            gotoXY("|ID: ", 60, 0);
            gotoXY("|Carrera: ",73, 0);
            gotoXY("|Identificador nacional: ", 95, 0);
            gotoXY("|Fecha de nacimiento ", 125, 0);
            gotoXY("|dominicano?", 150, 0);
            gotoXY("|Estado", 165, 0);
            int i = 1;
            foreach (User estudent in userList)
            {
                gotoXY(estudent.nombre, 0, i);
                gotoXY(estudent.apellido, 30, i);
                gotoXY(estudent.ID, 60, i);
                gotoXY(estudent.carrera, 73, i);
                gotoXY(estudent.identificadorPersonal, 95, i);
                gotoXY(estudent.fechaNacimiento.ToShortDateString(), 125, i);
                gotoXY(estudent.extrangero.ToString(), 150, i);
                gotoXY(estudent.estado, 165, i);
                i++;
            }
        }
        static void BuscarUsuario(String id)
        {
            Console.Clear();
            gotoXY("Nombre: ", 0, 0);
            gotoXY("|Apellido: ", 30, 0);
            gotoXY("|ID: ", 60, 0);
            gotoXY("|Carrera: ", 73, 0);
            gotoXY("|Identificador nacional: ", 95, 0);
            gotoXY("|Fecha de nacimiento ", 125, 0);
            gotoXY("|dominicano?", 150, 0);
            gotoXY("|Estado", 165, 0);
            int i = 1;
            foreach (User estudent in userList)
            {
                if (estudent.ID == id)
                {
                    gotoXY(estudent.nombre, 0, i);
                    gotoXY(estudent.apellido, 30, i);
                    gotoXY(estudent.ID, 60, i);
                    gotoXY(estudent.carrera, 73, i);
                    gotoXY(estudent.identificadorPersonal, 95, i);
                    gotoXY(estudent.fechaNacimiento.ToShortDateString(), 125, i);
                    gotoXY(estudent.extrangero.ToString(), 150, i);
                    gotoXY(estudent.estado, 165, i);
                    i++;
                }
                
            }
        }
    }
}
