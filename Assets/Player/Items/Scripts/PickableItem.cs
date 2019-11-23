using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableItem : MonoBehaviour
{
    [SerializeField]
    Item item;
    SpriteRenderer sR;

    Text nameText;

    bool isInRange = false;

    PlayerItems playerItems;

    void Awake()
    {
        if (item != null)
        {
            Set();
        }
    }

    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                item.OnPickUp(this);
                playerItems.PickUpItem(item);
                Destroy(gameObject);
            }
        }
    }

    public void Initialize(Item item)
    {
        this.item = item;
        Set();
    }

    private void Set()
    {
        Transform graphic = transform.Find("Graphic");
        sR = graphic.GetComponent<SpriteRenderer>();
        sR.sprite = item.Icon;
        Transform textObject = transform.Find("Canvas").Find("NameText");
        nameText = textObject.GetComponent<Text>();
        nameText.text = item.Name;
        nameText.transform.parent.gameObject.SetActive(false);
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            nameText.transform.parent.gameObject.SetActive(true);
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            nameText.transform.parent.gameObject.SetActive(false);
            isInRange = false;
        }
    }
}
