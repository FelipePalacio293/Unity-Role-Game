using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenmyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float visionRadius;
    public float attackRadius;
    public float speed;

    //Variable para guardar al judador
    GameObject player;

    //Varaiable para guardar la posicion inicial
    Vector3 initialPosition;

    //Animadoe y cuerpo cinematico con la rotacion en Z congelada
    Animator anim;
    Rigidbody2D rb2d;

    void Start()
    {
        //Recuperamos al jufador gracias al Tag
        player = GameObject.FindGameObjectWithTag("Player");
        
        //Guardamos la posicion inicial
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Por defefcto nuestro target siempre sera nuestra posicion inicial 
        Vector3 target = initialPosition;
        
        //Comprobamos un Raycast del enemigo hasta el jugador 

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            player.transform.position - transform.position,
            visionRadius * 1f,
            1 << LayerMask.NameToLayer("Default") //Capa en la cual va detectar los elementos
            //Poner el propio Enemy en una layer distina a Defautl y al Prefab Slash una layer Attack
            //Tambien poner al obAttack y al Prefab Slash una layer attack
            // Sino los detectara y se mueve atras al hacer ataques

        );

        //Aqui podemos debugaear el Raycast
        Vector3 forward =  transform.TransformDirection(player.transform.position -transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);
    }
}
