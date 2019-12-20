using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NameAndMessageHighlight : MonoBehaviour, IHighlightable
{
    [SerializeField]
    string _name;
    [SerializeField]
    string message;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Text messageText;

    void Start()
    {
        nameText.text = _name;
        messageText.text = message;
    }

    public void Highlight()
    {
        nameText.gameObject.SetActive(true);
        messageText.gameObject.SetActive(true);
    }

    public void Unhighlight()
    {
        nameText.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false);
    }

}
