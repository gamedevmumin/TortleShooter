using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBehaviour : MonoBehaviour
{
    private bool isInRange;

    IInteractable interaction;

    void Awake()
    {
        interaction = GetComponent<IInteractable>();
    }

    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interaction.Interact();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {         
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {           
            isInRange = false;
        }
    }
}
