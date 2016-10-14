using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public GlobalStateManager GlobalManager;

    public float maxSpeed = 3f;
    public bool canMove = true;
    public bool dead = false;



    [Range(1, 2)]
    public int playerNumber = 1;
    public string playerName;


    private Rigidbody2D rb2d;
    private Animator anim;
    private Text playerNameField;
    private Text playerNameLabel;

    public GameObject bomb;
    
    void Awake()
    {
        
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerNameField = GameObject.Find("Player" + playerNumber + "Name").GetComponent<Text>();
        playerNameLabel = GameObject.Find("Player" + playerNumber + "Label").GetComponent<Text>();

    }

    public void updatePlayerName()
    {
        if (playerNameField != null && System.String.IsNullOrEmpty(playerNameField.text))
        {
            playerName = "Player " + playerNumber;
            playerNameLabel.text = "Player " + playerNumber;
        }
        else
        {
            playerName = playerNameField.text;
            playerNameLabel.text = playerName;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStarted)
        {
            canMove = true;
            updatePlayerName();
        }
        else
            canMove = false;
        UpdateMovement();
    }

    void UpdateMovement()
    {
        if(!canMove)
        {   
            return;
        }

        if(playerNumber == 1)
        {
            updatePlayer1Movement();
        }
        else if(playerNumber == 2)
        {
            updatePlayer2Movement();    
        }
        
    }

    void updatePlayer1Movement()
    {
        float input_x = Input.GetAxisRaw("Horizontal_P1");
        float input_y = Input.GetAxisRaw("Vertical_P1");

        rb2d.velocity = new Vector2(input_x * maxSpeed, rb2d.velocity.y);
        rb2d.velocity = new Vector2(rb2d.velocity.x, input_y * maxSpeed);

        if (input_y == 1)
        {
            anim.SetBool("WalkingUp", true);
        }
        else
        {
            anim.SetBool("WalkingUp", false);
        }

        if (input_y == -1)
        {
            anim.SetBool("WalkingDown", true);
        }
        else
        {
            anim.SetBool("WalkingDown", false);
        }

        if (input_x == 1)
        {
            anim.SetBool("WalkingRight", true);
            
        }
        else
        {
            anim.SetBool("WalkingRight", false);
        }

        if (input_x == -1)
        {
            anim.SetBool("WalkingLeft", true);

        }
        else
        {
            anim.SetBool("WalkingLeft", false);
        }


        if (Input.GetButtonDown("Fire_P1"))
        {
            SetBomb();
        }
    }

    void updatePlayer2Movement()
    {
        float input_x = Input.GetAxisRaw("Horizontal_P2");
        float input_y = Input.GetAxisRaw("Vertical_P2");

        rb2d.velocity = new Vector2(input_x * maxSpeed, rb2d.velocity.y);
        rb2d.velocity = new Vector2(rb2d.velocity.x, input_y * maxSpeed);

        if (input_y == 1)
        {
            anim.SetBool("WalkingUp", true);
        }
        else
        {
            anim.SetBool("WalkingUp", false);
        }

        if (input_y == -1)
        {
            anim.SetBool("WalkingDown", true);
        }
        else
        {
            anim.SetBool("WalkingDown", false);
        }

        if (input_x == 1)
        {
            anim.SetBool("WalkingRight", true);

        }
        else
        {
            anim.SetBool("WalkingRight", false);
        }

        if (input_x == -1)
        {
            anim.SetBool("WalkingLeft", true);

        }
        else
        {
            anim.SetBool("WalkingLeft", false);
        }


        if (Input.GetButtonDown("Fire_P2"))
        {
            SetBomb();
        }
    }


    void SetBomb()
    {
        float x = rb2d.position.x;
        float y = rb2d.position.y;

        x = Mathf.Round(x);
        y = Mathf.Round(y);


        Instantiate(bomb, new Vector3(x, y, 0f), Quaternion.identity);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!dead && other.CompareTag("Flame"))
        {
            dead = true;
            GlobalManager.PlayerDied(playerNumber);
            Destroy(gameObject);
            
        }
    }

    public void EnableMovement(bool enable = true)
    {
        canMove = enable;
    }
}
