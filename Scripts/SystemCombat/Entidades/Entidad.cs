using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entidad
{
    [SerializeField] int nivel;
    [SerializeField] EntidadBase entidadBase;

    //Permite saber el nivel de una entidad.
    public int getNivel {
        get { return nivel; }
    }

    //Permite obtener la entidad con sus estadísticas base.
    public EntidadBase getEntidadBase
    {
        get { return entidadBase; }
    }

    //Permite obtener la vida de una entidad.
    public int Vida{get; set;}

    //Permite obtener la lista de poderes.
    public List<Poder> Poderes {get; set;}

    //Inicia la batalla en su forma base.
    public void iniciarBatalla()
    {
        Vida = PuntosDeVidaMax;

        Poderes = new List<Poder>();
        foreach(var poder in getEntidadBase.PoderAprendibles)
        {
            if(poder.Nivel <= getNivel)
            {
                Poderes.Add(new Poder(poder.pBase));
            }

            if(Poderes.Count >= 2)
                break;
        }
    }

    //Fórmula para saber cuánto daño hacen las entidades.
    public int Atacar
    {
        get{return Mathf.FloorToInt((getEntidadBase.PuntosDeDanio * getNivel) / 100f);}
    }

    //Puntos de vida máximos para las entidades base.
    public int PuntosDeVidaMax
    {
        get{return Mathf.FloorToInt(getEntidadBase.PuntosDeVidaMax * getNivel);}
    }

    //Fórmula del daño recibido.
    public bool recibirDano(Poder poder, Entidad atacador)
    {
        int danio = Mathf.FloorToInt((poder.Base.PoderDeAtaque * getNivel) - getEntidadBase.getPuntosDefensa() * 0.2f);
        Vida -= danio;
        if(Vida <= 0)
        {
            Vida = 0;
            return true;
        }

        return false;

    }

    //No utilizado.
    public void mostrarPoderes()
    {
        foreach (Poder pod in Poderes)
        {
            Debug.Log(pod.Base.Nombre);
        }
    }

    //Se obtiene el poder random del enemigo.
    public Poder getRandomPoder()
    {
        int r = Random.Range(0, 2);
        return Poderes[r];
    }
}
