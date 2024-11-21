using System;
using System.IO;

namespace PL
{
    internal class Usuario
    {
        public static void Add()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.Write("Dame el User Name: ");
            usuario.UserName = Console.ReadLine();
            Console.Write("Dame el Nombre del Usuario: ");
            usuario.Nombre = Console.ReadLine();
            Console.Write("Dame el Apellido Paterno: ");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.Write("Dame el Apellido Materno: ");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.Write("Dame el Email: ");
            usuario.Email = Console.ReadLine();
            Console.Write("Dame el Password: ");
            usuario.Password = Console.ReadLine();
            Console.Write("Dame el Sexo: ");
            usuario.Sexo = Console.ReadLine();
            Console.Write("Dame su Telefono: ");
            usuario.Telefono = Console.ReadLine();
            Console.Write("Dame Celular: ");
            usuario.Celular = Console.ReadLine();
            Console.Write("Dame su Fecha de Nacimiento: ");
            usuario.FechaNacimiento = DateTime.Parse(Console.ReadLine());
            Console.Write("Dame su CURP: ");
            usuario.CURP = Console.ReadLine();

            Console.Write("Ingresa el ID del Rol: ");
            //instanciamos la primera vez que usemos el dato
            //por lo tanto, antes de hacer la referencia en la capa BL, se tiene que instanciar aqui para poder recibirlo
            usuario.Rol = new ML.Rol();
            usuario.Rol.idRol = int.Parse(Console.ReadLine());

            //ML.Result resultado = BL.Usuario.AddSP(usuario);
            //ML.Result resultado = BL.Usuario.AddEF(usuario);
            ML.Result resultado = BL.Usuario.AddEFLINQ(usuario);
            if (resultado.Correct) Console.WriteLine("El usuario se dio de alta correctamente");
            else Console.WriteLine(resultado.ErrorMessage);
        }

        public static void Update()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.Write("Dame el ID que quieres actualizar: ");
            usuario.IdUsuario = int.Parse(Console.ReadLine());
            Console.Write("Dame el nuevo User Name: ");
            usuario.UserName = Console.ReadLine();
            Console.Write("Dame el nuevo Nombre del Usuario: ");
            usuario.Nombre = Console.ReadLine();
            Console.Write("Dame el nuevo Apellido Paterno: ");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.Write("Dame el nuevo Apellido Materno: ");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.Write("Dame el nuevo Email: ");
            usuario.Email = Console.ReadLine();
            Console.Write("Dame el nuevo Password: ");
            usuario.Password = Console.ReadLine();
            Console.Write("Dame el nuevo Sexo: ");
            usuario.Sexo = Console.ReadLine();
            Console.Write("Dame el nuevo Telefono: ");
            usuario.Telefono = Console.ReadLine();
            Console.Write("Dame el nuevo Celular: ");
            usuario.Celular = Console.ReadLine();
            Console.Write("Dame su Fecha de Nacimiento: ");
            usuario.FechaNacimiento = DateTime.Parse(Console.ReadLine());
            Console.Write("Dame su CURP: ");
            usuario.CURP = Console.ReadLine();

            Console.Write("Ingresa el nuevo ID del Rol: ");
            //instanciamos la primera vez que usemos el dato
            //por lo tanto, antes de hacer la referencia en la capa BL, se tiene que instanciar aqui para poder recibirlo
            usuario.Rol = new ML.Rol();
            usuario.Rol.idRol = int.Parse(Console.ReadLine());

            //ML.Result resultado = BL.Usuario.UpdateSP(usuario);
            //ML.Result resultado = BL.Usuario.UpdateEF(usuario);
            ML.Result resultado = BL.Usuario.UpdateEFLINQ(usuario);
            if (resultado.Correct) Console.WriteLine("El usuario fue Actualizado Correctamente");
            else Console.WriteLine(resultado.ErrorMessage);

        }

        public static void Delete()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.Write("Dame el ID del usuario que quieres borrar: ");
            usuario.IdUsuario = int.Parse(Console.ReadLine());

            //ML.Result resultado = BL.Usuario.Delete(usuario.id);
            //ML.Result resultado = BL.Usuario.DeleteEF(usuario.IdUsuario);
            ML.Result resultado = BL.Usuario.DeleteEF(usuario.IdUsuario);
            if (resultado.Correct) Console.WriteLine("Registro Borrado");
            else Console.WriteLine(resultado.ErrorMessage);

        }

        public static void GetAll()
        {
            //ML.Result result = BL.Usuario.GetAllSP();
            ML.Result result = BL.Usuario.GetAllLINQ();
            if (result.Correct)
            {
                foreach (ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine("\n---------------------------------------------------");
                    Console.WriteLine("ID: " + usuario.IdUsuario);
                    Console.WriteLine("ID: " + usuario.UserName);
                    Console.WriteLine("Nombre: " + usuario.Nombre);
                    Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno);
                    Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno);
                    Console.WriteLine("Email: " + usuario.Email);
                    Console.WriteLine("Password: " + usuario.Password);
                    Console.WriteLine("Sexo: " + usuario.Sexo);
                    Console.WriteLine("Telefono: " + usuario.Telefono);
                    Console.WriteLine("Celular: " + usuario.Celular);
                    Console.WriteLine("Fecha Nacimiento: " + usuario.FechaNacimiento);
                    Console.WriteLine("CURP: " + usuario.CURP);

                    Console.WriteLine("ID Rol: " + usuario.Rol.idRol);
                    Console.WriteLine("Nombre Rol: " + usuario.Rol.Nombre);
                    Console.WriteLine("---------------------------------------------------\n");
                }
            }
        }

        public static void GetById()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.Write("Dame el ID que quieres mostrar: ");
            usuario.IdUsuario = int.Parse(Console.ReadLine());

            //ML.Result result = BL.Usuario.GetById(usuario.id);
            //ML.Result result = BL.Usuario.GetByIdEF(usuario.IdUsuario);
            ML.Result result = BL.Usuario.GetByIdEFLINQ(usuario.IdUsuario);

            if (result.Correct)
            {
                //unboxing, recibiendo datos de la capa BL
                usuario = (ML.Usuario)result.Object;

                Console.WriteLine("\n---------------------------------------------------");
                Console.WriteLine("ID: " + usuario.IdUsuario);
                Console.WriteLine("ID: " + usuario.UserName);
                Console.WriteLine("Nombre: " + usuario.Nombre);
                Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno);
                Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno);
                Console.WriteLine("Email: " + usuario.Email);
                Console.WriteLine("Password: " + usuario.Password);
                Console.WriteLine("Sexo: " + usuario.Sexo);
                Console.WriteLine("Telefono: " + usuario.Telefono);
                Console.WriteLine("Celular: " + usuario.Celular);
                Console.WriteLine("Fecha Nacimiento: " + usuario.FechaNacimiento);
                Console.WriteLine("CURP: " + usuario.CURP);

                Console.WriteLine("ID Rol: " + usuario.Rol.idRol);
                Console.WriteLine("Nombre Rol: " + usuario.Rol.Nombre);
                Console.WriteLine("---------------------------------------------------\n");
            }
            else
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }

        public static void CargaMasivaTxt()
        {
            //el @ significa escapado de strings, quiere decir que lo que tenga en la cadena lo tomara como caracteres 
            string file = @"C:\Users\digis\OneDrive\Documentos\Jorge Guevara Flores\JGuevaraProgramacionNCapas\WebMVC\Files\CargaMasivaUsuario.txt";

            if (File.Exists(file))
            {
                int contInsert = 0;

                StreamReader streamReader = new StreamReader(file);

                string line = streamReader.ReadLine(); //SALTAR HEDEARS

                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] row = line.Split('|');
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.UserName = row[0];
                    usuario.Nombre = row[1];
                    usuario.ApellidoPaterno = row[2];
                    usuario.ApellidoMaterno = row[3];
                    usuario.Email = row[4];
                    usuario.Password = row[5];
                    usuario.Sexo = row[6];
                    usuario.Telefono = row[7];
                    usuario.Celular = row[8];
                    usuario.FechaNacimiento = DateTime.Parse(row[9]);
                    usuario.CURP = row[10];

                    usuario.Rol = new ML.Rol();
                    usuario.Rol.idRol = int.Parse(row[11]);

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.IdColonia = int.Parse(row[12]);
                    usuario.Direccion.Calle = row[13];
                    usuario.Direccion.NumeroInterior = row[14];
                    usuario.Direccion.NumeroExterior = row[15];


                    ML.Result userAdd = BL.Usuario.AddEF(usuario);

                    if (userAdd.Correct)
                    {
                        contInsert++;
                    }

                }

                Console.WriteLine("Se insertaron "+contInsert+" usuarios");

            }
        }

    }
}
