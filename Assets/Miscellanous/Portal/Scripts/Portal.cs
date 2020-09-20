using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour {
    [SerializeField]
    PlayerController player;

    [SerializeField]
    float pullRadius = 2;
    [SerializeField]
    float pullForce = 1;

    CameraShake cameraShake;
    Animator sceneTransitionAnim;
    Animator anim;
    // Use this for initialization
    void Start () {

        anim = transform.Find ("Sprite").GetComponent<Animator> ();

    }

    public void Open () {
        cameraShake = GameObject.Find ("CameraShake").GetComponent<CameraShake> ();
        AudioManager.instance.PlaySound ("PortalOpen");
        cameraShake.Shake (0.2f, 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (player) {
            Vector2 forceDirection = transform.position - player.transform.position;
            player.rb.velocity = forceDirection * pullForce;
        }
    }

    void OnTriggerEnter2D (Collider2D coll) {
        if (coll.CompareTag ("Player")) {
            anim.SetBool ("isClosing", true);
            AudioManager.instance.PlaySound ("PortalClose");
            sceneTransitionAnim = GameObject.Find ("Transition").GetComponent<Animator> ();
            sceneTransitionAnim.SetTrigger ("end");
            player.gameObject.SetActive (false);
            StartCoroutine (load ());
        }
    }

    IEnumerator load () {
        yield return new WaitForSeconds (1f);
        player.gameObject.SetActive (true);
        SceneManager.LoadScene ("Map");
    }

}