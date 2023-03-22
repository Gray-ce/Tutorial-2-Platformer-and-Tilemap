using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text lives;
    public GameObject Player;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public AudioClip musicClipWin;
    public AudioClip musicClipOne;
    public AudioSource musicSource;
    private int scoreValue = 0;
    private int livesValue = 3;
    private bool facingRight = true;
    Animator anim;
    //private bool gameOver;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score:" +  scoreValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        lives.text = "Lives:" + livesValue.ToString();
        transform.position = new Vector2(-1.22f , 0.01f);
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        //gameOver = false;
    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        //float vertMovement = Input.GetAxis("Vertical"); nullified this code because the player kept moving upwards if the W key was held.

        rd2d.AddForce(new Vector2(hozMovement * speed, 0)); //, vertMovement * speed)); setting the vertical value to 0 constantly fixed this problem, as the player would now only be able to jump if touching the ground

          if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
             Flip();
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.D))
        {
          anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
          anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
          anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
          anim.SetInteger("State", 0);
        }
    }
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 7)
            {
            livesValue = 3;
            lives.text = "Lives:" + livesValue.ToString();
            transform.position  = new Vector2(38.8f , -0.28f);
            }
        }
        if (scoreValue == 12) // gameOver = false &&
        {
            //gameOver = true;
            winTextObject.SetActive(true);
            musicSource.clip = musicClipWin;
            musicSource.Play();
            musicSource.loop = false;

        }

        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives:" + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
    
        if (livesValue == 0)
        {
            loseTextObject.SetActive(true);
            Destroy(Player);
        }
    }
     private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}