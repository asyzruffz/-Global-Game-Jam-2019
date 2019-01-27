using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField]
    float speed = 1;

    [SerializeField]
    Transform[] tiles;

    [SerializeField]
    Sprite[] sprites;

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
    Vector3 lastPos;
    bool isOverlapped;
    bool byPlayer;
    bool onGround;

    void Start () {
        ts = 0;
        Speed = speed;

        int randSprite = Random.Range (0, sprites.Length);
        
        for (int i = 0; i < tiles.Length; i++) {
            tiles[i].GetComponent<BlockTile> ().SetParent (this);
            tiles[i].GetComponent<SpriteRenderer> ().sprite = sprites[randSprite];
        }
    }

    void Update () {
        if (GameController.Instance.IsGameOver) return;

        if (!onGround) {
            Fall ();
        }

        // Hack to fix block pass into the ground
        if (transform.position.y <= -2) {
            isOverlapped = true;
        }
    }

    void LateUpdate () {
        if (GameController.Instance.IsGameOver) return;

        CheckGround ();
    }

    void Fall () {
        if (ts > delay) {
            PrepareToMove ();
            transform.Translate (Vector3.down, Space.World);
            MomentaryHide ();
            ts = 0;
        }
    }

    void CheckGround () {
        if (isOverlapped && !onGround) {
            isOverlapped = false;

            if (byPlayer) {
                // Move back to resolve collision
                transform.position = lastPos;
            } else {
                // Move back up to resolve collision
                transform.Translate (Vector3.up, Space.World);

                if (level) {
                    SetBlocksToLevel ();
                    level.SettleBlock ();
                }
                onGround = true;
            }
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

    public Level GetLevel () {
        return level;
    }

    void SetVisible (bool visible) {
        for (int i = 0; i < tiles.Length; i++) {
            tiles[i].GetComponent<SpriteRenderer> ().enabled = visible;
        }
    }

    public void MomentaryHide () {
        StartCoroutine (DelayVisibility ());
    }

    IEnumerator DelayVisibility () {
        SetVisible (false);
        yield return null;
        SetVisible (true);
    }

    public void PrepareToMove (bool byOther = false) {
        // Store the position before moving and who's moving it
        lastPos = transform.position;
        byPlayer = byOther;
    }
}
