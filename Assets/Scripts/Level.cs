using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour {

    [SerializeField]
    Vector2Int gridSize;

    [SerializeField]
    GameObject tilePrefab;

    [SerializeField]
    GameObject[] blockPrefabs;

    public event Action OnBlockSettled;

    public Grid GameGrid { get { return gameGrid; } }

    Grid gameGrid;
    Block currentBlock;
    Vector2 sideBorder;
    float gridOffsetX;

    void Start () {
        OnBlockSettled += SpawnNextBlock;

        gridOffsetX = Mathf.Round (gridSize.x / -2f);

        gameGrid = new Grid (gridSize, Vector2.right * gridOffsetX);

        for (int y = 0; y < gridSize.y; y++) {
            for (int x = 0; x < gridSize.x; x++) {
                gameGrid.Tiles[x, y].BackTile = Instantiate (tilePrefab, transform.position + new Vector3 (x + gridOffsetX, y, 0), Quaternion.identity, transform);
            }
        }

        sideBorder = new Vector2 (gridOffsetX, -gridOffsetX - 1);
        SpawnNextBlock ();
    }
    
    void Update () {

    }

    void SpawnNextBlock () {
        if (GameController.Instance.IsGameOver) return;

        GameObject blockObject = Instantiate (blockPrefabs[Random.Range (0, blockPrefabs.Length)], Vector3.up * gridSize.y, Quaternion.identity);
        blockObject.GetComponent<MovementController> ().SetSideLimit (sideBorder);
        currentBlock = blockObject.GetComponent<Block> ();
        currentBlock.SetLevel (this);
    }

    public void SettleBlock () {
        OnBlockSettled?.Invoke ();
    }
}
