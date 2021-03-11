using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(IInteractable)))]
public class InteractionBehaviour : MonoBehaviour
{
    private bool isInRange;

    private IInteractable interaction;

    private void Awake()
    {
        interaction = GetComponent<IInteractable>();
    }

    private void Update()
    {
        if (!isInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            interaction.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {         
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {           
            isInRange = false;
        }
    }
}
