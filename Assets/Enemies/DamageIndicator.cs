using UnityEngine;

public class DamageIndicator : MonoBehaviour {
    void Update () {
        transform.position = new Vector2 (transform.position.x, transform.position.y + 10f * Time.deltaTime);
    }
}