using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] HpBar hpBar;
    Entidad _entidad;

    //Se definen las variables de la clase BattleHud para que puedan ser mostradas y utilizadas en la interfaz.
    public void setData(Entidad entidad)
    {
        _entidad = entidad;
        nameText.text = entidad.getEntidadBase.Nombre;
        hpBar.setHP((float)entidad.Vida / entidad.PuntosDeVidaMax);
    }

    //Permite que la barra de vida en el sistema de combate pueda disminuir o aumentar.
    public IEnumerator UpdateVida()
    {
        yield return hpBar.SetHPSmooth((float)_entidad.Vida / _entidad.PuntosDeVidaMax);
    }
}
