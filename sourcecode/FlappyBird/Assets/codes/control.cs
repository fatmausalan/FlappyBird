using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class control : MonoBehaviour
{
    public Sprite[] birdSprites;
    SpriteRenderer spriteRenderer;
    bool nextforwardcontrol = true;
    int birdCount = 0;
    float birdAnimationTime;
    int score = 0;
    public Text scoreText;
    bool gameOver = true;
    gamecontrol gamecontrol;
    AudioSource[] sesler;
    int highScore = 0;
    Rigidbody2D phy;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        phy = GetComponent<Rigidbody2D>();
        gamecontrol = GameObject.FindGameObjectWithTag("gamecontroltag").GetComponent<gamecontrol>();
        sesler = GetComponents<AudioSource>();
        highScore = PlayerPrefs.GetInt("highScore");
    }

    // Update is called once per frame
    void Update()
    {
        birdMovement();
        myAnimation();
              
    }
    void birdMovement()
    {
        if (Input.GetMouseButtonDown(0) && gameOver)
        {
            phy.velocity = new Vector2(0, 0);
            phy.AddForce(new Vector2(0, 200));
            sesler[0].Play();
        }
        if (phy.velocity.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 30);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -30);
        }
    }
    void myAnimation()
    {
        birdAnimationTime += Time.deltaTime;
        if (birdAnimationTime > 0.2f )
        {
            birdAnimationTime = 0;
            if (nextforwardcontrol)
            {
                spriteRenderer.sprite = birdSprites[birdCount];
                birdCount++;
                if (birdCount == birdSprites.Length)
                {
                    birdCount--;
                    nextforwardcontrol = false;
                }
            }
            else
            {
                birdCount--;
                spriteRenderer.sprite = birdSprites[birdCount];
                if (birdCount == 0)
                {
                    birdCount++;
                    nextforwardcontrol = true;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "pointtag")
        {
            score++;
            scoreText.text = score + "";
            sesler[1].Play();
        }
        if(collision.gameObject.tag == "engeltag")
        {
            gameOver = false;
            gamecontrol.gameOver();
            sesler[2].Play();
            GetComponent<CircleCollider2D>().enabled = false;
            if(score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("highScore", highScore);
            }
            PlayerPrefs.SetInt("score", score);
            Invoke("returnMenu", 2);
        }
    }
    void returnMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
}
