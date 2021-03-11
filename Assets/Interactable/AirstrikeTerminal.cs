using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirstrikeTerminal : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject rockets;
    [SerializeField] private GameObject theFlyingPart;
    private BoxCollider2D box2D;
    
    private SpriteRenderer sR;
    private Animator anim;
    private static readonly int IsOn = Animator.StringToHash("isOn");

    private void Awake()
    {
        anim = theFlyingPart.GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();
        box2D = GetComponent<BoxCollider2D>();
    }
    
    public void Interact()
    {
        AudioManager.instance.PlaySound("RocketLauncherShot");
        rockets.SetActive(true);
        theFlyingPart.SetActive(true);
        anim.SetBool(IsOn, true);
        sR.enabled = false;
        box2D.enabled = false;
    }
}
