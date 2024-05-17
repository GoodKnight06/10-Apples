using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectApple : MonoBehaviour
{
    private bool isCollected = false;

    public AudioSource eatApple;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !isCollected)
        {
            isCollected = true;
            eatApple.Play();
            PlayerScript.applesCollected++;
            gameObject.GetComponent<Animator>().SetBool("isCollected", true);
            Destroy(gameObject, Time.deltaTime*15);
        }
    }
}
