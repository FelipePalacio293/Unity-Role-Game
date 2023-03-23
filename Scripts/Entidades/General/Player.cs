using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    private float speed = 4f;
    private Animator anim;
    private Rigidbody2D rigidbody2D;
    private Vector2 mov;

    public event Action iniciadorPeleas;

    private GameObject enemigoAtacandoActual;
    private string animacionActual;

    //Función de Unity
    private void Awake()
    {

    }

    //Actualiza las animaciones del jugador para poder moverse por el mapa.
    public void setAnimacionActual(string animacionActual)
    {
        this.animacionActual = animacionActual;
    }

    //Función para habilitar la primera acción de movimiento.
    void Start()
    {
        animacionActual = "caminar";
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

    }

    //Permite activar las animaciones 'caminar'.
    public void habilitarUpdate()
    { 
        
        caminar();
        
    }

    //Cambia las posiciones por las que se va moviendo el personaje.
    void FixedUpdate()
    {
        actualizarPosicion();
    }

    //Función para poder caminar en las diferentes direcciones y ejes del plano cartesiano. Además usar un bool para poder cambiar entre quieto y caminar.
    void caminar()
    {
        mov = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (mov != Vector2.zero)
        {
            anim.SetFloat("movX", mov.x);
            anim.SetFloat("movY", mov.y);
            anim.SetBool(animacionActual, true);
        }
        else
        {
            anim.SetBool(animacionActual, false);
        }
    }

    //Actualiza las posiciones por las que se mueve el personaje.
    void actualizarPosicion()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + mov * speed * Time.deltaTime);
    }

    //Colisión contra los enemigos e inicia el sistema de combate.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            mov = Vector2.zero;
            anim.SetBool("caminar", false);
            enemigoAtacandoActual = collision.gameObject;
            iniciadorPeleas();
            Destroy(collision.gameObject);
        }
    }

    //Retorna el enemigo con el que se topa en ese momento.
    public GameObject getEnemigoActual()
    {
        return enemigoAtacandoActual;
    }
}