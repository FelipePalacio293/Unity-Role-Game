using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : Arma
{
    public override void inicializarAtributos()
    {
        nombre = "Espada sangrienta";
        animacionArma = "caminarEspada";
        aumentoResistencia = Random.Range(0, 10);
        durabilidad = Random.Range(0, 10);
        aumentoResistencia = Random.Range(0, 12);
    }
}
