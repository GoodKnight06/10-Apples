using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyEnd : MonoBehaviour
{

    private bool isTouched = false;

    public AudioSource youWon;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && !isTouched)
        {
            youWon.Play();
            gameObject.GetComponent<Animator>().SetBool("isTouched", true);
            isTouched = true;
            PlayerScript.isGameWon = true;
        }
    }
}
