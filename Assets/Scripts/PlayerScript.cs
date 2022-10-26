using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public TextMeshProUGUI score;
    public TextMeshProUGUI life;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject bonusTextObject;
    private int scoreValue = 0;
    private int lifeValue;
    private bool bonus;
    private bool facingRight = true;
    public AudioClip BGM;
    public AudioClip winBGM;
    public AudioSource musicSource;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        lifeValue = 5;
        bonus = false;

        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        life.text = "Lives: " + lifeValue.ToString();
        
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        bonusTextObject.SetActive(false);
        
        musicSource.clip = BGM;
        musicSource.Play();
        musicSource.loop = true;
    }

    // Update is called once per frame

    void FixedUpdate()
    {

        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.W))
        {
          anim.SetInteger("State", 2);
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
          anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
          anim.SetInteger("State", 3);
        }

        else if (Input.GetKeyUp(KeyCode.W) == true && vertMovement == 0)
            {
                anim.SetInteger("State", 0);
            }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
          anim.SetInteger("State", 3);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
          anim.SetInteger("State", 2);  
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
          anim.SetInteger("State", 3);
        }

        else if (Input.GetKeyUp(KeyCode.UpArrow) == true && vertMovement == 0)
            {
                anim.SetInteger("State", 0);
            }

        if (Input.GetKeyDown(KeyCode.A))
        {
          anim.SetInteger("State", 1);
        }

        else if (Input.GetKeyDown(KeyCode.A) == true && vertMovement < 0)
            {
                anim.SetInteger("State", 3);
            }

        if (Input.GetKeyUp(KeyCode.A))
        {
          anim.SetInteger("State", 0);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
          anim.SetInteger("State", 1);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) == true && vertMovement < 0)
            {
                anim.SetInteger("State", 3);
            }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
          anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
          anim.SetInteger("State", 1);
        }

        else if (Input.GetKeyDown(KeyCode.D) == true && vertMovement < 0)
            {
                anim.SetInteger("State", 3);
            }

        if (Input.GetKeyUp(KeyCode.D))
        {
          anim.SetInteger("State", 0);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
          anim.SetInteger("State", 1);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) == true && vertMovement < 0)
            {
                anim.SetInteger("State", 3);
            }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
          anim.SetInteger("State", 0);
        }
        
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    void Flip()
        {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
        }

     private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue >= 15)
            {
            winTextObject.SetActive(true);
            musicSource.clip = winBGM;
            musicSource.Play();
            speed = 0;
            anim.SetInteger("State", 0);
            }
             
            else if (scoreValue == 6)
            {
            transform.position = new Vector3(55.8f, 0.0f, 0.0f);
            bonusTextObject.SetActive(false);
            }
        }

        if (collision.collider.tag == "Bonus")
        {
            bonus = true;
            bonusTextObject.SetActive(true);
            lifeValue = lifeValue + 1;
            life.text = "Lives: " + lifeValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        else if (collision.collider.tag == "Enemy")
        {
            lifeValue = lifeValue - 1;
            life.text = "Lives: " + lifeValue.ToString();
            Destroy(collision.collider.gameObject);

            if (lifeValue == 0)
            {
            loseTextObject.SetActive(true);
            anim.SetInteger("State", 0);
            Physics.gravity = new Vector3(0, -10.0F, 0);
            speed = 0;
            }
        }
 }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                // the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.S))
            {
                rd2d.AddForce(new Vector2(0, -2), ForceMode2D.Impulse);
                // the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                rd2d.AddForce(new Vector2(0, -2), ForceMode2D.Impulse);
            }
        }
    }

}
