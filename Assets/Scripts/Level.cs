using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour {

    public int nextBlock;

    [SerializeField]
    Vector2Int gridSize;

    [SerializeField]
    Vector2Int[] blockers;

    [SerializeField]
    Transform backTilePrefab;

    [SerializeField]
    Transform foreTilePrefab;

    [SerializeField]
    GameObject[] blockPrefabs;

    public event Action OnBlockSettled;

    public Grid GameGrid { get { return gameGrid; } }

    AudioController aud;
    Grid gameGrid;
    Block currentBlock;
    Vector2 sideBorder;
    float gridOffsetX;

    void Start () {
        aud = GetComponent<AudioController> ();

        OnBlockSettled += PlaySettleSound;
        OnBlockSettled += SpawnNextBlock;

        gridOffsetX = Mathf.Round (gridSize.x / -2f);
        sideBorder = new Vector2 (gridOffsetX, -gridOffsetX - 1);

        gameGrid = new Grid (gridSize, Vector2.right * gridOffsetX);

        // Create Back Tiles
        for (int y = 0; y < gridSize.y; y++) {
            for (int x = 0; x < gridSize.x; x++) {
                gameGrid.Tiles[x, y].BackTile = InstantiateBlock (backTilePrefab, new Vector3 (x + gridOffsetX, y, 0));
            }
        }

        // Added Fore Tiles if any
        for (int i = 0; i < blockers.Length; i++) {
            if (gameGrid.Tiles[blockers[i].x, blockers[i].y].ForeTile == null) {
                gameGrid.Tiles[blockers[i].x, blockers[i].y].ForeTile = InstantiateBlock (foreTilePrefab, new Vector3 (blockers[i].x + gridOffsetX, blockers[i].y, 0));
            }
        }

        nextBlock = Random.Range (0, blockPrefabs.Length);
        Invoke ("SpawnNextBlock", 1.8f);
        //SpawnNextBlock ();
    }
    
    Transform InstantiateBlock (Transform prefab, Vector3 position) {
        return Instantiate (prefab, transform.position + position, Quaternion.identity, transform);
    }

    void SpawnNextBlock () {
        if (GameController.Instance.IsGameOver) return;

        GameObject blockObject = Instantiate (blockPrefabs[nextBlock], Vector3.up * gridSize.y, Quaternion.identity);
        blockObject.GetComponent<MovementController> ().SetSideLimit (sideBorder);
        currentBlock = blockObject.GetComponent<Block> ();
        currentBlock.SetLevel (this);

        nextBlock = Random.Range (0, blockPrefabs.Length);
    }

    public void SettleBlock () {
        OnBlockSettled?.Invoke ();
        PlaySettleSound ();
    }

    void PlaySettleSound () {
        aud.PlaySoundType ("Settle");
    }

    public void PlayRotateSound () {
        aud.PlaySoundType ("Rotate");
    }
}
