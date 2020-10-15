using UnityEngine;
using UnityEngine.UI;

public class DamageIndicatorSpawner : MonoBehaviour {

    [SerializeField]
    protected DamageIndicator damageIndicator;
    public void SpawnEffect (Vector2 position, Vector2 scale, DamageInfo damageInfo) {
        Text text = damageIndicator.GetComponentInChildren<Text> ();
        text.text = damageInfo.DamageDone.ToString ();
        text.color = damageInfo.WasCritical ? new Color32(200, 0, 0, 255) : new Color32(255, 255, 255, 255);
        Vector2 pos = new Vector2 (position.x + Random.Range (-0.5f, 0.5f), position.y + Random.Range (-0.5f, 0.5f));
        GameObject eo = Instantiate (damageIndicator.gameObject, pos, Quaternion.Euler (Vector2.zero));
        eo.transform.localScale = damageInfo.WasCritical ? scale * 2f : scale;
        Destroy (eo, 0.25f);
    }
}