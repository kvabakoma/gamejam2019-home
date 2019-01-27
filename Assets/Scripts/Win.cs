using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject WinScreen;
    private GameObject Player;

    void Awake() {
        Player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == 9) {
            ShowWinScreen();
        }
    }

    private void ShowWinScreen() {
        Player.GetComponent<PlayerController>().playerIsAlive = false;
        WinScreen.SetActive(true);
    }

}
