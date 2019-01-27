using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {
    
    [SerializeField]
    int nextLevel;

    public void NextGame () {
        if (nextLevel > 0) {
            SceneManager.LoadScene ("Level" + nextLevel);
        } else {
            SceneManager.LoadScene ("MainMenu");
        }
    }

    public void ReloadLevel () {
        Scene currentScene = SceneManager.GetActiveScene ();
        SceneManager.LoadScene (currentScene.name);
    }

    void Update () {
        if (Input.GetButtonUp ("Cancel")) {
            SceneManager.LoadScene ("MainMenu");
        }
    }
}
