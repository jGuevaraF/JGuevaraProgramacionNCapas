using BL;
using System;
using System.Security.Cryptography.X509Certificates;

namespace PL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0;

            do
            {
                Console.WriteLine("\nQue opcion quieres hacer?");
                Console.WriteLine("1. Alta");
                Console.WriteLine("2. Baja");
                Console.WriteLine("3. Cambiar registro");
                Console.WriteLine("4. Consultar todos los registros");
                Console.WriteLine("5. Consultar registro especifico");
                Console.WriteLine("6. Carga masiva\n");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        PL.Usuario.Add();
                        break;
                    case 2:
                        PL.Usuario.Delete();
                        break;

                    case 3:
                        PL.Usuario.Update();
                        break;
                    case 4:
                        PL.Usuario.GetAll();
                        break;
                    case 5:
                        PL.Usuario.GetById();
                        break;
                    case 6:
                        PL.Usuario.CargaMasivaTxt();
                        Console.ReadKey();
                        break;
                }

            } while (opcion != 7);

        }
    }
}
