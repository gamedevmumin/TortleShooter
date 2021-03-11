using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightningBehaviour : MonoBehaviour
{

    private IHighlightable highlightable;

    // Start is called before the first frame update
    void Awake()
    {
        highlightable = GetComponent<IHighlightable>();
    }

    private void Start()
    {
        highlightable.Unhighlight();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            highlightable.Highlight();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            highlightable.Unhighlight();
        }
    }

}
