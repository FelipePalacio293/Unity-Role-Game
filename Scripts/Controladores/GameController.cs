using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum EstadoJuego
{
    normal, 
    luchando
}

public class GameController : MonoBehaviour
{
    [SerializeField] Transform[] transforms; // Se crea una lista de transforms que se inicializa en la jerarquia
    [SerializeField] Item[] items; // Se crea una lista de items que se inicializa en la jerarquia
    [SerializeField] Player player; 
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera camera;
    [SerializeField] Camera cameraSecundaria;
    [SerializeField] Canvas canvasInventario;
    [SerializeField] GameObject canvasDatos;
    [SerializeField] GameObject[] enemigos;
    [SerializeField] AudioClip musicaAmbiental;
    [SerializeField] AudioClip musicaPelea;
    AudioSource audioSource;
    EstadoJuego estadoJuego;

    private void Start()
    {
        generarItem();
        generarEnemigo();
        player.iniciadorPeleas += iniciarBatalla; // Se inicializa el evento iniciarPeleas del player, asociado con iniciarBatalla del controller
        battleSystem.cuandoFinalicePelea += terminarPelea; // Se inicializa el evento cuandoFinalicePelea del BattleSystem, asociado con el terminar pelea del battlesystem
        audioSource = GetComponent<AudioSource>();
        generarItemsIniciales();
        generarEnemigosIniciales();
        audioSource.clip = musicaAmbiental;
        audioSource.Play();
    }

    // Funcion que genera los 25 items iniciales
    public void generarItemsIniciales()
    {
        for(int i = 0; i <= 20; i++)
        {
            int x = Random.Range(0, transforms.Length - 1);
            int y = Random.Range(0, items.Length - 1);
            ItemWorldSpawner.instanciarItem(transforms[x], items[y]); // Se hace uso de la funcion de Unity Instantiate
        }
    }

    // Funcion que genera los 25 enemigos iniciales
    public void generarEnemigosIniciales()
    {
        for (int i = 0; i <= 20; i++)
        {
            int x = Random.Range(0, transforms.Length - 1);
            int y = Random.Range(0, enemigos.Length - 1);
            Instantiate(enemigos[y], transforms[x].position, Quaternion.identity); // Se hace uso de la funcion de Unity Instantiate
        }
    }

    public void terminarPelea(bool ganador)
    {
        estadoJuego = EstadoJuego.normal; // Se pone el estado del juego en normal
        camera.gameObject.SetActive(true); // Se activa la camara principal
        battleSystem.gameObject.SetActive(false);
        canvasDatos.SetActive(true); 
        canvasInventario.worldCamera = camera;
        audioSource.clip = musicaAmbiental;
        audioSource.Play();
        if (!ganador)
        {
            SceneManager.LoadScene("Muerte");
        }
    }

    public void iniciarBatalla()
    {
        estadoJuego = EstadoJuego.luchando; // Se pone el estado del juego en luchando
        battleSystem.gameObject.SetActive(true);  // Se activa el battlesystem completo
        camera.gameObject.SetActive(false);  // Se desactiva la camara principal
        canvasInventario.worldCamera = cameraSecundaria;
        canvasDatos.SetActive(false);
        audioSource.clip = musicaPelea;  // Se cambia el clip de reproduccion
        audioSource.Play();
        var caballero = player.GetComponent<Caballero>();  // Del Player se extrae el componente caballero
        var enemigo = player.getEnemigoActual().GetComponent<Enemigo>(); // Del Player se extrae el componente enemigo que esta tocando actualmente
        battleSystem.empezarPelea(caballero, enemigo);
    }

    private void Update()
    {
        if(estadoJuego == EstadoJuego.normal) // Se llama al Update del player solo si el estado del juego es normal
        {
            player.habilitarUpdate();
        }
        else if(estadoJuego == EstadoJuego.luchando) // Se llama al Update del luchando solo si el estado del juego es luchando
        {
            battleSystem.habilitarUpdate();
        }
    }

    public void generarItem() 
    {
        int x = Random.Range(0, transforms.Length - 1); // Se genera una posicion aleatoria de la lista de transforms
        int y = Random.Range(0, items.Length - 1); // Se genera una posicion aleatoria de la lista de items
        ItemWorldSpawner.instanciarItem(transforms[x], items[y]);
        Invoke("generarItem", 30f);
    }

    public void generarEnemigo()
    {
        int x = Random.Range(0, transforms.Length - 1);
        int y = Random.Range(0, enemigos.Length - 1);
        Instantiate(enemigos[y], transforms[x].position, Quaternion.identity);
        Invoke("generarEnemigo", 30f);
    }
}
