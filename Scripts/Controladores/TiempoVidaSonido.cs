using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiempoVidaSonido : MonoBehaviour
{
    /*Funcion que permite que el objeto se elimine de la escena en un tiempo establecido por el dessarrolador */

    public float tiempoDeVida;

    // Start is called before the first frame update
    void Start()
    {
        /*Se obtiene el objeto a destruir y el tiempo en que se hara*/
        Destroy(gameObject, tiempoDeVida);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
