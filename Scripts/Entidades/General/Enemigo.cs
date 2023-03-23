using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemigo : MonoBehaviour
{
    [SerializeField] protected Entidad enemigo;

    private void Start()
    {
        enemigo.iniciarBatalla();
    }

    public abstract Entidad getTipoEnemigo();
}
