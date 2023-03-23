using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] GameObject vida;

    //Variantes entre la vida actual y la vida que se pierde.
    public void setHP(float hpNormalized)
    {
        vida.transform.localScale = new Vector3(hpNormalized, 1f);
    }

    //Transforma la barra de vida a vida actual, haciendo que se dé el efecto de menor vida.
    public IEnumerator SetHPSmooth(float newHp)
    {
        float curHp = vida.transform.localScale.x;
        float changeAmt = curHp - newHp;

        while(curHp - newHp > Mathf.Epsilon)
        {
            curHp -= changeAmt * Time.deltaTime;
            vida.transform.localScale = new Vector3(curHp, 1f);
            yield return null;
        }
        vida.transform.localScale = new Vector3(newHp, 1f);

    }
}
