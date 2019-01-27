using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    Animator anim;
    AudioController aud;
    bool collStay;

    void Start () {
        anim = GetComponent<Animator> ();
        aud = GetComponent<AudioController> ();
    }
    
    void OnTriggerEnter2D (Collider2D collision) {
        StartCoroutine (CheckAfterCollisionResolve ());
    }

    void OnTriggerStay2D (Collider2D collision) {
        collStay = true;
    }

    IEnumerator CheckAfterCollisionResolve () {
        yield return null;
        if (collStay) {
            Debug.Log ("Character smashed!");
            aud.PlaySoundType ("Death");
            GameController.Instance.EndGame (false);
        }
    }
}
