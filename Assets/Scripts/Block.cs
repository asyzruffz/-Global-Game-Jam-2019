using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField]
    float speed = 1;

    [SerializeField]
    Transform[] tiles;

    public float Speed {
        get { return speed; }
        set {
            if (value > 0) {
                delay = 1 / value;
            } else {
                delay = float.MaxValue;
            }
        }
    }

    Level level;
    float delay = float.MaxValue;
    TimeSince ts;
    bool isOverlapped;
    bool onGround;

    void Start () {
        ts = 0;
        Speed = speed;

        for (int i = 0; i < tiles.Length; i++) {
            tiles[i].GetComponent<BlockTile> ().SetParent (this);
        }
    }

    void Update () {
        if (GameController.Instance.IsGameOver) return;

        if (!onGround) {
            Fall ();
        }
    }

    void LateUpdate () {
        if (GameController.Instance.IsGameOver) return;

        CheckGround ();
    }

    void Fall () {
        if (ts > delay) {
            transform.Translate (Vector3.down, Space.World);
            StartCoroutine (DelayVisibility ());
            ts = 0;
        }
    }

    void CheckGround () {
        if (isOverlapped && !onGround) {
            // Move back up to resolve collision
            transform.Translate (Vector3.up, Space.World);
            isOverlapped = false;

            if (level) {
                SetBlocksToLevel ();
                level.SettleBlock ();
            }
            onGround = true;
            SetVisible (true);
        }
    }

    void SetBlocksToLevel () {
        //Debug.Log (name + ": Stop!");
        Grid grid = level.GameGrid;
        for (int i = 0; i < tiles.Length; i++) {
            Tile gridTile = grid.TileAt (tiles[i].position);
            if (gridTile != null) {
                gridTile.ForeTile = tiles[i];
                tiles[i].SetParent (level.transform);
                tiles[i].GetComponent<BlockTile> ().SetParent (null);
            } else {
                Debug.Log (tiles[i].name + " can't be settled at " + tiles[i].position);
                GameController.Instance.EndGame (false);
            }
        }

        Destroy (gameObject);
    }

    public float ExceedLimit (Vector2 limit) {
        for (int i = 0; i < tiles.Length; i++) {
            if (tiles[i].position.x < limit.x) {
                return limit.x - tiles[i].position.x;
            } else if (tiles[i].position.x > limit.y) {
                return limit.y - tiles[i].position.x;
            }
        }
        return 0;
    }

    public void SetOverlapped () {
        isOverlapped = true;
    }

    public void SetLevel (Level lvl) {
        level = lvl;
    }
    
    void SetVisible (bool visible) {
        for (int i = 0; i < tiles.Length; i++) {
            tiles[i].GetComponent<SpriteRenderer> ().enabled = visible;
        }
    }

    IEnumerator DelayVisibility () {
        SetVisible (false);
        yield return null;
        SetVisible (true);
    }
}
