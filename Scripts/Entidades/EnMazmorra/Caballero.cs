using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Caballero : MonoBehaviour
{
    [SerializeField] Entidad entidadCaballero;
    private Inventario inventario;
    [SerializeField] private InventoryUI inventarioUI;
    [SerializeField] private GameObject canvasInventario;
    private Rigidbody2D rigidbody2D;
    bool inventarioAbierto;
    [SerializeField] TextMeshProUGUI cantVidaUi;
    [SerializeField] TextMeshProUGUI cantDefensaUi;
    [SerializeField] TextMeshProUGUI nombreArma;
    [SerializeField] TextMeshProUGUI durabilidadArma;
    Player player;
    Arma armaActual;

    private void Start()
    {
        player = GetComponent<Player>();
        inventario = new Inventario(usarItem); // Se inicializa el inventario con la funcion usada para el evento utilizado cuando se usa un item
        entidadCaballero.iniciarBatalla(); // Se inicializan los atributos del caballero
        canvasInventario.SetActive(false);
        inventarioUI.setInventario(inventario);
        rigidbody2D = GetComponent<Rigidbody2D>();
        inventarioAbierto = false;
        entidadCaballero.getEntidadBase.setPuntosDefensa(5);
    }

    // Se verifica el tipoDeItem que llega y con base a ello se llama la funcion que activa sus propiedades
    private void usarItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.pocionVida:
                usarPocionVida(item);
                inventario.removerItem(new Item { itemType = Item.ItemType.pocionVida, cantidadItem = 1 });
                break;
            case Item.ItemType.pocionDanio:
                usarPocionAumentoDanio(item);
                inventario.removerItem(new Item { itemType = Item.ItemType.pocionDanio, cantidadItem = 1 });
                break;
            case Item.ItemType.pocionDefensa:
                usarPocionDefensa(item);
                inventario.removerItem(new Item { itemType = Item.ItemType.pocionDefensa, cantidadItem = 1 });
                break;
            case Item.ItemType.pocionAumentoDanio:
                usarPocionAumentoDanio(item);
                inventario.removerItem(new Item { itemType = Item.ItemType.pocionAumentoDanio, cantidadItem = 1 });
                break;
            case Item.ItemType.espada:
                Arma espada = new Espada { };
                espada.inicializarAtributos();
                usarArma(espada);
                
                break;
            case Item.ItemType.lanza:
                Arma lanza = new Lanza { };
                lanza.inicializarAtributos();
                usarArma(lanza);
                break;
            case Item.ItemType.hacha:
                Arma hacha = new Hacha { };
                hacha.inicializarAtributos();
                usarArma(hacha);
                break;
        }
    }

    // 
    public void usarPocionVida(Item item)
    {
        if (entidadCaballero.Vida + item.getPropiedades() < entidadCaballero.getEntidadBase.PuntosDeVidaMax)
        {
            float vida = item.getPropiedades();
            entidadCaballero.Vida += (int)vida;
        } 
    }

    public void usarPocionDanio(Item item)
    {
        
    }

    public void usarPocionAumentoDanio(Item item)
    {
        float danio = item.getPropiedades();
        entidadCaballero.getEntidadBase.PuntosDeDanio += (int)danio;
    }

    public void usarPocionDefensa(Item item)
    {
        float defensa = item.getPropiedades();
        entidadCaballero.getEntidadBase.setPuntosDefensa((int)defensa);
    }

    public void usarArma(Arma arma)
    {
        player.setAnimacionActual(arma.getNombreAnimacion());
        if (armaActual != null)
        {
            entidadCaballero.getEntidadBase.setPuntosDefensa((int)arma.getAumentoResistencia() - (int)armaActual.getAumentoResistencia());
            entidadCaballero.getEntidadBase.setPuntosDanio((int)arma.getPuntosDeAtaque() - (int)armaActual.getPuntosDeAtaque());
        }
        else
        {
            entidadCaballero.getEntidadBase.setPuntosDefensa((int)arma.getAumentoResistencia());
            entidadCaballero.getEntidadBase.setPuntosDanio((int)arma.getPuntosDeAtaque());
        }

        armaActual = arma;
    }

    private void Update()
    {
        if (inventarioAbierto)
        {
            cerrarInventario();
        }
        else
        {
            abrirInventario();
        }
        fijarValoresAtributosUI();
    }

    // Se fijan los stats del caballero en el UI del mundo
    public void fijarValoresAtributosUI()
    {
        cantVidaUi.SetText(entidadCaballero.Vida.ToString() + "/" + entidadCaballero.getEntidadBase.PuntosDeVidaMax.ToString());
        cantDefensaUi.SetText(entidadCaballero.getEntidadBase.getPuntosDefensa().ToString());
        durabilidadArma.SetText(entidadCaballero.Atacar.ToString());
        if (armaActual != null)
            nombreArma.SetText(armaActual.getNombre());
    }

    public Entidad getEntidad()
    {
        return entidadCaballero;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se comprueba el momento donde el personaje toca un item para agregarlo al inventario
        ItemMundo itemMundo = collision.GetComponent<ItemMundo>();
        if (itemMundo != null)
        {
            if (inventario.comprobarSiPuedeAgregarItem())
            {
                inventario.agregarItem(itemMundo.getItem());
                itemMundo.destruirItem(); // Se destruye el item que esta en la escena
            }
        }
    }

    public void abrirInventario()
    {
        if (Input.GetKeyDown(KeyCode.E) && !inventarioAbierto)
        {
            canvasInventario.SetActive(true);
            inventarioAbierto = true;
        }
    }

    public void cerrarInventario()
    {
        if (Input.GetKeyDown(KeyCode.E) && inventarioAbierto)
        {
            canvasInventario.SetActive(false);
            inventarioAbierto = false;
        }
    }

    public Inventario getInventario()
    {
        return inventario;
    }

    public GameObject getCanvasUI()
    {
        return canvasInventario;
    }
}