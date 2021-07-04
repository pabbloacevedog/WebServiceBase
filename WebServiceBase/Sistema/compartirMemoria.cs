using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace WebServiceBase.Sistema
{
    public class compartirMemoria
    {
        public static bool bandera = false;
        public static char[] datosFecha = new char[19];
        public static string conexion = string.Empty;
        public static bool creado = false;
        public static string nombreMutex = configInicial.NOMBRE_MUTEX;
        public static string mutexId = string.Format("Global\\{{{0}}}", nombreMutex);
        private static Object thisLock = new Object();
        public static string archivo = configInicial.ARCHIVO_MEMORIA;
        public static Mutex mutex;

        public static void publicar(int estado)
        {
            int posicionEstado = Convert.ToInt32(configInicial.POSICION_ESTADO);
            int posicionFecha = Convert.ToInt32(configInicial.POSICION_FECHA);
            int tamañoBufer = 1024;
            string nombreBuffer = configInicial.NOMBRE_BUFFER;
            bool mutexCreated;
            try
            {
                using (mutex = Mutex.OpenExisting(mutexId))
                {
                    try
                    {
                        using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting(nombreBuffer))
                        {
                            try
                            {
                                memoria(mmf, posicionEstado, estado);
                            }
                            catch (Exception e2)
                            {
                                ControlExcepciones ex = new ControlExcepciones();
                                ex.Exception("compartirMemoria", "compartirMemoria", e2);
                            }
                            mmf.Dispose();
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        try
                        {
                            using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(@"" + archivo, FileMode.Open, nombreBuffer, tamañoBufer))
                            {
                                try
                                {
                                    memoria(mmf, posicionEstado, estado);
                                }
                                catch (Exception e2)
                                {
                                    ControlExcepciones ex = new ControlExcepciones();
                                    ex.Exception("compartirMemoria", "compartirMemoria", e2);
                                }
                                mmf.Dispose();
                            }
                        }
                        catch (FileNotFoundException)
                        {
                            EscribirLogEvento.escribirLog("leerMemoria     ", "compartirMemoria()", "El archivo (" + archivo + ") no se encuentra en la ruta indicada, por favor revise la carpeta MMF", "", "");
                        }
                        catch (IOException)
                        {
                        }
                        catch (Exception e2)
                        {
                            ControlExcepciones ex = new ControlExcepciones();
                            ex.Exception("compartirMemoria", "compartirMemoria", e2);
                        }
                    }
                }
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                using (mutex = new Mutex(true, mutexId, out mutexCreated))
                {
                    try
                    {
                        using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting(nombreBuffer))
                        {
                            try
                            {
                                memoria(mmf, posicionEstado, estado);
                            }
                            catch (Exception e2)
                            {
                                ControlExcepciones ex = new ControlExcepciones();
                                ex.Exception("compartirMemoria", "compartirMemoria", e2);
                            }
                            mmf.Dispose();
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        try
                        {
                            using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(@"" + archivo, FileMode.Open, nombreBuffer, tamañoBufer))
                            {
                                try
                                {
                                    memoria(mmf, posicionEstado, estado);
                                }
                                catch (Exception e2)
                                {
                                    ControlExcepciones ex = new ControlExcepciones();
                                    ex.Exception("compartirMemoria", "compartirMemoria", e2);
                                }
                                mmf.Dispose();
                            }
                        }
                        catch (FileNotFoundException)
                        {
                            EscribirLogEvento.escribirLog("leerMemoria     ", "compartirMemoria()", "El archivo (" + archivo + ") no se encuentra en la ruta indicada, por favor revise la caperta MMF", "", "");
                        }

                        catch (Exception e2)
                        {
                            ControlExcepciones ex = new ControlExcepciones();
                            ex.Exception("compartirMemoria", "compartirMemoria", e2);
                        }
                    }
                }
            }
            catch (Exception e2)
            {
                ControlExcepciones ex = new ControlExcepciones();
                ex.Exception("compartirMemoria", "compartirMemoria", e2);
            }
        }
        public static void memoria(MemoryMappedFile mmf, int posicionEstado, Int64 estado)
        {
            lock (thisLock)
            {
                using (MemoryMappedViewAccessor ACCESOR = mmf.CreateViewAccessor())
                {
                    DateTime localDate = DateTime.Now;
                    string fechaS = localDate.ToString();
                    char[] fecha = fechaS.ToCharArray();
                    int largoFecha = fecha.Length;
                    string arregloFecha = new string(fecha);
                    // escribe un valor del estado en la posición 0
                    ACCESOR.Write(posicionEstado, estado);
                    int posicionFecha = Convert.ToInt32(configInicial.POSICION_FECHA);
                    ACCESOR.WriteArray(posicionFecha, fecha, 0, largoFecha);
                }
            }
        }
    }
}