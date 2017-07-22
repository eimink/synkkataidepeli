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
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    private float tickDMG = 0.1f;
    private int HPChange;
    private float nextActionTime = 0.0f;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        rb.AddForce(movement * speed);

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
        return health;


    }

    public float gainHP(float amount)
    {
        health += amount;
        HPChange = 1;
        healthSlider.value = health;
        return health;

    }
}