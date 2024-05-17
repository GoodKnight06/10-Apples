using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerScript : MonoBehaviour
{

    public float jumpForce;
    public float movementSpeed;
    public static int applesCollected;
    public TextMeshProUGUI displayApples;
    public GameObject gameOverScreen;
    public GameObject gameWonScreen;
    public TextMeshProUGUI gameWonScreenText;

    public static bool isGameOver;
    public static bool isGameWon;

    public AudioSource highJump;
    public AudioSource backgroundMusic;

    private int jumpsLeft;
    private int direction;
    private int state;

    // Start is called before the first frame update
    void Start()
    {
        jumpsLeft = 1;
        direction = 1;
        applesCollected = 0;
        gameObject.GetComponent<Animator>().SetInteger("PlayerMoveState", 3);
        state = 3;

        isGameOver = false;
        isGameWon = false;
        gameOverScreen.SetActive(false);
        gameWonScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        displayApples.text = applesCollected.ToString();
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            gameObject.GetComponent<Animator>().SetInteger("PlayerMoveState", 0);
            state = 0;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (direction == 1)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                direction = 0;
            }
            gameObject.GetComponent<Animator>().SetInteger("PlayerMoveState", 1);
            state = 1;
            transform.Translate(Vector2.left * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (direction == 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                direction = 1;
            }
            gameObject.GetComponent<Animator>().SetInteger("PlayerMoveState", 1);
            state = 1;
            transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            jumpsLeft--;
            highJump.Play();
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && jumpsLeft == 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -200));
        }
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            gameObject.GetComponent<Animator>().SetInteger("PlayerMoveState", 2);
            state = 2;
        }
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            gameObject.GetComponent<Animator>().SetInteger("PlayerMoveState", 3);
            state = 3;
        }

        if(transform.position.y <= -20 && state != 5)
        {
            isGameOver = true;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            gameObject.GetComponent<Animator>().SetInteger("PlayerMoveState", 5);
            state = 5;
            
        }

        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
        }
        if (isGameWon)
        {
            backgroundMusic.Pause();
            if (applesCollected == 10)
            {
                gameWonScreenText.text = "You are the chose one!";
            }
            else
            {
                gameWonScreenText.text = "GGWP Lucky shot... :')";
            }
            gameObject.SetActive(false);
            gameWonScreen.SetActive(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Terrain")
        {
            jumpsLeft = 1;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level 1");
    }
}
