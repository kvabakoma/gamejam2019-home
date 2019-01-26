using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneController : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject player;
    public float monsterOffsetX = 20f;

    void Start() {
        SpawnEnemy ();
    }

    void Update() {
        if (!player.GetComponent<PlayerController>().playerIsAlive && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 2"))) {
            RestartGame();
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene("GamePlay");
    }

    private void SpawnEnemy () {
        Vector3 enemyPos = new Vector3 (player.transform.position.x + monsterOffsetX, Random.Range(4.5f,5.5f),-30f);
        GameObject newEnemy = Instantiate(enemyPrefabs[Random.Range (0, enemyPrefabs.Length)], enemyPos, Quaternion.identity);
        Invoke("SpawnEnemy", 4f);
    }
}
