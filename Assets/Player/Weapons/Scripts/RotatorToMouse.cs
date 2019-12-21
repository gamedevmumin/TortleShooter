using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorToMouse : MonoBehaviour, IRotatable
{

    private bool isOnRight = true;

    public void Rotate()
    {
        
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, angle));

            if (mousePos.x > 0 && !isOnRight)
            {
                isOnRight = !isOnRight;
                Vector2 newScale = transform.localScale;
                newScale.y *= -1;
                transform.localScale = newScale;
            }
            else if (mousePos.x < 0 && isOnRight)
            {
                isOnRight = !isOnRight;
                Vector2 newScale = transform.localScale;
                newScale.y *= -1;
                transform.localScale = newScale;
            }
        
    }
}
