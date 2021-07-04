using System;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using WebServiceBase.Sistema;

namespace WebServiceBase.Modelo
{
    public class FuncionesModel : Conexion
    {

        public bool ActualizarObjeto(MENSAJES item)
        {
            bool error = false;
            SqlConnection Conn = ObtenerConexionBD_OVSAP();
            SqlCommand cmd = Conn.CreateCommand();
            SqlTransaction Transaction = Conn.BeginTransaction();
            cmd.Transaction = Transaction;

            try
            {
                string query = " UPDATE tabla_test SET " +
                                " ,ACCION = @ACCION  " +
                                " ,ID = @ID " +
                                " ,RESPUESTA = @RESPUESTA " +
                                " ,TIPO = @TIPO  " +
                                " ,MENSAJE = @MENSAJE  " +
                               " WHERE " +
                               " ID = @ID ";
                cmd.CommandText = query;

                cmd.Parameters.AddWithValue("@ACCION", item.ACCION);
                cmd.Parameters.AddWithValue("@ID", item.ID);
                cmd.Parameters.AddWithValue("@RESPUESTA", item.RESPUESTA);
                cmd.Parameters.AddWithValue("@TIPO", item.TIPO);
                cmd.Parameters.AddWithValue("@MENSAJE", item.MENSAJE);
                cmd.Parameters.AddWithValue("@ID", item.ID);

                if (configInicial.MODO_EJECUCION == "DEBUG")
                {
                    JavaScriptSerializer jsonserializer = new JavaScriptSerializer();
                    string json = jsonserializer.Serialize(item);

                    EscribirLogEvento.escribirLog("FuncionesModel", "ActualizarObjeto", "objeto item : " + json, "", "");
                }

                if (cmd.ExecuteNonQuery() == 0)
                {
                    error = true;
                }
                Transaction.Commit();
            }
            catch (SqlException e)
            {
                ControlExcepciones ex = new ControlExcepciones();
                ex.SQLException(e, "FuncionesModel.cs", "ActualizarObjeto()");
                try
                {
                    Transaction.Rollback();
                }
                catch (SqlException e2)
                {
                    ex.SQLException(e2, "FuncionesModel.cs", "Intentando hacer Rollback en la transacción.");
                }
                catch (Exception e2)
                {
                    ex.Exception("FuncionesModel.cs", "ActualizarObjeto()", e2);
                }
            }
            catch (Exception e)
            {
                ControlExcepciones ex = new ControlExcepciones();
                ex.Exception("FuncionesModel.cs", "ActualizarObjeto()", e);
            }
            finally
            {
                Conn.Close();
            }
            return error;
        }

        public bool eliminarObjeto(string id)
        {
            bool error = false;
            SqlConnection Conn = ObtenerConexionBD_OVSAP();
            SqlCommand cmd = Conn.CreateCommand();
            SqlTransaction Transaction = Conn.BeginTransaction();
            cmd.Transaction = Transaction;

            try
            {
                string query = " DELETE FROM tabla_test " +
                               " WHERE " +
                               " id = @id ";
                cmd.CommandText = query;

                cmd.Parameters.AddWithValue("@id", id);


                if (cmd.ExecuteNonQuery() == 0)
                {
                    error = true;
                }
                Transaction.Commit();
            }
            catch (SqlException e)
            {
                ControlExcepciones ex = new ControlExcepciones();
                ex.SQLException(e, "FuncionesModel.cs", "eliminarObjeto()");
                try
                {
                    Transaction.Rollback();
                }
                catch (SqlException e2)
                {
                    ex.SQLException(e2, "FuncionesModel.cs", "Intentando hacer Rollback en la transacción.");
                }
                catch (Exception e2)
                {
                    ex.Exception("FuncionesModel.cs", "eliminarObjeto()", e2);
                }
            }
            catch (Exception e)
            {
                ControlExcepciones ex = new ControlExcepciones();
                ex.Exception("FuncionesModel.cs", "eliminarObjeto()", e);
            }
            finally
            {
                Conn.Close();
            }
            return error;
        }
        public bool guardarItems(MENSAJES item)
        {
            bool error = false;
            SqlConnection Conn = ObtenerConexionBD_OVSAP();
            SqlCommand cmd = Conn.CreateCommand();
            SqlTransaction Transaction = Conn.BeginTransaction();
            cmd.Transaction = Transaction;

            try
            {
                string query = " INSERT INTO tabla_test " +
                        " ( " +
                        " ACCION " +
                        " ,ID " +
                        " ,RESPUESTA " +
                        " ,TIPO" +
                        " ,MENSAJE " +
                        " ) " +
                        " VALUES( " +
                        " @ACCION " +
                        " ,@ID " +
                        " ,@RESPUESTA " +
                        " ,@TIPO " +
                        " ,@MENSAJE " +
                        ")";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@ACCION", item.ACCION);
                cmd.Parameters.AddWithValue("@ID", item.ID);
                cmd.Parameters.AddWithValue("@RESPUESTA", item.RESPUESTA);
                cmd.Parameters.AddWithValue("@TIPO", item.TIPO);
                cmd.Parameters.AddWithValue("@MENSAJE", item.MENSAJE);

                if (cmd.ExecuteNonQuery() == 0)
                {
                    error = true;
                }
                Transaction.Commit();
            }
            catch (SqlException e)
            {
                ControlExcepciones ex = new ControlExcepciones();
                ex.SQLException(e, "existePedido.cs", "guardarItems()");
                try
                {
                    Transaction.Rollback();
                }
                catch (SqlException e2)
                {
                    ex.SQLException(e2, "existePedido.cs", "Intentando hacer Rollback en la transacción.");
                }
                catch (Exception e2)
                {
                    ex.Exception("existePedido.cs", "guardarItems()", e2);
                }
            }
            catch (Exception e)
            {
                ControlExcepciones ex = new ControlExcepciones();
                ex.Exception("existePedido.cs", "guardarItems()", e);
            }
            finally
            {
                Conn.Close();
            }

            JavaScriptSerializer jsonserializer = new JavaScriptSerializer();
            string json = jsonserializer.Serialize(item);
            if (configInicial.MODO_EJECUCION == "DEBUG")
            {
                EscribirLogEvento.escribirLog("guardarItems()", "Comprobar Objeto Crear o editar", "Comprobar que existe el objeto :", json, "");
            }

            return error;
        }
      
    }
}
