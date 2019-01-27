using UnityEngine;

public class BackToExit : MonoBehaviour {
    
    void Update () {
        if (Input.GetButtonUp("Cancel")) {
            Application.Quit ();
        }
    }

}
