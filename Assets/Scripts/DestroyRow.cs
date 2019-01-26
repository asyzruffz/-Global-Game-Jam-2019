using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRow : MonoBehaviour {

    [SerializeField]
    Level level;

    void Start () {
        level.OnBlockSettled += CheckForRows;
    }

    void OnDestroy () {
        level.OnBlockSettled -= CheckForRows;
    }

    void CheckForRows () {
        Grid grid = level.GameGrid;

        // Find which row needs to be destroyed
        List<int> needDestroy = new List<int> ();

        for (int row = 0; row < grid.Height; row++) {
            bool fullRow = true;
            for (int x = 0; x < grid.Width; x++) {
                if (grid.Tiles[x, row].ForeTile == null) {
                    fullRow = false;
                    break;
                }
            }

            if (fullRow) {
                needDestroy.Add (row);
            }
        }

        // Destroy determined rows
        for (int i = 0; i < needDestroy.Count; i++) {
            for (int x = 0; x < grid.Width; x++) {
                Destroy (grid.Tiles[x, needDestroy[i]].ForeTile.gameObject);
                grid.Tiles[x, needDestroy[i]].ForeTile = null;
            }
        }

        // Shift blocks down
        int highestBlock = grid.Height - 1;
        for (int i = 0; i < needDestroy.Count; i++) {
            // Iterate each empty row

            int heightMeasure = 0; // To measure and loop until the highest block only
            for (int y = needDestroy[i]; y < highestBlock; y++) {
                for (int x = 0; x < grid.Width; x++) {
                    grid.Tiles[x, y].ForeTile = grid.Tiles[x, y + 1].ForeTile;
                    if (grid.Tiles[x, y].ForeTile) {
                        grid.Tiles[x, y].ForeTile.Translate (Vector3.down, Space.World);
                        heightMeasure = y;
                    }
                }
            }
            highestBlock = heightMeasure;

            // The subsequent empty row also move down
            for (int j = i + 1; j < needDestroy.Count; j++) {
                needDestroy[j]--;
            }
        }

        GetComponent<DetectRoom> ().CheckRoom (grid);
    }
}
