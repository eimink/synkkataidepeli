using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed;
    public float period = 6.0f;
    public float health = 100.0f;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Slider healthSlider;
    public Text collectablesText;
    public int collectablesMax;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    private float tickDMG = 0.1f;
    private int HPChange;
    private bool isDead;
    private float nextActionTime = 0.0f;
    private Rigidbody2D rb;
    private bool playerMovement = true;
    private int collectablesCount;
    private GameObject gameController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = (GameObject)GameObject.FindGameObjectsWithTag("GameController")[0];
        collectablesCount = 0;
        collectablesMax = 6;
        setCollectables();
    }

    void Update()
    {
        // If the player has just been damaged...
        if (HPChange == 2)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        HPChange = 0;
    }

    void FixedUpdate()
    {
        if (playerMovement == true)
        { 
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
            rb.AddForce(movement * speed);
        }

        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            health = loseHP(tickDMG);
            // execute block of code here
        }
    }

    public float loseHP(float amount)
    {
        health -= amount;
        HPChange = 2;
        healthSlider.value = health;
        if (health <= 0 && !isDead)
        {
            Death();
        }
        return health;
    }

    public float gainHP(float amount)
    {
        health += amount;
        HPChange = 1;
        healthSlider.value = health;
        return health;

    }
    private void setCollectables()
    {
        collectablesText.text = "Collectables: " + collectablesCount.ToString() + "/" + collectablesMax.ToString();
    }
    public void collectedItem(int collected)
    {
        collectablesCount += collected;
        setCollectables();
    }

    private void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        
        // Turn off the movement
        playerMovement = false;
        
        // Call gameController that blargh im ded
        gameController.SendMessage("PlayerDied");
    }
}