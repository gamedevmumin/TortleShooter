using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysUI : MonoBehaviour
{
    [SerializeField]
    Text keysAmountText;
    [SerializeField]
    PlayerCollectables playerCollectables;

    private void Start()
    {
        UpdateState();
    }
    public void UpdateState()
    {
        keysAmountText.text = "x" + playerCollectables.KeysAmount;
    }
}
