using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Poder", menuName = "Poder/Crear nuevo poder")]
public class PoderBase : ScriptableObject
{
    [SerializeField] string nombre;
    [SerializeField] int poderDeAtaque;

    //Returna el nombre de la entidad.
    public string Nombre
    {
        get{return nombre;}
    }

    //Retorna el poder de ataque de la entidad.
    public int PoderDeAtaque
    {
        get{return poderDeAtaque;}
    }
    
}
