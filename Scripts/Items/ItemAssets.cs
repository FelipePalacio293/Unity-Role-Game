using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemMundo;

    public Sprite spriteEspada;
    public Sprite spriteLanza;
    public Sprite spriteHacha;
    public Sprite spritePocionDanio;
    public Sprite spritePocionVida;
    public Sprite spritePocionAumentoDanio;
    public Sprite spritePocionHuir;
    public Sprite spriteLlave;
}
