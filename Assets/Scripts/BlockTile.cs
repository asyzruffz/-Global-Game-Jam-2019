using UnityEngine;

public class BlockTile : MonoBehaviour {

    Block parent;
    
    public void SetParent (Block block) {
        parent = block;
    }

    void Update () {
        // Always upwards
        if (transform.rotation.eulerAngles.z != 0) {
            transform.rotation = Quaternion.identity;
        }
    }

    void OnTriggerEnter2D (Collider2D collision) {
        if (!collision.CompareTag ("Player")) {
            if (parent) {
                parent.SetOverlapped ();
            }
        }
    }
}
