using UnityEngine;

public class BlockTile : MonoBehaviour {

    Block parent;
    
    public void SetParent (Block block) {
        parent = block;
    }
    
    void OnTriggerEnter2D (Collider2D collision) {
        if (transform.parent != collision.transform.parent) { // Ignore tile within the same block
            if (parent) {
                //Debug.Log (transform.parent.gameObject.name + " x " + collision.transform.parent.gameObject.name + " (" + gameObject.name + ")");
                parent.SetOverlapped ();
            }
        }
    }
}
