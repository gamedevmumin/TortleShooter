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
    [SerializeField] PlayerStats playerStats;

    private void Start()
    {
       setStartingHearts();
       changeState();
    }

    public void setStartingHearts()
    {
        int fullHearts = playerStats.maxHP;
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

    public void changeState()
    {
        int emptyHearts = playerStats.maxHP - playerStats.currentHP;
        int fullHearts = playerStats.currentHP;
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
    }
}
