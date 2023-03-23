using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Estados de la batalla.
public enum BattleState {Start, AccionPlayer, PoderPlayer, PoderEnemy, Busy}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    public event Action<bool> cuandoFinalicePelea;

    BattleState state;
    int actualAccion;
    int actualPoder;

    GameObject canvasUI;
    Caballero caballero;
    Enemigo enemigo;
    Inventario inventario;

    //Inicia la pelea en el sistema de batalla donde define a los personajes que se enfrentarán, su respectivo inventario y el inicio a la corutina para empezar la batalla.
    public void empezarPelea(Caballero caballero, Enemigo enemigo)
    {
        this.caballero = caballero;
        this.enemigo = enemigo;
        inventario = caballero.getInventario();
        canvasUI = caballero.getCanvasUI();
        StartCoroutine(setupBattle());
    }

    //Se cargan los datos de los personajes y aparece el primer cuadro de diálogo.
    public IEnumerator setupBattle()
    {
        playerUnit.setup(caballero.getEntidad());
        playerHud.setData(playerUnit.Entidad);
        enemyUnit.setup(enemigo.getTipoEnemigo());
        enemyHud.setData(enemyUnit.Entidad);

        dialogBox.setPoderesNombre(playerUnit.Entidad.Poderes);

        yield return dialogBox.escogerDialogo($"Un {enemyUnit.Entidad.getEntidadBase.name} ha aparecido.");
        yield return new WaitForSeconds(1f);

        escogerAccion();
    }

    //Función para escoger entre las acciones (atacar - huir).
    void escogerAccion()
    {
        state = BattleState.AccionPlayer;
        StartCoroutine(dialogBox.escogerDialogo("Escoge una acción"));
        dialogBox.habilitarSelectorDeAccion(true);
    }

    //Se activan los diálogos para que el personaje pueda hacer uso de sus poderes.
    void atacarDeJugador()
    {
        state = BattleState.PoderPlayer;
        dialogBox.habilitarSelectorDeAccion(false);
        dialogBox.habilitarTextoDeDialogo(false);
        dialogBox.habilitarSelectorDePoder(true);
    }

    //Función para intentar huir de la batalla dependiendo de la probabilidad.
    IEnumerator huirDeLaBatalla()
    {
        dialogBox.habilitarTextoDeDialogo(false);
        dialogBox.habilitarSelectorDeAccion(false);
        float generarPosibilidadEscape = UnityEngine.Random.Range(1, 10);
        if(generarPosibilidadEscape <= 5)
        {
            dialogBox.habilitarTextoDeDialogo(true);
            yield return dialogBox.escogerDialogo($"El Caballero ha escapado");
            yield return new WaitForSeconds(1f);
            cuandoFinalicePelea(true);
        }
        else
        {
            dialogBox.habilitarTextoDeDialogo(true);
            yield return dialogBox.escogerDialogo($"El Caballero no ha escapado");
            yield return new WaitForSeconds(1f);
            StartCoroutine(ejecutarAccionEnemigo());
        }
    }

    //Se ejecutan las acciones del jugador como atacar, intercambiar entre los poderes y todo lo que resulta de atacar al enemigo.
    IEnumerator ejecutarAccionJugador()
    {
        state = BattleState.Busy;
        var poder = playerUnit.Entidad.Poderes[actualPoder];
        yield return dialogBox.escogerDialogo($"{playerUnit.Entidad.getEntidadBase.Nombre} usó {poder.Base.Nombre}");
        playerUnit.playAttackAnimation();
        yield return new WaitForSeconds(1f);
        enemyUnit.playHitAnimation();
        bool playerDerrotado = enemyUnit.Entidad.recibirDano(poder, playerUnit.Entidad);
        yield return enemyHud.UpdateVida();

        if(playerDerrotado)
        {
            yield return dialogBox.escogerDialogo($"{enemyUnit.Entidad.getEntidadBase.Nombre} ha muerto");
            enemyUnit.playFaintAnimation();

            yield return new WaitForSeconds(2f);
            cuandoFinalicePelea(true);
        }
        else
        {
            StartCoroutine(ejecutarAccionEnemigo());
        }
    }

    //Se ejecutan las acciones automáticas del enemigo como atacar, intercambiar entre los poderes y todo lo que resulta de atacar al personaje.
    IEnumerator ejecutarAccionEnemigo()
    {
        state = BattleState.PoderEnemy;
        var poder = enemyUnit.Entidad.getRandomPoder();
        yield return dialogBox.escogerDialogo($"{enemyUnit.Entidad.getEntidadBase.Nombre} usó {poder.Base.Nombre}");
        enemyUnit.playAttackAnimation();
        yield return new WaitForSeconds(1f);
        playerUnit.playHitAnimation();
        bool enemigoDerrotado = playerUnit.Entidad.recibirDano(poder, playerUnit.Entidad);
        yield return playerHud.UpdateVida();
        if(enemigoDerrotado)
        {
            yield return dialogBox.escogerDialogo($"{playerUnit.Entidad.getEntidadBase.Nombre} ha muerto");
            playerUnit.playFaintAnimation();

            yield return new WaitForSeconds(2f);
            cuandoFinalicePelea(false);
        }
        else
        {
            escogerAccion();
        }
    }

    //Habilitan las acciones que puede seleccionar el jugador.
    public void habilitarUpdate()
    {
        if(state == BattleState.AccionPlayer)
        {
            habilitarCambioDeAccion();
        }
        else if(state == BattleState.PoderPlayer)
        {
            habilitarCambioDePoder();
        }
    }

    //Se implementa esta función para recorrer las acciones usando las flechas del teclado.
    void habilitarCambioDeAccion()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (actualAccion < 1)
                ++actualAccion;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(actualAccion > 0)
                --actualAccion;
        }

        dialogBox.actualizarCambioDeAccion(actualAccion);

        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if(actualAccion == 0)
            {
                atacarDeJugador();
            }
            else if(actualAccion == 1)
            {
                StartCoroutine(huirDeLaBatalla());
            }
        }
    }

    //Se implementa función para moverse por los poderes del personaje y seleccionarlos.
    void habilitarCambioDePoder()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (actualPoder < playerUnit.Entidad.Poderes.Count - 1)
                ++actualPoder;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(actualPoder > 0)
                --actualPoder;
        }

        dialogBox.actualizarCambioDePoder(actualPoder, playerUnit.Entidad.Poderes[actualPoder]);

        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            dialogBox.habilitarSelectorDePoder(false);
            dialogBox.habilitarTextoDeDialogo(true);
            StartCoroutine(ejecutarAccionJugador());
        }
    }
}