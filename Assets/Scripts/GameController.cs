using System.Collections.Generic;
using UnityEngine;
using Ruffz.Utilities;
using DG.Tweening;

public class GameController : Singleton<GameController> {

    [SerializeField]
    Transform character;

    [Header("Intro Cinematic")]
    [SerializeField]
    bool useCinematic;
    [SerializeField]
    int startZoom;
    [SerializeField]
    Vector3 startPos;

    public bool IsGameOver { get; private set; }

    void Start () {
        if (useCinematic) {
            Sequence introSequence = DOTween.Sequence ();
            introSequence.AppendInterval (1);
            introSequence.Append (Camera.main.transform.DOMove (startPos, 2).From ());
            introSequence.Join (Camera.main.DOOrthoSize (startZoom, 2).From ());
        }
    }

    void Update () {

    }

    public void EndGame (bool hasWon) {
        if (!IsGameOver) {
            Debug.Log (hasWon ? "You win!" : "You lose!");
            character.GetComponent<Animator> ().SetTrigger (hasWon ? "Happy" : "Sad");
            IsGameOver = true;

            if (useCinematic) {
                Sequence endSequence = DOTween.Sequence ();
                endSequence.AppendInterval (1);
                endSequence.Append (Camera.main.transform.DOMove (startPos, 2));
                endSequence.Join (Camera.main.DOOrthoSize (startZoom, 2));
            }

            if (hasWon) {
                Invoke ("ProceedLevel", 5);
            } else {
                Invoke ("RetryLevel", 5);
            }
        }
    }

    void ProceedLevel () {
        GetComponent<NextLevel> ().NextGame ();
    }

    void RetryLevel () {
        GetComponent<NextLevel> ().ReloadLevel ();
    }
}
