using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectRoom : MonoBehaviour {

    public int areaRequired = 5;

    [SerializeField]
    Transform character;

    [SerializeField]
    Sprite[] wallpaperSprites;

    [SerializeField]
    TextMeshProUGUI roomSizeText;

    // Called at the end of DestroyRow.CheckForRows()
    public void CheckRoom (Grid grid) {
        List<Vector3> roomArea;
        if (grid.FloodFill (character.position, out roomArea)) {
            int randSprite = Random.Range (0, wallpaperSprites.Length);

            for (int i = 0; i < roomArea.Count; i++) {
                Tile t = grid.TileAt (roomArea[i]);
                if (t != null) {
                    t.BackTile.GetComponent<SpriteRenderer> ().sprite = wallpaperSprites[randSprite];
                }
            }

            Debug.Log ("Room size: " + roomArea.Count);
            SetRoomSizeDisplay (roomArea.Count);
            GameController.Instance.EndGame (roomArea.Count >= areaRequired);
        }
    }

    void Start () {
        SetRoomSizeDisplay (-1);
        Invoke ("DisplayRoomSize", 2);
    }

    void SetRoomSizeDisplay (int amount) {
        if (roomSizeText) {
            if (amount >= 0) {
                roomSizeText.text = amount + "/" + areaRequired;
            } else {
                roomSizeText.text = "?/" + areaRequired;
            }
        }
    }

    void DisplayRoomSize () {
        if (roomSizeText) {
            roomSizeText.gameObject.SetActive (true);
        }
    }
}
