using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneController : MonoBehaviour
{
    void Update() {
        if (Input.GetKeyDown("space") || Input.GetKeyDown("joystick button 2")) {
            RestartGame();
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene("GamePlay");
    }
}
