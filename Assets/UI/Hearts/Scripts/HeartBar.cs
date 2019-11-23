using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBar : MonoBehaviour
{

    [SerializeField]
    Image heartPrefab;

    [SerializeField]
    Sprite emptyHeart;
    [SerializeField]
    Sprite fullHeart;

    [SerializeField]
    List<Image> hearts;

    [SerializeField] int maxHearts = 20;

    //Player player;

    // Use this for initialization
    void Start()
    {
        //player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setStartingHearts(int currentHealth, int maxHealth)
    {
        int fullHearts = maxHealth;
        int amountOfFullHearts = 0;
        for (int i = 0; i < maxHearts; i++)
        {
            addHeart();
            if (amountOfFullHearts < fullHearts)
            {
                amountOfFullHearts++;
            }
            else
            {
                hearts[i].color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void addHeart()
    {
        Image heart = Instantiate(heartPrefab, transform) as Image;
        heart.sprite = fullHeart;
        hearts.Add(heart);

    }

    public void changeState(int currentHealth, int maxHealth)
    {
        int emptyHearts = maxHealth - currentHealth;
        int fullHearts = currentHealth;
        int amountOfFullHearts = 0;
        int amountOfEmptyHearts = 0;

        for (int i = 0; i < maxHearts; i++)
        {
            if (amountOfFullHearts < fullHearts)
            {
                hearts[i].color = new Color(1, 1, 1, 1);
                hearts[i].sprite = fullHeart;
                amountOfFullHearts++;
            }
            else if (amountOfEmptyHearts < emptyHearts)
            {
                hearts[i].color = new Color(1, 1, 1, 1);
                hearts[i].sprite = emptyHeart;
                amountOfEmptyHearts++;
            }
            else
            {
                hearts[i].color = new Color(1, 1, 1, 0);
            }
        }
        /*
        foreach(Image heart in hearts)
        {
            Destroy(heart.gameObject);
            hearts.Remove(heart);
        }
        int howMuchFullAlready = 0;
        int howMuchFullInStats = playerStats.CurHealth;
        for(int i = 0; i < playerStats.maxHealth; i++)
        {

            if(howMuchFullAlready<=howMuchFullInStats)
            {
                heart.sprite = fullHeart;
            }
            else
            {
                heart.sprite = emptyHeart;
            }
            howMuchFullAlready++;

        }*/
    }
}
