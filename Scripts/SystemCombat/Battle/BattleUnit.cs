using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] bool isPlayerUnit;


    public Entidad Entidad {get; set;}

    Image image;
    Vector3 originalPos;
    Color originalColor;
    
    //Función de Unity
    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    //Permite que al encontrarse a los enemigos durante el mapa, pueda ser cargada la imagen en el sistema de combate.
    public void setup(Entidad entidad)
    {
        Entidad = entidad;
        image.sprite = Entidad.getEntidadBase.Sprite;
        playEnterAnimation();
        image.color = originalColor;
    }

    //Permite ver las animaciones de los personajes al ejecutar una acción de ataque.
    public void playEnterAnimation()
    {
        if(isPlayerUnit)
            image.transform.localPosition = new Vector3(500f, originalPos.y);
        else
            image.transform.localPosition = new Vector3(-500f, originalPos.y);
        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    //Permite ver las animaciones de los personajes al ser atacados.
    public void playAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        if(isPlayerUnit)
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f));
        else
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f));
        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }

    //Permite ver las animaciones de los personajes al ser atacados y afectados.
    public void playHitAnimation()
    {
        var Sequence = DOTween.Sequence();
        Sequence.Append(image.DOColor(Color.gray, 0.1f));
        Sequence.Append(image.DOColor(originalColor, 0.1f));
    }

    //Permite ver la animación de morir al acabarse la vida de algún personaje.
    public void playFaintAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 150f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }
}
