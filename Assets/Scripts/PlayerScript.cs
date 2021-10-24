using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour

{

    public float speed;
    public Text score;
    public Text WinScore;
    public Text livesText;
    public Transform Destination;
    

    public AudioSource BackgroundMusic;
    public AudioSource WinSound;
    public float volume = 1.0f;

   
    private Rigidbody2D rd2d;
    private int scoreValue = 0;
    private int playerLives = 3;
    private int fakeScore = 0;
    private bool GameOver = false;
    
   

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text =  "Score" + scoreValue.ToString ();
        WinScore.text = "";
        livesText.text = " Lives: " + playerLives.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if(GameOver == false)
        {
            float hozMovement = Input.GetAxis("Horizontal");
            float verMovement = Input.GetAxis("Vertical");
        

            rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        }
        
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            fakeScore += 1;
            score.text = "Score:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            // Teleporation to Stage 2

             if (fakeScore == 4)
             {
                GameObject.FindWithTag("Player").transform.position = new Vector3(54.9f, 6.2f, 0.0f);
                playerLives = 3;
                livesText.text = "Lives: " + playerLives.ToString();
             }

        }


       

        if(collision.collider.tag == "Enemy")
        {
            playerLives -=1;
            score.text = "Score: " + scoreValue.ToString();
            livesText.text = "Lives : " + playerLives.ToString();
            Destroy(collision.collider.gameObject);
        }

        if(scoreValue == 10)
        {
            GameOver = true;
            if(scoreValue == 10 && GameOver == true)
            {
                BackgroundMusic.Stop();
                WinSound.Play();
            }
            
            WinScore.text = "Great Job! You WIN!!! Created by Kaitlin Duffey";
    
        }


        if (playerLives == 0)
        {
            WinScore.text = "Oops! You Lose! :( Game Created by Kaitlin Duffey";
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground") 
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0,4),ForceMode2D.Impulse);
            }
        }
    }

}
