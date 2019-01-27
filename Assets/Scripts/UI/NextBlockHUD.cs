using UnityEngine;

public class NextBlockHUD : MonoBehaviour {

    [SerializeField]
    Level level;

    [SerializeField]
    GameObject[] hintBlocks;

    void Start () {

    }

    void Update () {
        for (int i = 0; i < hintBlocks.Length; i++) {
            if (i == level.nextBlock) {
                hintBlocks[i].SetActive (true);
            } else {
                hintBlocks[i].SetActive (false);
            }
        }
    }
}
