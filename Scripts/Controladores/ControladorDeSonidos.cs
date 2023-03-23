using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Esta metodo permite asignar sonidos a diferentes acciones realizadas por el usuario*/
public class ControladorDeSonidos: MonoBehaviour
{
    public GameObject sonidoSeleccionar;
    public GameObject sonidoClick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BotonSonSelec()
    {
        Instantiate(sonidoSeleccionar);
    }

    public void BotonSonClick()
    {
        Instantiate(sonidoClick);
    }
}
