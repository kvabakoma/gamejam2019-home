using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneController : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject player, enemiesContainer;
    public float monsterOffsetX = 20f, monsterSpawnRate = 8f;

    private int enemyCount = 0;

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
        Vector3 enemyPos = new Vector3();
        enemyCount++;
        // Debug.Log("enemyCount: " + enemyCount + " | " + enemyCount%3);
        if (enemyCount % 2 == 0) {
            enemyPos = new Vector3 (player.transform.position.x - monsterOffsetX * Random.Range(.8f, 1f), Random.Range(.8f, 1.2f), -30f);
            GameObject newEnemy1 = Instantiate(enemyPrefabs[Random.Range (0, enemyPrefabs.Length)], enemyPos, Quaternion.identity);
            if (newEnemy1.GetComponent<EnemyController>().EnemyId == 3) {
                newEnemy1.transform.position = new Vector3(newEnemy1.transform.position.x, 10f, newEnemy1.transform.position.z);
            }
            newEnemy1.transform.localScale *= Random.Range(.8f, 1.1f);
            newEnemy1.transform.parent = enemiesContainer.transform;
        }
        enemyPos = new Vector3 (player.transform.position.x + monsterOffsetX, -1f, -30f);
        GameObject newEnemy = Instantiate(enemyPrefabs[Random.Range (0, enemyPrefabs.Length)], enemyPos, Quaternion.identity);
        if (newEnemy.GetComponent<EnemyController>().EnemyId == 3) {
            newEnemy.transform.position = new Vector3(newEnemy.transform.position.x, 10f, newEnemy.transform.position.z);
        }
        newEnemy.transform.parent = enemiesContainer.transform;
        Invoke("SpawnEnemy", monsterSpawnRate);
    }
}
