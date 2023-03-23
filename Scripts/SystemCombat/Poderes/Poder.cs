using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase base del poder.
public class Poder 
{
    public PoderBase Base {get; set;}
    public Poder(PoderBase pBase)
    {
        Base = pBase;
    }
}
