using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highligthtedColor;
    [SerializeField] GameObject accionSelector;
    [SerializeField] GameObject poderSelector;
    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> poderTexts;
    [SerializeField] List<Text> pocionTexts;
    

    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    //Función para iniciar el texto de diálogo y darle tiempo a la aparición de letras.
    public IEnumerator escogerDialogo(string dialog)
    {
        dialogText.text = "";
        foreach(var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
    }

    //Función para activar el texto de diálogo general con los que inicia el combate.
    public void habilitarTextoDeDialogo(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    //Permite seleccionar entre las acciones (Huir - atacar).
    public void habilitarSelectorDeAccion(bool enabled)
    {
        accionSelector.SetActive(enabled);
    }

    //Permite seleccionar los poderes después de haber accedido al apartado de atacar.
    public void habilitarSelectorDePoder(bool enabled)
    {
        poderSelector.SetActive(enabled);
    }

    //Ilumina la acción que se tiene seleccionada.
    public void actualizarCambioDeAccion(int accionSelected)
    {
        for(int i = 0; i < actionTexts.Count; ++i)
        {
            if(i == accionSelected)
                actionTexts[i].color = highligthtedColor;
            else
                actionTexts[i].color = Color.black;
        }
    }

    //Ilumina el poder que se tiene seleccionado.
    public void actualizarCambioDePoder(int poderSelected, Poder poder)
    {
        for(int i = 0; i < poderTexts.Count; ++i)
        {
            if(i == poderSelected)
                poderTexts[i].color = highligthtedColor;
            else
                poderTexts[i].color = Color.black;
        }
    }

    //Se guardan los poderes con sus nombre en una lista.
    public void setPoderesNombre(List<Poder> poderes)
    {
        for(int i = 0; i < poderTexts.Count; ++i)
        {
            if(i < poderes.Count)
                poderTexts[i].text = poderes[i].Base.Nombre;
            else
                poderTexts[i].text = "-";
        }
    }

    //Se guardan las pociones con sus nombres en una lista.
    public void setNombresPociones(List<Item> items)
    {
        foreach(Item item in items)
        {
            switch (item.getItemType())
            {
                case Item.ItemType.pocionVida: 
                    poderTexts[0].text = "Pocion Vida";
                    break;
                case Item.ItemType.pocionDanio: 
                    poderTexts[1].text = "Pocion danio";
                    break;
                case Item.ItemType.pocionDefensa: 
                    poderTexts[2].text = "Pocion huir";
                    break;
                case Item.ItemType.pocionAumentoDanio: 
                    poderTexts[3].text = "Pocion aumento danio";
                    break;
            }
            
        }
    }
}
