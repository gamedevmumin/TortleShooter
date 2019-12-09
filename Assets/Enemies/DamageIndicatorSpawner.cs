using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicatorSpawner : MonoBehaviour { 

    [SerializeField]
    protected DamageIndicator damageIndicator;
    public void SpawnEffect(Vector2 position, Vector2 scale, DamageInfo damageInfo)
    {
        Debug.Log("Effect spawned");
        damageIndicator.GetComponentInChildren<Text>().text = damageInfo.damageDone.ToString();
        Vector2 pos = new Vector2(position.x + Random.Range(-0.5f, 0.5f), position.y + Random.Range(-0.5f, 0.5f));
        GameObject eo = Instantiate(damageIndicator.gameObject, pos, Quaternion.Euler(Vector2.zero));
        eo.transform.localScale = scale;
        Destroy(eo, 5f);
    }
}
