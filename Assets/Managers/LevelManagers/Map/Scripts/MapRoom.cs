using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRoom : MonoBehaviour {

    [SerializeField]
    TextMesh typeText;
    [SerializeField]
    public Transform playerIconPosition;

    bool IsActive = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initialize(string type)
    {
        typeText.text = type;
        deactivate();
    }

    public void complete()
    {
        typeText.text = "DONE!";
        AudioManager.instance.PlaySound("LevelUp");
    }

    public void activate()
    {
        IsActive = true;
        typeText.gameObject.SetActive(true);
        transform.localScale = new Vector2(1f, 1f);
    }

    public void deactivate()
    {
        IsActive = false;
        typeText.gameObject.SetActive(false);
        transform.localScale = new Vector2(0.8f, 0.8f);
    }
}
