using WebServiceBase.Sistema;
using WebServiceBase.Modelo;
using System;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace WebServiceBase
{
    /// <summary>
    /// Descripción breve de WebServiceBase
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("WebServiceBase", "1.0.0.0")]
    [WebServiceAttribute(Namespace = "http://TESTWebServiceBase")]
    [WebServiceBindingAttribute(Name = "WebServiceBase", Namespace = "http://TESTWebServiceBase")]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceBase : System.Web.Services.WebService
    {

        [WebMethod]
        public Return actualizar_objeto(ActualizarObjeto obj)
        {
            Return r = new Return();
            JavaScriptSerializer jsonserializer = new JavaScriptSerializer();
            try
            {
                string json = jsonserializer.Serialize(obj);
                if (!crearConfigInicial())
                {
                    r.MENSAJE += "Error Interno de la aplicación";
                    r.TIPO = "E";
                    r.RESPUESTA = "500";
                    r.ID = "";
                }
                else
                {
                    EscribirLogEvento.escribirLog("WebServiceBase.cs", "actualizar_objeto", "Se recibe objeto", json, "");
                    r = validarDatos(obj);
                    if (r.TIPO != "E")
                    {
                        string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        r = ProcesarObjeto(obj, fecha);
                    }
                }
            }
            catch (Exception e)
            {

                r.MENSAJE += "Error Interno de la aplicación";
                r.TIPO = "E";
                r.RESPUESTA = "500";
                r.ID = "";
                ControlExcepciones ex = new ControlExcepciones();
                ex.Exception("WebServiceBase.cs", "actualizar_objeto()", e);
            }
            
            
                                                     
            string json_r = jsonserializer.Serialize(r);
            EscribirLogEvento.escribirLog("WebServiceBase.cs", "actualizar_objeto", "respuesta: ", json_r, "");
            return r;
        }
        private Return validarDatos(ActualizarObjeto obj)
        {
            Return r = new Return();
            string ID_antiguo="",ID_nuevo="";      
            if (string.IsNullOrWhiteSpace(obj.ID))
            {
                if (obj.MENSAJES.Length > 0)
                {
                    r.ID = obj.MENSAJES[0].ID;                   
                }
                else
                {                    
                    r.ID = "";
                }
                r.ACCION = 
                r.MENSAJE += "Verificar el valor del ID valor actual ("+ obj.ID + ") |";
                r.TIPO = "E";
                r.RESPUESTA = "500";                                                
            }
            else
            {
                if (obj.MENSAJES.Length == 0)
                {
                    r.MENSAJE += " No Existen Mensajes |";
                    r.ID = "";
                }
                else
                {
                    for (int j = 0; j < obj.MENSAJES.Length; j++)
                    {
                        ID_antiguo = obj.MENSAJES[j].ID;
                        try
                        {
                            if (string.IsNullOrWhiteSpace(Regex.Replace(obj.MENSAJES[j].ID, @"[^0-9A-Za-z]", "", RegexOptions.None)))
                            {
                                r.MENSAJE += "Verificar el valor del TAG ID, valor actual (" + obj.MENSAJES[j].ID + ") del arreglo N° (" + Convert.ToString(j) + ") |";
                                r.TIPO = "E";
                                r.RESPUESTA = "500";
                                r.ID = ID_nuevo;
                            }
                            else
                            {
                                if (ID_nuevo == "")
                                {
                                    ID_nuevo = obj.MENSAJES[j].ID;
                                }
                                else
                                {
                                    if (ID_nuevo == ID_antiguo)
                                    {
                                        ID_nuevo = ID_antiguo;
                                    }
                                    else
                                    {
                                        r.MENSAJE += "Los valores del TAG ID no son todos iguales, debe verificar valores, valor último ID (" + ID_nuevo + ") y el anterior (" + ID_antiguo + ")|";
                                        r.TIPO = "E";
                                        r.RESPUESTA = "500";
                                        r.ID = ID_nuevo;
                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            r.MENSAJE += "Verificar el valor del TAG ID, valor actual (" + obj.MENSAJES[j].ID + ") del arreglo N° (" + Convert.ToString(j) + ") |";
                            r.TIPO = "E";
                            r.RESPUESTA = "500";
                            r.ID = ID_nuevo;
                            break;
                        }
                        
                    if (!string.IsNullOrWhiteSpace(obj.MENSAJES[j].RESPUESTA))
                    {
                        try
                        {
                            int RESPUESTA = Convert.ToInt32(obj.MENSAJES[j].RESPUESTA);
                        }
                        catch (Exception)
                        {
                            r.MENSAJE += "verificar el valor del TAG  RESPUESTA del arreglo N° (" + Convert.ToString(j) + ")  valor actual (" + obj.MENSAJES[j].RESPUESTA + ")|, debe ser número |";
                            r.TIPO = "E";
                            r.RESPUESTA = "500";
                            r.ID = ID_nuevo;
                        }                                
                    }
                        try
                        {
                            if (string.IsNullOrWhiteSpace(obj.MENSAJES[j].TIPO))
                            {
                                r.MENSAJE += "verificar el valor del TAG TIPO del arreglo N° (" + Convert.ToString(j) + ") valor actual (" + obj.MENSAJES[j].TIPO + ")|";
                                r.TIPO = "E";
                                r.RESPUESTA = "500";
                                r.ID = ID_nuevo;
                            }
                        }
                        catch (Exception)
                        {
                            r.MENSAJE += "verificar el valor del TAG TIPO del arreglo N° (" + Convert.ToString(j) + ") valor actual (" + obj.MENSAJES[j].TIPO + ")|";
                            r.TIPO = "E";
                            r.RESPUESTA = "500";
                            r.ID = ID_nuevo;
                        }
                        if (string.IsNullOrWhiteSpace(obj.MENSAJES[j].ID))
                        {
                            obj.MENSAJES[j].ID = "";
                        }                                   
                    }
                }
            }
            
            return r;
        }
        private bool crearConfigInicial()
        {
            bool existeSetting = false;
            string path_install = "";
            try
            {
                path_install = HttpContext.Current.Server.MapPath("~");
                path_install = Path.GetDirectoryName(path_install);
                path_install = Path.GetDirectoryName(path_install);
                path_install = Path.GetDirectoryName(path_install);

                if (!File.Exists(@"" + path_install + "\\CONFIG\\WebServiceBase.ini"))
                {
                    //pasar la ruta y la extensión del log a mano
                    configInicial.PATH_LOG = path_install + "\\LOG\\WebServiceBase\\LogACCION-WebServiceBase-";
                    configInicial.EXT_LOG = ".txt";
                    EscribirLogEvento.escribirLog("Server INI", "", "No se encuentra el archivo INI en la ruta especificada", "", "");
                }
                else
                {
                    IniFile ini = new IniFile(@"" + path_install + "\\CONFIG\\CONFIG_WebServiceBase.ini");

                    configInicial.PATH_LOG = ini.Read("PATH_LOG");
                    configInicial.EXT_LOG = ini.Read("EXT_PATH_LOG");

                    configInicial.NOMBRE_SERVICIO = ini.Read("NOMBRE_SERVICIO");

                    configInicial.SERVER_BD = ini.Read("SERVER_OVSAP");
                    configInicial.NOMBRE_BD = ini.Read("BD_OVSAP");
                    configInicial.USER_BD = ini.Read("USER_OVSAP");
                    configInicial.PASS_BD = ini.Read("PASS_OVSAP");

                    configInicial.NOMBRE_MUTEX = ini.Read("NOMBRE_MUTEX");
                    configInicial.ARCHIVO_MEMORIA = ini.Read("ARCHIVO_MEMORIA");
                    configInicial.POSICION_ESTADO = ini.Read("POSICION_ESTADO");
                    configInicial.POSICION_FECHA = ini.Read("POSICION_FECHA");
                    configInicial.NOMBRE_BUFFER = ini.Read("NOMBRE_BUFFER");

                    configInicial.PATH_LOG_SQL = ini.Read("PATH_LOG_SQL");
                    configInicial.EXT_PATH_LOG_SQL = ini.Read("EXT_PATH_LOG_SQL");
                    configInicial.LOG_SQL = ini.Read("LOG_SQL");

                    existeSetting = true;
                }
            }
            catch (Exception e2)
            {
                ControlExcepciones ex = new ControlExcepciones();
                ex.Exception("WebServiceBase", "ReadIniFile", e2);
            }
            return existeSetting;
        }

        private Return ProcesarObjeto(ActualizarObjeto obj,string fecha)
        {
            Return r = new Return();

            JavaScriptSerializer jsonserializer = new JavaScriptSerializer();
            FuncionesModel modelo = new FuncionesModel();
            try
            {
                for (int j = 0; j < obj.MENSAJES.Length; j++)
                {
                    
                    if (obj.MENSAJES[j].ACCION == "U" || obj.MENSAJES[j].ACCION == "X" || obj.MENSAJES[j].ACCION == "E" || obj.MENSAJES[j].ACCION == "C")
                    {
                        modelo.eliminarObjeto(obj.MENSAJES[j].ID);
                        string item_json = jsonserializer.Serialize(obj);
                        EscribirLogEvento.escribirLog("WebService", "validarEnviarPedido", " Se ELIMINA el OBJETO : " + item_json, "", "");

                    }
                    else if (obj.MENSAJES[j].ACCION == "NEWP")
                    {
                        modelo.guardarItems(obj.MENSAJES[j]);
                        //actualizar datos respuesta
                        if (configInicial.MODO_EJECUCION == "DEBUG")
                        {
                            string item_json = jsonserializer.Serialize(obj);
                            EscribirLogEvento.escribirLog("WebService ", "validarEnviarPedido", " se CREA el OBJETO: " + item_json, "", "");
                        }

                    }
                    else if (obj.MENSAJES[j].ACCION == "UPDT")
                    {
                        modelo.ActualizarObjeto(obj.MENSAJES[j]);
                        //actualizar datos respuesta
                        string item_json = jsonserializer.Serialize(obj);
                        EscribirLogEvento.escribirLog("WebService ", "validarEnviarPedido", " Se actualizan el OBJETO: " + item_json, "", "");

                    }
                }
            }
            catch (Exception e)
            {
                EscribirLogEvento.escribirLog("WebService ", "validarEnviarPedido", "Error al Procesar : " + e, "", "");
            }
            return r;
        }
    }
}
