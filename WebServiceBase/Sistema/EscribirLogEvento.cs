using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Web.Script.Serialization;


namespace WebServiceBase.Sistema
{
    class EscribirLogEvento
    {
        public static Mutex mut = new Mutex();
        public static void escribirLog(String modulo, String accion, String mensaje, String json, String destinoLog)
        {
            if (mut.WaitOne())
            {               
                try
                {
                    string unidadFuente = ConfigurationManager.AppSettings["unidadFuente"].ToString();

                    if (!Directory.Exists(@"" + unidadFuente + ":\\TCP"))
                    {
                        Directory.CreateDirectory(@"" + unidadFuente + ":\\TCP");
                    }
                    else if(!Directory.Exists(@"" + unidadFuente + ":\\TCP\\WEBSERVICES"))
                    {
                        Directory.CreateDirectory(@"" + unidadFuente + ":\\TCP\\WEBSERVICES");
                    }
                    else if (!Directory.Exists(@"" + unidadFuente + ":\\TCP\\WEBSERVICES\\LOG"))
                    {
                        Directory.CreateDirectory(@"" + unidadFuente + ":\\TCP\\WEBSERVICES\\LOG");
                    }
                    else if (!Directory.Exists(@"" + unidadFuente + ":\\TCP\\WEBSERVICES\\LOG\\WebServiceBase"))
                    {
                        Directory.CreateDirectory(@"" + unidadFuente + ":\\TCP\\WEBSERVICES\\LOG\\WebServiceBase");
                    }

                    String ext = String.Empty;
                    String rutaLog = String.Empty;
                    String pathLog = String.Empty;
                    if (json == "")
                    {
                        json = "[{}]";
                    }
                    String servicio = configInicial.NOMBRE_SERVICIO;

                    switch (destinoLog)
                    {
                        case "SQL":
                            ext = configInicial.EXT_PATH_LOG_SQL;
                            rutaLog = configInicial.PATH_LOG_SQL + "_ROLLOS_";
                            pathLog = rutaLog + (DateTime.Today).ToString("d").Replace("/", "-") + ext;
                            break;
                        default:
                            ext = configInicial.EXT_LOG;
                            rutaLog = configInicial.PATH_LOG;
                            pathLog = rutaLog + (DateTime.Today).ToString("d").Replace("/", "-") + ext;
                            break;
                    }


                    // This text is added only once to the file.
                    if (!File.Exists(pathLog))
                    {
                        // Create a file to write to.
                        using (StreamWriter sw = File.CreateText(pathLog))
                        {
                            switch (destinoLog)
                            {
                                case "HB":
                                    sw.WriteLine("FECHA : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ";             MENSAJE : " + mensaje);
                                    sw.Close();
                                    break;
                                case "SQL_ROLLOS":
                                    sw.WriteLine("FECHA : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "; SERVICIO : " + servicio + "; MODULO : " + modulo + "; ACCION : " + accion + "; CONSULTA : " + mensaje);
                                    sw.Close();
                                    break;
                                case "SQL_PULPAS":
                                    sw.WriteLine("FECHA : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "; SERVICIO : " + servicio + "; MODULO : " + modulo + "; ACCION : " + accion + "; CONSULTA : " + mensaje);
                                    sw.Close();
                                    break;
                                case "SQL_PILAS":
                                    sw.WriteLine("FECHA : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "; SERVICIO : " + servicio + "; MODULO : " + modulo + "; ACCION : " + accion + "; CONSULTA : " + mensaje);
                                    sw.Close();
                                    break;
                                default:
                                    sw.WriteLine("FECHA : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "; SERVICIO : " + servicio + "; MODULO : " + modulo + "; ACCION : " + accion + "; MENSAJE : " + mensaje + "; JSON : " + json);
                                    sw.Close();
                                    break;
                            }
                        }
                    }
                    else
                    {
                        // This text is always added, making the file longer over time
                        // if it is not deleted.
                        using (StreamWriter sw = File.AppendText(pathLog))
                        {
                            switch (destinoLog)
                            {
                                case "HB":
                                    sw.WriteLine("FECHA : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ";             MENSAJE : " + mensaje);
                                    sw.Close();
                                    break;
                                case "SQL":
                                    sw.WriteLine("FECHA : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "; SERVICIO : " + servicio + "; MODULO : " + modulo + "; ACCION : " + accion + "; CONSULTA : " + mensaje);
                                    sw.Close();
                                    break;
                                default:
                                    sw.WriteLine("FECHA : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "; SERVICIO : " + servicio + "; MODULO : " + modulo + "; ACCION : " + accion + "; MENSAJE : " + mensaje + "; JSON : " + json);
                                    sw.Close();
                                    break;
                            }
                        }
                    }
                }
                catch (IOException e)
                {
                    ControlExcepciones obj = new ControlExcepciones();
                    obj.IOException("EscribirLogEvento", "Escribiendo LOG", e);
                }
                catch (Exception e)
                {
                    ControlExcepciones ex = new ControlExcepciones();
                    ex.Exception("EscribirLogEvento", "Escribiendo LOG", e);
                }
                mut.ReleaseMutex();
            }
        }
        public static String serializar(object obj)
        {
            String json = "";
            List<object> Lista = new List<object>();

            try
            {
                Lista.Add(obj);

                JavaScriptSerializer jsonserializer = new JavaScriptSerializer();
                json = jsonserializer.Serialize(Lista);
            }
            catch (Exception e)
            {
                ControlExcepciones ex = new ControlExcepciones();
                ex.Exception("EscribirLogEvento", "serializar()", e);
            }

            return json;
        }        
    }
}
