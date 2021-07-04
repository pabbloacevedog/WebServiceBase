public class ActualizarObjeto
{
    public string ID { get; set; }
    public MENSAJES[] MENSAJES;
}
public class MENSAJES
{
    public string ACCION { get; set; }
    public string RESPUESTA { get; set; }
    public string ID { get; set; }
    public string TIPO { get; set; }
    public string MENSAJE { get; set; }

}

public class Return
{
    public string ACCION { get; set; }
    public string RESPUESTA { get; set; }
    public string ID { get; set; }
    public string TIPO { get; set; }
    public string MENSAJE { get; set; }
}
public class Retorno
{
    public Return[] Return;
}