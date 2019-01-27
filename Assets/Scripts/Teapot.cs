using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teapot : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.layer == 13) {
            GetComponent<Animator>().SetTrigger("Break");
        }
    }
}
