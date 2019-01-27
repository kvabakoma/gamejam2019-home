using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float health = 100, enemySpeed = 2f;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float direction = -1;
        if (transform.position.x < Player.transform.position.x) {
            direction = 1;
        }
        float x = transform.position.x + (enemySpeed * direction * Time.deltaTime);

        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.layer == 12) {
            health--;
            if (health<1) {
                Die();
            }
        } else if (other.transform.gameObject.layer == 9) {            
            Debug.Log(other.name);
            if (other.transform.gameObject.GetComponent<PlayerController>().playerIsAlive) {
                other.transform.gameObject.GetComponent<PlayerController>().Defeath();
            }
        }      
    }

    private void Die() {
        Destroy(gameObject);
    }
}
