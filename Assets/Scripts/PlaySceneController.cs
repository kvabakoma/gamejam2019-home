using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneController : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject player;

    void Start() {
        SpawnEnemy ();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 2")) {
            RestartGame();
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene("GamePlay");
    }

    private void SpawnEnemy () {
        // Rigidbody rocketClone = (Rigidbody) Instantiate(rocket, transform.position, transform.rotation);
        float x = player.transform.position.x + 13f;
        Vector3 enemyPos = new Vector3 (x, Random.Range(4.5f,5.5f),-30f);
        GameObject newEnemy = Instantiate(enemyPrefabs[Random.Range (0, enemyPrefabs.Length)], enemyPos, Quaternion.identity);
        Invoke("SpawnEnemy", 2f);
    }
}
