using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

using System.Data.SqlClient;

namespace WebServiceBase.Sistema
{
    class ControlExcepciones
    {
        // CONTROL DE ESCEPCIONES DE SOCKETS
        public void SocketException(String modulo, String accion, SocketException e)
        {
            String error = "";
            switch (e.SocketErrorCode)
            {
                case SocketError.AccessDenied:
                    error = "Se intentó obtener acceso a un Socket de una manera prohibida por sus permisos de acceso.";
                    break;
                case SocketError.AddressAlreadyInUse:
                    error = "Normalmente se permite un solo uso de una dirección.";
                    break;
                case SocketError.AddressFamilyNotSupported:
                    error = "No admite la familia de direcciones especificada.Se devuelve este error si se especificó la familia de direcciones IPv6 y la pila del IPv6 no está instalada en el equipo local.Se devuelve este error si se especificó la familia de direcciones IPv4 y la pila del IPv4 no está instalada en el equipo local.";
                    break;
                case SocketError.AddressNotAvailable:
                    error = "La dirección IP seleccionada no es válida en este contexto.";
                    break;
                case SocketError.AlreadyInProgress:
                    error = "El Socket de no bloqueo ya tiene una operación en curso.";
                    break;
                case SocketError.ConnectionAborted:
                    error = "NET Framework o el proveedor de sockets subyacentes anuló la conexión.";
                    break;
                case SocketError.ConnectionRefused:
                    error = "El host remoto rechaza activamente una conexión.";
                    break;
                case SocketError.ConnectionReset:
                    error = "El host remoto restableció la conexión.";
                    break;
                case SocketError.DestinationAddressRequired:
                    error = "Se ha omitido una dirección necesaria de una operación en un Socket.";
                    break;
                case SocketError.Disconnecting:
                    error = "Se está realizando correctamente una desconexión.";
                    break;
                case SocketError.Fault:
                    error = "El proveedor de sockets subyacentes detectó una dirección de puntero no válida.";
                    break;
                case SocketError.HostDown:
                    error = "Se ha generado un error en la operación porque el host remoto está inactivo.";
                    break;
                case SocketError.HostNotFound:
                    error = "Se desconoce el host.El nombre no es un nombre de host o alias oficial.";
                    break;
                case SocketError.HostUnreachable:
                    error = "No hay ninguna ruta de red al host especificado.";
                    break;
                case SocketError.IOPending:
                    error = "La aplicación ha iniciado una operación superpuesta que no se puede finalizar inmediatamente.";
                    break;
                case SocketError.InProgress:
                    error = "Hay una operación de bloqueo en curso.";
                    break;
                case SocketError.Interrupted:
                    error = "Se canceló una llamada Socket de bloqueo.";
                    break;
                case SocketError.InvalidArgument:
                    error = "Se ha proporcionado un argumento no válido a un miembro de Socket.";
                    break;
                case SocketError.IsConnected:
                    error = "El Socket ya está conectado.";
                    break;
                case SocketError.MessageSize:
                    error = "El datagrama es demasiado largo.";
                    break;
                case SocketError.NetworkDown:
                    error = "La red no está disponible.";
                    break;
                case SocketError.NetworkReset:
                    error = "La aplicación intentó establecer KeepAlive en una conexión cuyo tiempo de espera ya está agotado.";
                    break;
                case SocketError.NetworkUnreachable:
                    error = "No existe ninguna ruta al host remoto.";
                    break;
                case SocketError.NoBufferSpaceAvailable:
                    error = "No hay espacio en búfer disponible para una operación de Socket.";
                    break;
                case SocketError.NoData:
                    error = "No se encontró el nombre o la dirección IP solicitada en el servidor de nombres.";
                    break;
                case SocketError.NoRecovery:
                    error = "El error es irrecuperable o no se encuentra la base de datos solicitada.";
                    break;
                case SocketError.NotConnected:
                    error = "La aplicación intentó enviar o recibir datos y el Socket no está conectado.";
                    break;
                case SocketError.NotInitialized:
                    error = "No se ha inicializado el proveedor de sockets subyacentes.";
                    break;
                case SocketError.NotSocket:
                    error = "Se intentó realizar una operación de Socket en algo que no es un socket.";
                    break;
                case SocketError.OperationAborted:
                    error = "La operación superpuesta se anuló debido al cierre del Socket.";
                    break;
                case SocketError.OperationNotSupported:
                    error = "La familia de protocolos no admite la familia de direcciones.";
                    break;
                case SocketError.ProcessLimit:
                    error = "Demasiados procesos están utilizando el proveedor de sockets subyacentes.";
                    break;
                case SocketError.ProtocolFamilyNotSupported:
                    error = "La familia de protocolos no está implementada o no está configurada.";
                    break;
                case SocketError.ProtocolNotSupported:
                    error = "El protocolo no está implementado o no está configurado.";
                    break;
                case SocketError.ProtocolOption:
                    error = "Se ha utilizado una opción o un nivel desconocido, no válido o incompatible con un Socket.";
                    break;
                case SocketError.ProtocolType:
                    error = "El tipo de protocolo es incorrecto para este Socket.";
                    break;
                case SocketError.Shutdown:
                    error = "Se denegó una solicitud de envío o recepción de datos porque ya se ha cerrado el Socket.";
                    break;
                case SocketError.SocketError:
                    error = "Se ha producido un error de Socket no especificado.";
                    break;
                case SocketError.SocketNotSupported:
                    error = "Esta familia de direcciones no es compatible con el tipo de socket especificado.";
                    break;
                case SocketError.Success:
                    error = "La operación de Socket se ha realizado correctamente.";
                    break;
                case SocketError.SystemNotReady:
                    error = "El subsistema de red no está disponible.";
                    break;
                case SocketError.TimedOut:
                    error = "El intento de conexión ha sobrepasado el tiempo de espera o el host conectado no ha respondido.";
                    break;
                case SocketError.TooManyOpenSockets:
                    error = "Hay demasiados sockets abiertos en el proveedor de sockets subyacentes.";
                    break;
                case SocketError.TryAgain:
                    error = "No se pudo resolver el nombre del host.Vuelva a intentarlo más tarde.";
                    break;
                case SocketError.TypeNotFound:
                    error = "No se encontró la clase especificada.";
                    break;
                case SocketError.VersionNotSupported:
                    error = "La versión del proveedor de sockets subyacentes está fuera del intervalo.";
                    break;
                case SocketError.WouldBlock:
                    error = "No se puede finalizar inmediatamente una operación en un socket de no bloqueo.";
                    break;
                default:
                    error = "OTRO ERROR NO ESPECIFICADO";
                    break;
            }
            String tipo = e.GetType().ToString();
            String baseException = e.GetBaseException().ToString();
            EscribirLogEvento.escribirLog(modulo, accion, "EXCEPTION: TIPO: " + tipo + ", MENSAJE: " + error + ", BaseException: " + baseException, "", "");
        }
        

        public void InvalidOperationException(String modulo, String accion, InvalidOperationException e)
        {
            String error = e.Message.ToString();
            EscribirLogEvento.escribirLog(modulo, accion, error, "", "");
        }
        public void IOException(String modulo, String accion, IOException e)
        {
            String error = e.Message.ToString();
            EscribirLogEvento.escribirLog(modulo, accion, error, "", "");
        }
        public void SQLException(SqlException e, String modulo, String accion)
        {
            String mensaje = e.Message.ToString();
            EscribirLogEvento.escribirLog(modulo, accion, "WARNING: MENSAJE: " + mensaje, "", "");
            //String tipo = e.GetTIPO().ToString();
            //String baseException = e.GetBaseException().ToString();
            //EscribirLogEvento.escribirLog(modulo, accion, "EXCEPTION: TIPO: " + tipo + ", MENSAJE: " + messaje + ", BaseException: " + baseException, "", "");
        }

        public void Exception(String modulo, String accion, Exception e)
        {
            //String mensaje = e.MENSAJE.ToString();
            //EscribirLogEvento.escribirLog(modulo, accion, "WARNING: MENSAJE: " + mensaje, "", "");
            String mensaje = e.Message.ToString();
            String tipo = e.GetType().ToString();
            String baseException = e.GetBaseException().ToString();
            EscribirLogEvento.escribirLog(modulo, accion, "EXCEPTION: TIPO: " + tipo + ", MENSAJE: " + mensaje + ", BaseException: " + baseException, "", "");
        }      
    }
}
