using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void cargarEscenaFacil()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void cargarEscenaDificil()
    {
        SceneManager.LoadScene("EscenaNormal");
    }

}
