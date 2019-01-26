using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        if (other.transform.gameObject.layer == 12) {
            health--;
            if (health<1) {
                Die();
            }
        } else if (other.transform.gameObject.layer == 9) {
            other.transform.gameObject.GetComponent<PlayerController>().Die();
        }      
    }

    private void Die() {
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }
}
