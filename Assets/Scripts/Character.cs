using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    void Start () {

    }

    void Update () {

    }

    void OnTriggerEnter2D (Collider2D collision) {
        Debug.Log ("Character smashed!");
        GameController.Instance.EndGame (false);
    }
}
