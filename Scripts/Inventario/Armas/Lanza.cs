using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanza : Arma
{
    public override void inicializarAtributos()
    {
        nombre = "Lanza del destino";
        animacionArma = "caminarLanza";
        aumentoResistencia = Random.Range(0, 10);
        durabilidad = Random.Range(0, 10);
        aumentoResistencia = Random.Range(0, 12);
    }
}
