using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed;
    public float period = 6.0f;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Slider healthSlider;
    public Text gameStateText;


    private Color flashDMGColor = new Color(1f, 0f, 1f, 0.1f);
    private Color flashHPColor = new Color(0f, 1f, 0f, 0.1f);
    private float tickDMG = 0.1f;
    private int HPChange;
    private bool isDead;
    private float nextActionTime = 0.0f;
    private Rigidbody2D rb;
    private bool playerMovement = true;
    private GameObject gameController;
    
    private PlayerUtils playUtils = new PlayerUtils();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindWithTag("GameController");
    }

    void Update()
    {
        if (HPChange == 2)
        {
            damageImage.color = flashDMGColor;
        }
        else if (HPChange ==1) {
            damageImage.color = flashHPColor;
        }
        else
        {
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
            loseHP(tickDMG);
        }
    }

    private void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        gameStateText.text = "IT'S GAME OVER MAN, GAME OVER! PRESS R TO TRY AGAIN";

        // Turn off the movement
        playerMovement = false;

        // Death animation
        animateDeath();

        // Call gameController that blargh im ded
        gameController.SendMessage("PlayerDied");
    }

    // blarg im ded
    private void animateDeath() {
        transform.Rotate(0,0, -90);
    }

    public void loseHP(float amount)
    {
        playUtils.HPRemove(amount);
        HPChange = 2;
        healthSlider.value = playUtils.getHealth();
        if (playUtils.getHealth() <= 0 && !isDead)
        {
            Death();
        }
    }

    public void gainHP(float amount)
    {
        playUtils.HPGrant(amount);
        HPChange = 1;
        healthSlider.value = playUtils.getHealth();
    }
}
