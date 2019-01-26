using System;
using System.Collections.Generic;
using UnityEngine;
using Ruffz.Utilities;

public class GameController : Singleton<GameController> {

    //public Action OnGameOver;
    public bool IsGameOver { get; private set; }

    void Start () {

    }

    void Update () {

    }

    public void EndGame (bool hasWon) {
        if (!IsGameOver) {
            Debug.Log (hasWon ? "You win!" : "You lose!");
            IsGameOver = true;
        }
    }
}
