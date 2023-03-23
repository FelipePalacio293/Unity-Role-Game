using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arma 
{
    protected string nombre;
    protected float puntosDeAtaque;
    protected int durabilidad;
    protected float aumentoResistencia;
    protected string animacionArma;

    public abstract void inicializarAtributos();

    public float getAumentoResistencia()
    {
        return aumentoResistencia;
    }

    public int getDurabilidadArma()
    {
        return durabilidad;
    }

    public float getPuntosDeAtaque()
    {
        return puntosDeAtaque;
    }

    public string getNombre()
    {
        return nombre;
    }

    public string getNombreAnimacion()
    {
        return animacionArma;
    }
}
