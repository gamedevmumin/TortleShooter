using ItemSystem.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemUI : MonoBehaviour
{
    [SerializeField] private ActiveItemsManager activeItemsManager;
    [SerializeField] private Image image;
    [SerializeField] private Sprite noItemSprite;
    [SerializeField] private Text txt;

    private void Start()
    {
        RefreshItemIcon();
    }
    
    public void RefreshItemIcon()
    {
        if (activeItemsManager.ChosenActiveItem != null)
        {
            Debug.Log(activeItemsManager.ChosenActiveItem.Name);
            image.sprite = activeItemsManager.ChosenActiveItem.Icon;
            txt.text = "Q";
        }
        else
        {
            Debug.Log(" Ni ma");
            image.sprite = noItemSprite;
            txt.text = "";
        }
    }
}
