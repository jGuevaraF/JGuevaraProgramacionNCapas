using DL;
using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Objects;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;


namespace BL
{
    public class Usuario
    {

        public static ML.Result AddEFLINQ( ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    //creamos un objeto de tipo DL_EF, 
                    DL_EF.Usuario addLINQ = new DL_EF.Usuario();
                    addLINQ.UserName = usuario.UserName;
                    addLINQ.Nombre = usuario.Nombre;
                    addLINQ.ApellidoPaterno = usuario.ApellidoPaterno;
                    addLINQ.ApellidoMaterno = usuario.ApellidoMaterno;
                    addLINQ.Email = usuario.Email;
                    addLINQ.Password = usuario.Password;
                    addLINQ.Sexo = usuario.Sexo;
                    addLINQ.Telefono = usuario.Telefono;
                    addLINQ.Celular = usuario.Celular;
                    addLINQ.FechaNacimiento = usuario.FechaNacimiento;
                    addLINQ.CURP = usuario.CURP;
                    addLINQ.IdRol = usuario.Rol.idRol;

                    context.Usuarios.Add(addLINQ);
                    int filasAfectadas = context.SaveChanges();

                    if (filasAfectadas > 0) result.Correct = true;
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo insertar el registro";
                    }

                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result AddEF (ML.Usuario usuario) // metodo con EF con SP
        {

            ML.Result result = new ML.Result ();
            try
            {
                //Traemos la conexion de la BD que se hizo con EF
                using(DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    //creamos Objeto para especificar el parametro de salida
                    ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));

                    //var query = context.UsuarioADD(usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, usuario.Imagen, usuario.Rol.idRol, usuario.Direccion.Colonia.IdColonia, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, filasAfectadas);
                    var query = 2;

                    if (query == 2) result.Correct = true;
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El usuario no se pudo dar de alta";
                    }
                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }
            return result;
        }

        //public static ML.Result AddSP(ML.Usuario usuario) // metodo implementado con la clase Result y el SP en la BD
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "UsuarioADD";
        //            //creamos el objeto cmd para poder indicar que el query es un SP
        //            SqlCommand cmd = new SqlCommand();
        //            //asignamos la query a tipo texto
        //            cmd.CommandText = query;
        //            //lo cambiamos a tipo SP
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            //establecemos la conexion
        //            cmd.Connection = context;

        //            SqlParameter[] collection = new SqlParameter[7];
        //            collection[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
        //            collection[0].Value = usuario.Nombre;
        //            collection[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
        //            collection[1].Value = usuario.ApellidoPaterno;
        //            collection[2] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
        //            collection[2].Value = usuario.ApellidoMaterno;
        //            collection[3] = new SqlParameter("@FechaNacimiento", SqlDbType.Date);
        //            collection[3].Value = usuario.FechaNacimiento;
        //            collection[4] = new SqlParameter("@Peso", SqlDbType.Float);
        //            collection[4].Value = usuario.Peso;
        //            collection[5] = new SqlParameter("@Direccion", SqlDbType.VarChar);
        //            collection[5].Value = usuario.Direccion;
        //            //instanciamos la clase Rol, la primera vez que lo usamos
        //            //usuario.Rol = new ML.Rol();
        //            collection[6] = new SqlParameter("@idRol", SqlDbType.Int);
        //            collection[6].Value = usuario.Rol.idRol;

        //            //agregamos las referencias al objeto cmd de SqlCommand
        //            cmd.Parameters.AddRange(collection);
        //            cmd.Connection.Open();

        //            int filasAfectadas = cmd.ExecuteNonQuery();
        //            if (filasAfectadas > 0)
        //            {
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se pudo insertar el registro";
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;
        //}

        //public static ML.Result Add2(ML.Usuario usuario) //metodo implementando la clase Result de la capa ML
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "INSERT INTO Usuario VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento, @Peso, @Direccion)";

        //            SqlCommand cmd = new SqlCommand(query, context);

        //            SqlParameter[] collection = new SqlParameter[6];
        //            cmd.Parameters.AddWithValue("@ID", id); EJEMPLO

        //            collection[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
        //            collection[0].Value = usuario.Nombre;
        //            collection[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
        //            collection[1].Value = usuario.ApellidoPaterno;
        //            collection[2] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
        //            collection[2].Value = usuario.ApellidoMaterno;
        //            collection[3] = new SqlParameter("@FechaNacimiento", SqlDbType.Date);
        //            collection[3].Value = usuario.FechaNacimiento;
        //            collection[4] = new SqlParameter("@Peso", SqlDbType.Float);
        //            collection[4].Value = usuario.Peso;
        //            collection[5] = new SqlParameter("@Direccion", SqlDbType.VarChar);
        //            collection[5].Value = usuario.Direccion;

        //            cmd.Parameters.AddRange(collection);
        //            cmd.Connection.Open();

        //            int filasAfectadas = cmd.ExecuteNonQuery();
        //            if (filasAfectadas > 0)
        //            {
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct= false;
        //                result.ErrorMessage = "Error al agregar el usuario";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;
        //}

        //public static string Add3(ML.Usuario usuario) //metodo normal, logica
        //{
        //    string resultado = "";
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "INSERT INTO Usuario VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento, @Peso, @Direccion)";

        //            SqlCommand cmd = new SqlCommand(query, context);

        //            SqlParameter[] collection = new SqlParameter[6];

        //            collection[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
        //            collection[0].Value = usuario.Nombre;
        //            collection[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
        //            collection[1].Value = usuario.ApellidoPaterno;
        //            collection[2] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
        //            collection[2].Value = usuario.ApellidoMaterno;
        //            collection[3] = new SqlParameter("@FechaNacimiento", SqlDbType.Date);
        //            collection[3].Value = usuario.FechaNacimiento;
        //            collection[4] = new SqlParameter("@Peso", SqlDbType.Float);
        //            collection[4].Value = usuario.Peso;
        //            collection[5] = new SqlParameter("@Direccion", SqlDbType.VarChar);
        //            collection[5].Value = usuario.Direccion;

        //            cmd.Parameters.AddRange(collection);
        //            cmd.Connection.Open();

        //            int filasAfectadas = cmd.ExecuteNonQuery();
        //            if (filasAfectadas > 0)
        //            {
        //                resultado = "registro exitoso";
        //            }
        //            else
        //            {
        //                resultado = "error";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado = ex.Message;
        //    }

        //    return resultado;
        //} 



        //public static ML.Result encontrarID(int id)
        //{
        //    ML.Result find = new ML.Result();
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "SELECT * FROM Usuario WHERE id = @ID";

        //            SqlCommand cmd = new SqlCommand(query, context);
        //            cmd.Parameters.AddWithValue("@ID", id);

        //            cmd.Connection.Open();

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            if (reader.HasRows)
        //            {
        //                find.Correct = true;
        //            }
        //            else
        //            {
        //                find.Correct = false;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        find.Correct= false;
        //        find.ErrorMessage = ex.Message;
        //        find.Ex = ex;
        //    }
        //    return find;

        //}


        public static ML.Result UpdateEFLINQ (ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = (from objUsuario in context.Usuarios
                                 where objUsuario.IdUsuario == usuario.IdUsuario
                                 select objUsuario).SingleOrDefault();

                    if(query != null)
                    {
                        query.UserName = usuario.UserName;
                        query.Nombre = usuario.Nombre;
                        query.ApellidoPaterno = usuario.ApellidoPaterno;
                        query.ApellidoMaterno = usuario.ApellidoMaterno;
                        query.Email = usuario.Email;
                        query.Password = usuario.Password;  
                        query.Sexo = usuario.Sexo;
                        query.Telefono = usuario.Telefono;
                        query.Celular = usuario.Celular;
                        query.FechaNacimiento = usuario.FechaNacimiento;
                        query.CURP = usuario.CURP;
                        query.IdRol = usuario.Rol.idRol;

                        context.SaveChanges();
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "ID no encontrado";
                    }

                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result UpdateEF(ML.Usuario usuario) //update usando Entity Framework
        {
            ML.Result result = new ML.Result();
            try
            {
                //traemos la conexion de la BD de EF
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));

                    int query = context.UsuarioUpdate(usuario.IdUsuario, usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, usuario.Imagen, usuario.Rol.idRol, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia, filasAfectadas);

                    if ((int)filasAfectadas.Value == 2) result.Correct = true;
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el ID a actualizar";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        //public static ML.Result UpdateSP (ML.Usuario usuario) //metodo implementado con la clase Result y el SP en la BD
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "UsuarioUpdate";
        //            //creamos el objeto cmd para poder indicar que el query es un SP
        //            SqlCommand cmd = new SqlCommand();
        //            //asignamos la query a tipo texto
        //            cmd.CommandText = query;
        //            //lo cambiamos a tipo SP
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            //establecemos la conexion con la BD
        //            cmd.Connection = context;

        //            //Agregamos los parametros con AddWithValue
        //            cmd.Parameters.AddWithValue("@ID", usuario.id);
        //            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
        //            cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
        //            cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
        //            cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
        //            cmd.Parameters.AddWithValue("@Peso", usuario.Peso);
        //            cmd.Parameters.AddWithValue("@Direccion", usuario.Direccion);
        //            cmd.Parameters.AddWithValue("@idRol", usuario.Rol.idRol);

        //            //todo listo, abrimos conexion con la BD
        //            cmd.Connection.Open();

        //            int filasAfectadas = cmd.ExecuteNonQuery();

        //            if (filasAfectadas > 0)
        //            {
        //                result.Correct = true;
        //            } else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se pudo actualizar porque no se encontro el ID";
        //            }

        //        }

        //    } catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;
        //}

        //public static ML.Result Update2(ML.Usuario usuario, int id) //metodo implementando la clase Result de la capa ML
        //{
        //    ML.Result update = new ML.Result();
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "UPDATE Usuario SET Nombre=@Nombre, ApellidoPaterno=@ApellidoPaterno, ApellidoMaterno=@ApellidoMaterno, FechaNacimiento=@FechaNacimiento, Peso=@Peso, Direccion=@Direccion WHERE id=@ID";

        //            SqlCommand cmd = new SqlCommand(query, context);

        //            SqlParameter[] collection = new SqlParameter[7];

        //            collection[0] = new SqlParameter("@id", SqlDbType.Int);
        //            collection[0].Value = id;
        //            collection[1] = new SqlParameter("@Nombre", SqlDbType.VarChar);
        //            collection[1].Value = usuario.Nombre;
        //            collection[2] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
        //            collection[2].Value = usuario.ApellidoPaterno;
        //            collection[3] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
        //            collection[3].Value = usuario.ApellidoMaterno;
        //            collection[4] = new SqlParameter("@FechaNacimiento", SqlDbType.Date);
        //            collection[4].Value = usuario.FechaNacimiento;
        //            collection[5] = new SqlParameter("@Peso", SqlDbType.Float);
        //            collection[5].Value = usuario.Peso;
        //            collection[6] = new SqlParameter("@Direccion", SqlDbType.VarChar);
        //            collection[6].Value = usuario.Direccion;

        //            cmd.Parameters.AddRange(collection);
        //            cmd.Connection.Open();

        //            int filasAfectadas = cmd.ExecuteNonQuery();
        //            if (filasAfectadas > 0)
        //            {
        //                update.Correct = true;
        //            }
        //            else
        //            {
        //                update.Correct = false;
        //                update.ErrorMessage = "Error al actualizar";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        update.Correct = false;
        //        update.ErrorMessage = ex.Message;
        //        update.Ex = ex;
        //    }

        //    return update;
        //}



        public static ML.Result DeleteEFLINQ (int id)
        {
            ML.Result result = new ML.Result();

            try
            {

                using(DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities() )
                {
                    var query = (from objUsuario in context.Usuarios
                                 where objUsuario.IdUsuario == id
                                 select objUsuario).SingleOrDefault();

                    if (query != null)
                    {
                        context.Usuarios.Remove(query);
                        context.SaveChanges();
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el ID";
                    }
                }


            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result DeleteEF(int id) //metodo con EF y el SP de la BD
        {
            ML.Result result = new ML.Result();
            try
            {
                //Traemos la conexion de la BD con EF
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));

                    int query = context.UsuarioDelete(id, filasAfectadas);

                    if (query == 3) result.Correct = true;
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el ID";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        //public static ML.Result DeleteSP (int id) //Metodo con SP de la BD
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using(SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "UsuarioDelete";
        //            //creamos el objeto cmd para poder indicar que el query es un SP
        //            SqlCommand cmd = new SqlCommand();
        //            //asignamos la query a tipo texto
        //            cmd.CommandText = query;
        //            //lo cambiamos a tipo SP
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            //establecemos la conexion
        //            cmd.Connection = context;

        //            cmd.Parameters.AddWithValue("@ID", id);
        //            cmd.Connection.Open ();

        //            int filasAfectadas = cmd.ExecuteNonQuery();

        //            if(filasAfectadas > 0)
        //            {
        //                result.Correct = true;
        //            } else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se encontro el ID a borrar";
        //            }

        //        }

        //    } catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;
        //}

        //public static ML.Result Delete2(int id) //metodo implementando la clase Result de la capa ML
        //{
        //   ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "DELETE FROM Usuario WHERE id = @ID";

        //            SqlCommand cmd = new SqlCommand(query, context);
        //            cmd.Parameters.AddWithValue("@ID", id);

        //            cmd.Connection.Open();

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            if (reader.HasRows)
        //            {
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct= false;
        //                result.ErrorMessage = "No se pudo eliminar el usuario";
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}



        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = (from objUsuario in context.Usuarios
                                    join rol in context.Rols on objUsuario.IdRol equals rol.IdRol
                                    select new {
                                        IdUsuario = objUsuario.IdUsuario,
                                        UserName = objUsuario.UserName,
                                        NombreUsuario = objUsuario.Nombre,
                                        ApellidoPaterno = objUsuario.ApellidoPaterno,
                                        ApellidoMaterno = objUsuario.ApellidoMaterno,
                                        Email = objUsuario.Email,
                                        Password = objUsuario.Password,
                                        Sexo = objUsuario.Sexo,
                                        Telefono = objUsuario.Telefono,
                                        Celular = objUsuario.Celular,
                                        FechaNacimiento = objUsuario.FechaNacimiento,
                                        CURP = objUsuario.CURP,
                                        IdRol = objUsuario.IdRol,
                                        NombreRol = rol.Nombre
                                    }).ToList();

                    result.Objects = new List<object>();

                    if(query.Count > 0)
                    {
                        foreach (var usuario in query)
                        {
                            ML.Usuario prop = new ML.Usuario();
                            prop.IdUsuario = usuario.IdUsuario;
                            prop.UserName = usuario.UserName;
                            prop.Nombre = usuario.NombreUsuario;
                            prop.ApellidoPaterno = usuario.ApellidoPaterno;
                            prop.ApellidoMaterno = usuario.ApellidoMaterno;
                            prop.Email = usuario.Email;
                            prop.Password = usuario.Password;
                            prop.Sexo = usuario.Sexo;
                            prop.Telefono = usuario.Telefono;
                            prop.Celular = usuario.Celular;
                            prop.FechaNacimiento = (DateTime)usuario.FechaNacimiento;
                            prop.CURP = usuario.CURP;


                            //instanciamos ROL
                            prop.Rol = new ML.Rol();
                            prop.Rol.idRol = (int)usuario.IdRol;
                            prop.Rol.Nombre = usuario.NombreRol;

                            result.Objects.Add(prop);

                        }

                        result.Correct = true;
                    }
                }

            } catch(Exception ex)
            {
                result.Correct= false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public static ML.Result GetAllEF(ML.Usuario buscarUsuario) //Metodo usando EF y el SP de la BD
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var usuarios = context.UsuarioGetAll(buscarUsuario.Nombre, buscarUsuario.ApellidoPaterno).ToList();
                    result.Objects = new List<object>();

                    if (usuarios.Count > 0)
                    {
                        foreach (var usuario in usuarios)
                        {
                            ML.Usuario prop = new ML.Usuario();
                            prop.IdUsuario = usuario.IdUsuario;
                            prop.UserName = usuario.UserName;
                            prop.Nombre = usuario.Nombre;
                            prop.ApellidoPaterno = usuario.ApellidoPaterno;
                            prop.ApellidoMaterno = usuario.ApellidoMaterno;
                            prop.Email = usuario.Email;
                            prop.Password = usuario.Password;
                            prop.Sexo = usuario.Sexo;
                            prop.Telefono = usuario.Telefono;
                            prop.Celular = usuario.Celular;
                            prop.FechaNacimiento = (DateTime)usuario.FechaNacimiento;
                            prop.CURP = usuario.CURP;
                            prop.Imagen = usuario.Imagen;
                            prop.Status = usuario.Status;


                            //instanciamos TODO
                            prop.Rol = new ML.Rol();
                            prop.Direccion = new ML.Direccion();
                            prop.Direccion.Colonia = new ML.Colonia();
                            prop.Direccion.Colonia.Municipio = new ML.Municipio();
                            prop.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            prop.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();


                            prop.Rol.idRol = (int)usuario.idRol;
                            prop.Rol.Nombre = usuario.NombreRol;

                            prop.Direccion.Colonia.Municipio.Estado.Pais.IdPais = usuario.IdPais;
                            prop.Direccion.Colonia.Municipio.Estado.Pais.Nombre = usuario.NombrePais;

                            prop.Direccion.Colonia.Municipio.Estado.IdEstado = usuario.IdEstado;
                            prop.Direccion.Colonia.Municipio.Estado.Nombre = usuario.NombreEstado;

                            prop.Direccion.Colonia.Municipio.IdMunicipio = usuario.IdMunicipio;
                            prop.Direccion.Colonia.Municipio.Nombre = usuario.NombreMunicipio;

                            prop.Direccion.Colonia.IdColonia = usuario.IdColonia;
                            prop.Direccion.Colonia.Nombre = usuario.NombreColonia;
                            prop.Direccion.Colonia.CodigoPostal = usuario.CodigoPostal;

                            prop.Direccion.IdDireccion = usuario.IdDireccion;
                            prop.Direccion.Calle = usuario.Calle;
                            prop.Direccion.NumeroExterior = usuario.NumeroExterior;
                            prop.Direccion.NumeroInterior = usuario.NumeroInterior;

                            result.Objects.Add(prop);

                        }

                        result.Correct = true;
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        //public static ML.Result GetAllSP() //Metodo usando el SP creado en la BD
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using(SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "UsuarioGetAll";
        //            //creamos el objeto cmd para poder indicar que el query es un SP
        //            SqlCommand cmd = new SqlCommand();
        //            //asignamos la query a tipo texto
        //            cmd.CommandText = query;
        //            //lo cambiamos a tipo SP
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            //establecemos la conexion
        //            cmd.Connection = context;

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataTable tablaUsuario = new DataTable();

        //            adapter.Fill(tablaUsuario);

        //            if(tablaUsuario.Rows.Count > 0)
        //            {
        //                result.Objects = new List<object>();
        //                foreach (DataRow row in tablaUsuario.Rows)
        //                {
        //                    ML.Usuario usuario = new ML.Usuario();
        //                    //sacamos los datos de row
        //                    usuario.id = int.Parse(row[0].ToString());
        //                    usuario.Nombre = row[1].ToString();
        //                    usuario.ApellidoPaterno = row[2].ToString();
        //                    usuario.ApellidoMaterno = row[3].ToString();
        //                    usuario.FechaNacimiento = DateTime.Parse(row[4].ToString());
        //                    usuario.Peso = float.Parse(row[5].ToString());
        //                    usuario.Direccion = row[6].ToString();
        //                    //como no hemos solicitado el dato de idRol, y esta es la primera vez que lo usaremos
        //                    //instanciaremos aqui la clase
        //                    usuario.Rol = new ML.Rol();
        //                    usuario.Rol.Nombre = row[7].ToString();

        //                    result.Objects.Add(usuario);
        //                }

        //                result.Correct = true;

        //            } else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No hay registros en la tabla";
        //            }
        //        }

        //    } catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}

        //public static ML.Result GetAll2() //metodo implementando la clase Result de la capa ML
        //{
        //    ML.Result select = new ML.Result();
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "SELECT id,Nombre,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,Peso,Direccion FROM Usuario";

        //            SqlCommand cmd = new SqlCommand(query, context);
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataTable tablaUsuario = new DataTable();

        //            //LLenamos el adapter con la tabla
        //            adapter.Fill(tablaUsuario);


        //            if (tablaUsuario.Rows.Count > 0)
        //            {
        //                //Se instancia la lista para poder llenarla
        //                select.Objects = new List<object>();
        //                foreach(DataRow row in tablaUsuario.Rows)
        //                {
        //                    ML.Usuario usuario = new ML.Usuario();

        //                    usuario.id = int.Parse(row[0].ToString());
        //                    usuario.Nombre = row[1].ToString();
        //                    usuario.ApellidoPaterno = row[2].ToString();
        //                    usuario.ApellidoMaterno = row[3].ToString();
        //                    usuario.FechaNacimiento = DateTime.Parse(row[4].ToString());
        //                    usuario.Peso = float.Parse(row[5].ToString());
        //                    usuario.Direccion = row[6].ToString();

        //                    select.Objects.Add(usuario);
        //                }

        //                select.Correct = true;

        //                //foreach(var user  in select.Objects)
        //                //{
        //                //    //Usamos una cadena interpolada.
        //                //    //Castemaos el object de la fila a tipo ML.Usuario y asi poder acceder a sus propiedades

        //                //    Console.WriteLine($"ID: {((ML.Usuario)user).id}");
        //                //    Console.WriteLine($"Nombre: {((ML.Usuario)user).Nombre}");
        //                //    Console.WriteLine($"Apellido Paterno: {((ML.Usuario)user).ApellidoPaterno}");
        //                //    Console.WriteLine($"Apellido Materno: {((ML.Usuario)user).ApellidoMaterno}");
        //                //    Console.WriteLine($"Fecha de Nacimiento: {((ML.Usuario)user).FechaNacimiento}");
        //                //    Console.WriteLine($"Peso: {((ML.Usuario)user).Peso}");
        //                //    Console.WriteLine($"Direccion: {((ML.Usuario)user).Direccion}");
        //                //    Console.WriteLine("----------------------------------------------");

        //                //    //Console.WriteLine(usuario.id[user]);
        //                //}
        //            } else
        //            {
        //                select.Correct = false;
        //                select.ErrorMessage = "La tabla no tiene registros";
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        select.Correct = false;
        //        select.ErrorMessage = ex.Message;
        //        select.Ex = ex;
        //    }

        //    return select;

        //}

        //public static void GetAll3() //metodo normal, logica
        //{
        //    string resultado = "";
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "SELECT * FROM Usuario";

        //            SqlCommand cmd = new SqlCommand(query, context);

        //            cmd.Connection.Open();

        //            SqlDataReader registro = cmd.ExecuteReader();
        //            if (registro.HasRows)
        //            {
        //                while (registro.Read())
        //                {
        //                    // Mostrar el resultado por pantalla
        //                    Console.WriteLine("ID: " + registro["id"].ToString());
        //                    Console.WriteLine("Nombre: "+registro["Nombre"].ToString());
        //                    Console.WriteLine("Apellido Paterno: "+registro["ApellidoPaterno"].ToString());
        //                    Console.WriteLine("Apellido Materno: "+registro["ApellidoMaterno"].ToString());
        //                    Console.WriteLine("Fecha de Nacimiento: "+registro["FechaNacimiento"].ToString());
        //                    Console.WriteLine("Peso: "+registro["Peso"].ToString());
        //                    Console.WriteLine("Dirección: " + registro["Direccion"].ToString());
        //                    Console.WriteLine("----------------------------------------------------");
        //                }

        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado = ex.Message;
        //        Console.WriteLine(resultado);
        //    }

        //}



        public static ML.Result GetByIdEFLINQ(int id)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = (from objUsuario in context.Usuarios
                                 join rol in context.Rols on objUsuario.IdRol equals rol.IdRol
                                 where objUsuario.IdUsuario == id
                                 select new
                                 {
                                     IdUsuario = objUsuario.IdUsuario,
                                     UserName = objUsuario.UserName,
                                     NombreUsuario = objUsuario.Nombre,
                                     ApellidoPaterno = objUsuario.ApellidoPaterno,
                                     ApellidoMaterno = objUsuario.ApellidoMaterno,
                                     Email = objUsuario.Email,
                                     Password = objUsuario.Password,
                                     Sexo = objUsuario.Sexo,
                                     Telefono = objUsuario.Telefono,
                                     Celular = objUsuario.Celular,
                                     FechaNacimiento = objUsuario.FechaNacimiento,
                                     CURP = objUsuario.CURP,
                                     IdRol = objUsuario.IdRol,
                                     NombreRol = rol.Nombre
                                 }).FirstOrDefault();

                    

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Nombre = query.NombreUsuario;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.FechaNacimiento = (DateTime)query.FechaNacimiento;
                        usuario.CURP = query.CURP;

                        //instanciamos ROL
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.idRol = (int)query.IdRol;
                        usuario.Rol.Nombre = query.NombreRol;

                        result.Object = usuario;

                        result.Correct = true;
                    } else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "ID no encontrado";
                    }
                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetByIdEF(int? id) //Metodo usando EF y el SP de la BD
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var usuario = context.UsuarioGetById(id.Value).SingleOrDefault();

                    if (usuario != null)
                    {
                        ML.Usuario prop = new ML.Usuario();

                        prop.IdUsuario = usuario.IdUsuario;
                        prop.UserName = usuario.UserName;
                        prop.Nombre = usuario.Nombre;
                        prop.ApellidoPaterno = usuario.ApellidoPaterno;
                        prop.ApellidoMaterno = usuario.ApellidoMaterno;
                        prop.Email = usuario.Email;
                        prop.Password = usuario.Password;
                        prop.Sexo = usuario.Sexo;
                        prop.Telefono = usuario.Telefono;
                        prop.Celular = usuario.Celular;
                        prop.FechaNacimiento = (DateTime)usuario.FechaNacimiento;
                        prop.CURP = usuario.CURP;
                        prop.Imagen = usuario.Imagen;
                        prop.Status = usuario.Status;


                        //instanciamos TODO
                        prop.Rol = new ML.Rol();
                        prop.Direccion = new ML.Direccion();
                        prop.Direccion.Colonia = new ML.Colonia();
                        prop.Direccion.Colonia.Municipio = new ML.Municipio();
                        prop.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        prop.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();


                        prop.Rol.idRol = (int)usuario.idRol;
                        prop.Rol.Nombre = usuario.NombreRol;

                        prop.Direccion.Colonia.Municipio.Estado.Pais.IdPais = usuario.IdPais;
                        prop.Direccion.Colonia.Municipio.Estado.Pais.Nombre = usuario.NombrePais;

                        prop.Direccion.Colonia.Municipio.Estado.IdEstado = usuario.IdEstado;
                        prop.Direccion.Colonia.Municipio.Estado.Nombre = usuario.NombreEstado;

                        prop.Direccion.Colonia.Municipio.IdMunicipio = usuario.IdMunicipio;
                        prop.Direccion.Colonia.Municipio.Nombre = usuario.NombreMunicipio;

                        prop.Direccion.Colonia.IdColonia = usuario.IdColonia;
                        prop.Direccion.Colonia.Nombre = usuario.NombreColonia;
                        prop.Direccion.Colonia.CodigoPostal = usuario.CodigoPostal;

                        prop.Direccion.IdDireccion = usuario.IdDireccion;
                        prop.Direccion.Calle = usuario.Calle;
                        prop.Direccion.NumeroExterior = usuario.NumeroExterior;
                        prop.Direccion.NumeroInterior = usuario.NumeroInterior;

                        result.Object = prop;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El registro no se encontro";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetById(int id) //Metodo usando SP de la BD
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UsuarioGetById";
                    //creamos el objeto cmd para poder indicar que el query es un SP
                    SqlCommand cmd = new SqlCommand();
                    //asignamos la query a tipo texto
                    cmd.CommandText = query;
                    //lo cambiamos a tipo SP
                    cmd.CommandType = CommandType.StoredProcedure;
                    //establecemos la conexion
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@ID", id);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaUsuario = new DataTable();
                    adapter.Fill(tablaUsuario);

                    if (tablaUsuario.Rows.Count > 0)
                    {
                        //Accedemos solo a la primera fila de resultado
                        DataRow row = tablaUsuario.Rows[0];

                        //Sacamos las propiedades
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.id = int.Parse(row[0].ToString());
                        usuario.Nombre = row[1].ToString();
                        usuario.ApellidoPaterno = row[2].ToString();
                        usuario.ApellidoMaterno = row[3].ToString();
                        usuario.FechaNacimiento = DateTime.Parse(row[4].ToString());
                        usuario.Peso = float.Parse(row[5].ToString());
                        usuario.Direccion = row[6].ToString();
                      
                        result.Object = usuario;

                        result.Correct = true;

                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        //public static ML.Result GetById2(int id) //metodo implementando la clase Result de la capa ML
        //{
        //    ML.Result selectID = new ML.Result();
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "SELECT id,Nombre,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,Peso,Direccion FROM Usuario WHERE id=@ID";

        //            //SqlCommand cmd = new SqlCommand(query, context);

        //            SqlCommand cmd = new SqlCommand(query, context);
        //            cmd.Parameters.AddWithValue("@ID", id);

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataTable tablaUsuario = new DataTable();
        //            adapter.Fill(tablaUsuario);

        //            if(tablaUsuario.Rows.Count > 0)
        //            {
        //                //Accedemos solo a la primera fila
        //                DataRow row = tablaUsuario.Rows[0];

        //                //Asignamos valores al objeto ML.Usuario
        //                ML.Usuario usuario = new ML.Usuario();
        //                usuario.id = int.Parse(row[0].ToString());
        //                usuario.Nombre = row[1].ToString();
        //                usuario.ApellidoPaterno = row[2].ToString();
        //                usuario.ApellidoMaterno = row[3].ToString();
        //                usuario.FechaNacimiento = DateTime.Parse(row[4].ToString());
        //                usuario.Peso = float.Parse(row[5].ToString());
        //                usuario.Direccion = row[6].ToString();

        //                selectID.Object = usuario; //Boxing - guardamos usuario de tipo ML.Usuario en selectID.Object de tipo ML.Result

        //                selectID.Correct = true;

        //            } else
        //            {
        //                selectID.Correct = false;
        //                selectID.ErrorMessage = "No se encontro el ID";
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        selectID.Correct = false;
        //        selectID.ErrorMessage = ex.Message;
        //        selectID.Ex = ex;
        //    }

        //    return selectID;
        //}

        //public static void GetById3( int id) //metodo normal, logica
        //{
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "SELECT * FROM Usuario WHERE id=@ID";

        //            SqlCommand cmd = new SqlCommand(query, context);

        //            cmd.Parameters.AddWithValue("@ID", id);

        //            cmd.Connection.Open();

        //            SqlDataReader registro = cmd.ExecuteReader();
        //            if (registro.HasRows)
        //            {
        //                while (registro.Read())
        //                {
        //                    // Mostrar el resultado por pantalla
        //                    Console.WriteLine("ID: " + registro["id"].ToString());
        //                    Console.WriteLine("Nombre: " + registro["Nombre"].ToString());
        //                    Console.WriteLine("Apellido Paterno: " + registro["ApellidoPaterno"].ToString());
        //                    Console.WriteLine("Apellido Materno: " + registro["ApellidoMaterno"].ToString());
        //                    Console.WriteLine("Fecha de Nacimiento: " + registro["FechaNacimiento"].ToString());
        //                    Console.WriteLine("Peso: " + registro["Peso"].ToString());
        //                    Console.WriteLine("Dirección: " + registro["Direccion"].ToString());
        //                    Console.WriteLine("----------------------------------------------------");
        //                }

        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string resultado = ex.Message;
        //        Console.WriteLine(resultado);
        //    }
        //}

        public static ML.Result CambiarStatus (int idUsuario, bool status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new ProgramacionNCapasEntities())
                {
                    var rowsAffetted = context.CambiarStatus(idUsuario, status);

                    if (rowsAffetted > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Login (string email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.UsuarioLogin(email).FirstOrDefault();
                    if(query != null) { 
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        result.Object = usuario;

                        result.Correct =true;
                    } else { 
                        result.Correct = false;
                        result.ErrorMessage = "El usuario o la contraseña estan mal";
                    }
                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public static ML.Result LeerExcel(string connectionString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [Sheet1$]"; //Sheet1 es el nombre de la hoja
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();

                        da.Fill(tableUsuario); //llenamos nuestra tabla con el DataReader

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.UserName = row[0].ToString();
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Password = row[5].ToString();
                                usuario.Sexo = row[6].ToString();
                                usuario.Telefono = row[7].ToString();
                                usuario.Celular = row[8].ToString();

                                DateTime fechaNacimiento;
                                if (DateTime.TryParse(row[9].ToString(), out fechaNacimiento))
                                {
                                    usuario.FechaNacimiento = fechaNacimiento;
                                }
                                else
                                {
                                    usuario.FechaNacimiento = DateTime.MinValue;
                                }

                                //usuario.FechaNacimiento = DateTime.Parse(row[9].ToString());
                                usuario.CURP = row[10].ToString();

                                usuario.Rol = new ML.Rol();
                                usuario.Rol.idRol = int.Parse(row[11].ToString());

                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Colonia = new ML.Colonia();

                                usuario.Direccion.Colonia.IdColonia = int.Parse(row[12].ToString());
                                usuario.Direccion.Calle = row[13].ToString();
                                usuario.Direccion.NumeroInterior = row[14].ToString();
                                usuario.Direccion.NumeroExterior = row[15].ToString();

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;

                        }

                        result.Object = tableUsuario;

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        public static ML.Result ValidarExcel(List<object> usuarios)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>(); //almacena los registros incompletos
                int i = 1; //guarda el numero de la fila
                foreach (ML.Usuario usuario in usuarios)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if(usuario.UserName == "")
                    {
                        error.Mensaje += "Ingresar el Nombre de Usuario ";
                    }
                    if (usuario.Nombre == "")
                    {
                        error.Mensaje += "Ingresar el nombre  ";
                    }
                    if (usuario.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido paterno  ";
                    }
                    if(usuario.Email == "")
                    {
                        error.Mensaje += "Ingresar el Email";
                    }
                    if(usuario.Password == "")
                    {
                        error.Mensaje += "Ingresar el Password ";
                    }
                    if(usuario.Sexo == "")
                    {
                        error.Mensaje += "Ingresar el Sexo ";
                    }
                    //if(usuario.Telefono == "")
                    //{
                    //    error.Mensaje += "Ingresar el Telefono";
                    //}
                    if (usuario.FechaNacimiento.Equals(DateTime.MinValue))
                    {
                        error.Mensaje += "Ingresar una Fecha de Nacimiento";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        public static (bool, string, Exception) CambioStatus (int idUsuario, bool status)
        {
            try
            {
                using(DL_EF.ProgramacionNCapasEntities context = new DL_EF.ProgramacionNCapasEntities())
                {
                    var query = context.CambiarStatus(idUsuario, status);
                    if(query > 0)
                    {
                        return (true, null, null);
                    } else
                    {
                        return (false, null, null);
                    }
                }

            } catch(Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }


    }
}
