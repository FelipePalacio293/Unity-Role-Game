using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMundo : MonoBehaviour
{
    
    public static ItemMundo generarItemMundo(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemMundo, position, Quaternion.identity);
        ItemMundo itemMundo = transform.GetComponent<ItemMundo>();
        itemMundo.setItem(item);

        return itemMundo;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void setItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.getSprite();
        //spriteRenderer.material.color = Color.gray;
    }

    public Item getItem()
    {
        return this.item;
    }

    public void destruirItem()
    {
        Destroy(gameObject);
    }
}
