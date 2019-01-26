using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRoom : MonoBehaviour {

    public int areaRequired = 5;

    [SerializeField]
    Transform character;

    // Called at the end of DestroyRow.CheckForRows()
    public void CheckRoom (Grid grid) {
        List<Vector3> roomArea;
        if (grid.FloodFill (character.position, out roomArea)) {

            for (int i = 0; i < roomArea.Count; i++) {
                Tile t = grid.TileAt (roomArea[i]);
                if (t != null) {
                    t.BackTile.GetComponent<SpriteRenderer> ().color = Color.yellow;
                }
            }

            Debug.Log ("Room size: " + roomArea.Count);
            GameController.Instance.EndGame (roomArea.Count >= areaRequired);
        }
    }
}
