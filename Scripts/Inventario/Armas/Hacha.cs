using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacha : Arma
{
    public override void inicializarAtributos()
    {
        nombre = "Hacha del Ragnarok";
        animacionArma = "caminarHacha";
        aumentoResistencia = Random.Range(0, 10);
        durabilidad = Random.Range(0, 10);
        aumentoResistencia = Random.Range(0, 12);
    }
}
