using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour, IInteractable
{
    bool isOpened = false;

    [SerializeField]
    SpriteRenderer sR;
    [SerializeField]
    Sprite closed;
    [SerializeField]
    Sprite opened;
    [SerializeField]
    GameObject effectSquare;
    [SerializeField]
    Transform itemSpawnPlace;
    [SerializeField]
    PickableWeapon itemToSpawn;

    HighlightningBehaviour highlightningBehaviour;

    public void Interact()
    {
        if(isOpened == false)
        {
            StartCoroutine(open());
            highlightningBehaviour = GetComponent<HighlightningBehaviour>();
            if (highlightningBehaviour)
            {
                IHighlightable ih = highlightningBehaviour.GetComponent<IHighlightable>();
                if (ih != null) ih.Unhighlight();
                
                Destroy(highlightningBehaviour);
            }
        }
    }

    IEnumerator open()
    {
        AudioManager.instance.PlaySound("ChestOpen");
        isOpened = true;
        yield return new WaitForSeconds(0.35f);
        if(itemToSpawn && itemSpawnPlace)Instantiate(itemToSpawn, itemSpawnPlace.position, itemSpawnPlace.rotation);
        if(effectSquare) effectSquare.SetActive(true);
        sR.sprite = opened;
    }
}
