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
    [SerializeField]
    PlayerCollectables playerCollectables;
    HighlightningBehaviour highlightningBehaviour;
    CameraShake cameraShake;


    void Awake()
    {
        cameraShake = FindObjectOfType<CameraShake>();
    }

    public void Interact()
    {
        if(isOpened == false)
        {
            if (playerCollectables.KeysAmount > 0)
            {
                StartCoroutine(open());
                highlightningBehaviour = GetComponent<HighlightningBehaviour>();
                if (highlightningBehaviour)
                {
                    IHighlightable ih = highlightningBehaviour.GetComponent<IHighlightable>();
                    if (ih != null) ih.Unhighlight();

                    Destroy(highlightningBehaviour);
                }
                playerCollectables.IncreaseKeysAmount(-1);                  
            }
            else
            {
                AudioManager.instance.PlaySound("LockedChest");
                cameraShake.Shake(0.015f, 0.017f);
            }
        }
    }

    IEnumerator open()
    {
        AudioManager.instance.PlaySound("ChestOpen");
        isOpened = true;
        yield return new WaitForSeconds(0.35f);
        cameraShake.Shake(0.2f, 0.02f);
        if(itemToSpawn && itemSpawnPlace)Instantiate(itemToSpawn, itemSpawnPlace.position, itemSpawnPlace.rotation);
        if(effectSquare) effectSquare.SetActive(true);
        sR.sprite = opened;
    }
}
