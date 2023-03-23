using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    [SerializeField] private Item item;
    private void Start()
    {
        if(item.itemType == Item.ItemType.espada)
            ItemMundo.generarItemMundo(transform.position, new Item { itemType = Item.ItemType.espada, cantidadItem = 1, esStackeable = false});
        else if (item.itemType == Item.ItemType.hacha)
            ItemMundo.generarItemMundo(transform.position, new Item { itemType = Item.ItemType.hacha, cantidadItem = 1, esStackeable = false });
        else if (item.itemType == Item.ItemType.lanza)
            ItemMundo.generarItemMundo(transform.position, new Item { itemType = Item.ItemType.lanza, cantidadItem = 1, esStackeable = false });
        else if (item.itemType == Item.ItemType.pocionVida)
            ItemMundo.generarItemMundo(transform.position, new PocionVida { itemType = Item.ItemType.pocionVida, cantidadItem = 1, esStackeable = true });
        else if (item.itemType == Item.ItemType.pocionDanio)
            ItemMundo.generarItemMundo(transform.position, new PocionDanio { itemType = Item.ItemType.pocionDanio, cantidadItem = 1, esStackeable = true });
        else if (item.itemType == Item.ItemType.pocionAumentoDanio)
            ItemMundo.generarItemMundo(transform.position, new PocionAumento { itemType = Item.ItemType.pocionAumentoDanio, cantidadItem = 1, esStackeable = true });
        else if (item.itemType == Item.ItemType.pocionDefensa)
            ItemMundo.generarItemMundo(transform.position, new PocionHuida { itemType = Item.ItemType.pocionDefensa, cantidadItem = 1, esStackeable = true });
        Destroy(gameObject);
    }

    public static void instanciarItem(Transform transformItem, Item item)
    {
        if (item.itemType == Item.ItemType.espada)
            ItemMundo.generarItemMundo(transformItem.position, new Item { itemType = Item.ItemType.espada, cantidadItem = 1, esStackeable = false });
        else if (item.itemType == Item.ItemType.hacha)
            ItemMundo.generarItemMundo(transformItem.position, new Item { itemType = Item.ItemType.hacha, cantidadItem = 1, esStackeable = false });
        else if (item.itemType == Item.ItemType.lanza)
            ItemMundo.generarItemMundo(transformItem.position, new Item { itemType = Item.ItemType.lanza, cantidadItem = 1, esStackeable = false });
        else if (item.itemType == Item.ItemType.pocionVida)
            ItemMundo.generarItemMundo(transformItem.position, new PocionVida { itemType = Item.ItemType.pocionVida, cantidadItem = 1, esStackeable = true });
        else if (item.itemType == Item.ItemType.pocionDanio)
            ItemMundo.generarItemMundo(transformItem.position, new PocionDanio { itemType = Item.ItemType.pocionDanio, cantidadItem = 1, esStackeable = true });
        else if (item.itemType == Item.ItemType.pocionAumentoDanio)
            ItemMundo.generarItemMundo(transformItem.position, new PocionAumento { itemType = Item.ItemType.pocionAumentoDanio, cantidadItem = 1, esStackeable = true });
        else if (item.itemType == Item.ItemType.pocionDefensa)
            ItemMundo.generarItemMundo(transformItem.position, new PocionHuida { itemType = Item.ItemType.pocionDefensa, cantidadItem = 1, esStackeable = true });
    }
}
