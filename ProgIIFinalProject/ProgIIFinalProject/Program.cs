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
          
            
            Console.SetCursorPosition(5, 0);
            Console.WriteLine("*****Menú*****");
            Console.WriteLine("1. Agregar usuarios \n" +
                "2. Modificar usuarios \n" +
                "3. Visualizar usuarios \n" +
                "4. Buscar \n" +
                "5. Eliminar usuarios \n" +
                "6. Salir \n");
            try
            {
                opcion = byte.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                opcion = 0;
            }
            switch (opcion)
            {
                case 1:
                    AgregarUsuario();
                    break;
                case 2:

                    break;
                case 3:

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

            Menu();
        }

        static void AgregarUsuario()
        {
            User usuario = new User();


            Menu();
        }
    }
}
