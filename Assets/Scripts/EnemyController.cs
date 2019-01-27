using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float health = 100, healthPerLightSession = 50, huntSpeed, runSpeed, attackSpeed, currentEnemySpeed, runAwayTime;
    public enum EnemyStateEnum
    {
        HUNT, INLIGHT, RUN, ATTACK, DEAD
    }
    public EnemyStateEnum enemyState;
    
    private float lightAttackStartHealth;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyStateEnum.HUNT;
        Player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
        case EnemyStateEnum.HUNT:            
            Hunt();
            break;
        case EnemyStateEnum.INLIGHT:
            EnemyInLight();
            break;
        case EnemyStateEnum.RUN:
            Run();
            break;
        case EnemyStateEnum.ATTACK:
            Attack();
            break;
        case EnemyStateEnum.DEAD:
            Debug.Log("DEAD STATE");
            currentEnemySpeed = 0f;
            break;
        default:
            break;
        }

        Move();
        
        Flip();
    }


    void Hunt() {
        print ("HUNT STATE");
        currentEnemySpeed = huntSpeed;
        if (health < healthPerLightSession && ((transform.localScale.x < 0 && Player.transform.localScale.x < 0) || (transform.localScale.x > 0 && Player.transform.localScale.x > 0))) {
            enemyState = EnemyStateEnum.ATTACK;
        }
    }

    void Run() {
        print ("RUN STATE");
        currentEnemySpeed = runSpeed;
    }

    void EnemyInLight() {
        print ("INLIGHT STATE");
        currentEnemySpeed = 0f;
    }

    void Attack() {
        print ("ATTACK STATE");
        currentEnemySpeed = attackSpeed;
    }

    void Move() {

        float direction = -1;
        if (transform.position.x < Player.transform.position.x) {
            direction = 1;
        }

        float x = transform.position.x + (currentEnemySpeed * direction * Time.deltaTime);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    void Flip() {
        if (Player.transform.position.x > transform.position.x && transform.localScale.x > 0) {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        if (Player.transform.position.x < transform.position.x && transform.localScale.x < 0) {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.layer == 12) {   // the enemy is in light

            if (enemyState == EnemyStateEnum.HUNT || enemyState == EnemyStateEnum.ATTACK) {
                enemyState = EnemyStateEnum.INLIGHT;
                lightAttackStartHealth = health;
                print("start health: " + lightAttackStartHealth);
            }

            health--;
            print("health: " + health);

            if (lightAttackStartHealth - health >= healthPerLightSession) {
                enemyState = EnemyStateEnum.RUN;
                Invoke("StopRunning", runAwayTime);
            }
            
            if (health<1) {
                enemyState = EnemyStateEnum.DEAD;
                Die();
            }
        } else if (other.transform.gameObject.layer == 9) {
            enemyState = EnemyStateEnum.HUNT;            
            Debug.Log(other.name);
            if (other.transform.gameObject.GetComponent<PlayerController>().playerIsAlive) {    // enemy caught player
                other.transform.gameObject.GetComponent<PlayerController>().Defeath();
            }
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.layer == 12) {
            if (enemyState == EnemyStateEnum.INLIGHT) enemyState = EnemyStateEnum.HUNT; 
        }
    }

    private void StopRunning() {
        if (enemyState == EnemyStateEnum.RUN) {
            enemyState = EnemyStateEnum.HUNT;
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}
