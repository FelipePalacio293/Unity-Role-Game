using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario
{
    public event EventHandler siLaListaCambia;
    List<Item> items;
    private int capacidadInventario = 10;
    private Action<Item> usarAccionItem;

    public Inventario(Action<Item> usarAccionItem)
    {
        this.usarAccionItem = usarAccionItem;
        items = new List<Item>();
    }


    public bool comprobarSiPuedeAgregarItem()
    {
        // Se recorre la lista de items en el inventario para determinar si es mayor que la capacidadDelInventario
        int cantItems = 0;
        foreach (Item item1 in items)
        {
            cantItems += item1.cantidadItem;
        }
        if(cantItems >= capacidadInventario)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public void usarItem(Item item)
    {
        usarAccionItem(item);
        if (!item.getEsStackeable())
            removerItem(item);
    }

    public void agregarItem(Item item)
    {
        if (item.comprobarSiEsStackeable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item item1 in items)
            {
                if (item1.itemType == item.itemType)
                {
                    item1.cantidadItem += 1;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                items.Add(item);
            }
        }
        else
        {
            items.Add(item);
        }
        siLaListaCambia?.Invoke(this, EventArgs.Empty);
    }

    public void removerItem(Item item) 
    {
        if (item.comprobarSiEsStackeable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in items)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.cantidadItem -= item.cantidadItem;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.cantidadItem <= 0)
            {
                items.Remove(itemInInventory);
            }
        }
        else
        {
            items.Remove(item);
        }
        siLaListaCambia?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> getItemList()
    {
        return items;
    }
}