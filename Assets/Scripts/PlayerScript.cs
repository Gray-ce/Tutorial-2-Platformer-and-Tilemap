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
    public GameObject winTextObject;
    private int scoreValue = 0;
    private int livesValue = 3;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score:" +  scoreValue.ToString();
        winTextObject.SetActive(false);
        lives.text = "Lives:" + livesValue.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        //float vertMovement = Input.GetAxis("Vertical"); nullified this code because the player kept moving upwards if the W key was held.

        rd2d.AddForce(new Vector2(hozMovement * speed, 0)); //, vertMovement * speed)); setting the vertical value to 0 constantly fixed this problem, as the player would now only be able to jump if touching the ground
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
         if (scoreValue >= 7)
        {
            winTextObject.SetActive(true);
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives:" + livesValue.ToString();
            Destroy(collision.collider.gameObject);
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