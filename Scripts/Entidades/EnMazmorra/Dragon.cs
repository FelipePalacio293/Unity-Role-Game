using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemigo
{
    public override Entidad getTipoEnemigo()
    {
        return this.enemigo;
    }
}
