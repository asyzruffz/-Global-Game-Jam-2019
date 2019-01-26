using UnityEngine;

public class MovementController : MonoBehaviour {

    [SerializeField]
    float delay = 0.2f;

    public enum RotateType {
        Full,
        Half,
        None
    }

    [SerializeField]
    RotateType rotation;

    Block block;
    float moveDir;

    TimeSince ts;
    bool firstMove;
    float oldFallSpeed;
    Vector2 sideLimit;

    void Start () {
        block = GetComponent<Block> ();
        oldFallSpeed = block.Speed;
        ts = 0;
    }

    void Update () {
        if (GameController.Instance.IsGameOver) return;

        if (Input.GetButtonDown ("Horizontal")) {
            moveDir = Input.GetAxisRaw ("Horizontal");
            firstMove = true;
            ts = 0;
        }

        if (Input.GetButton ("Horizontal")) {
            MoveBlock ();
        } else {
            if (Input.GetButton ("Vertical") && Input.GetAxisRaw ("Vertical") < 0) {
                block.Speed = 20;
            } else {
                block.Speed = oldFallSpeed;
            }
        }

        if (Input.GetButtonDown ("Jump")) {
            RotateBlock ();
        }

        float correction = block.ExceedLimit (sideLimit);
        if (Mathf.Abs (correction) > 0) {
            transform.Translate (Vector3.right * correction, Space.World);
        }
    }

    void MoveBlock () {
        if (firstMove) {
            firstMove = false;
            transform.Translate (Vector3.right * moveDir, Space.World);
        } else if (ts > delay) {
            transform.Translate (Vector3.right * moveDir, Space.World);
            ts = 0;
        }
    }

    void RotateBlock () {
        switch (rotation) {
            default:
            case RotateType.None:
                break;
            case RotateType.Full:
                transform.Rotate (Vector3.forward * 90);
                break;
            case RotateType.Half:
                if (transform.rotation.eulerAngles.z > 0 && transform.rotation.eulerAngles.z < 91) {
                    transform.Rotate (Vector3.back * 90);
                } else {
                    transform.Rotate (Vector3.forward * 90);
                }
                break;
        }
    }

    public void SetSideLimit (Vector2 limit) {
        sideLimit = limit;
    }
}
