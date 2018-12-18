using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {
    int nLevelPlaying = 0; //Id of the level that is being played

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            SceneManager.LoadScene("Level_" + nLevelPlaying.ToString());
        } else if (Input.GetKeyDown(KeyCode.Backspace)) {
            SceneManager.LoadScene("Level_Main_Test");
        }

    }
}
