using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entidad", menuName = "Entidad/Crear una nueva entidad")]
public class EntidadBase : ScriptableObject
{
    [SerializeField] string nombre;
    [SerializeField] int puntosDeDanio;
    [SerializeField] int puntosDeVidaMax;
    [SerializeField] int puntosDefensa;
    [SerializeField] Sprite sprite;   
    [SerializeField] List<PoderAprendible> poderAprendibles; 

    //Se guardan los puntos de daño según las estadísticas de la entidad.
    public void setPuntosDanio(int danio)
    {
        puntosDeDanio += danio;
    }

    //Se obtienen los puntos de daño de x entidad.
    public int getPuntosDanio()
    {
        return puntosDeDanio;
    }

    //Retorna el nombre de la entidad.
    public string Nombre
    {
        get{return nombre;}
    }

    //Retorna los puntos de defensa de la entidad.
    public int getPuntosDefensa()
    {
        return puntosDefensa;
    }

    //Se guardan los puntos de defensa dependiente de las estadísticas de la entidad.
    public void setPuntosDefensa(int defensa)
    {
        if(defensa + puntosDefensa < 10)
        {
            puntosDefensa += defensa;
        }
    }

    //Daño de las entidades.
    public int PuntosDeDanio { get; set; }

    //Vida máxima de las entidades.
    public int PuntosDeVidaMax
    {
        get{return puntosDeVidaMax;}
    }

    //Lista de los poderes de cada entidad específica.
    public List<PoderAprendible> PoderAprendibles
    {
        get {return poderAprendibles;}
    }

    //Imágenes para las entidades, items y demás cosas.
    public Sprite Sprite
    {
        get{return sprite;}
    }
}

[System.Serializable]

public class PoderAprendible
{
    [SerializeField] PoderBase poderBase;
    [SerializeField] int nivel;
    public int Nivel
    {
        get{return nivel;}
    }
    public PoderBase pBase
    {
        get{return poderBase;}
    }
}

//Enumera las entidades existentes.
public enum EntidadType
{
    None,
    Caballero,
    Minotauro,
    Dragon,
    Ciclope
}
