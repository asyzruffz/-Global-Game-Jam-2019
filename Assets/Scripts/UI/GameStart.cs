using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

    [SerializeField]
    Dropdown levelSelect;
    
    public void StartGame () {
        SceneManager.LoadScene ("Level" + (levelSelect.value + 1));
    }
}
