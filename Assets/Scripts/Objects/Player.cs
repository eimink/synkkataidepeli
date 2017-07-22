using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed;
    public float period = 6.0f;
    public Image damageImage;
    public Slider healthSlider;
    public Text gameStateText;

    private float flashSpeed = 5f;
    private Vector3 movement;
    private Color flashDMGColor = new Color(1f, 0f, 1f, 0.1f);
    private Color flashHPColor = new Color(0f, 1f, 0f, 0.1f);
    private float tickDMG = 0.1f;
    private int HPChange;
    private bool isDead;
    private float nextActionTime = 0.0f;
    private Rigidbody2D rb;
    private bool playerMovement = true;
    private GameObject gameController;
    private float x = 0.0f;
    private float y = 0.0f;
    private float dragSensitivity = 2;
    private SpriteRenderer spriteRenderer;
    private bool lookRight = true;

    private PlayerUtils playUtils = new PlayerUtils();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = GameObject.FindWithTag("GameController");
		damageImage = GameObject.Find("DamageImage").GetComponent<Image>();
		healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
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
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Touch f0 = Input.GetTouch(0);
                Vector3 f0Delta2 = new Vector3(f0.deltaPosition.x, -f0.deltaPosition.y, 0);
                x += Mathf.Deg2Rad * f0Delta2.x * 10;
                y += Mathf.Deg2Rad * f0Delta2.y * 10;
                moveHorizontal = x;
                moveVertical = y;
            }
#endif
            Move(moveHorizontal, moveVertical);
            tickHandler();
        }
    }

    private void tickHandler()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            loseHP(tickDMG);
        }
    }

    private void Move(float h, float v)
    {
        if (h > 0 && lookRight)
        {
            lookRight = false;
        }
        else if(h < 0 && !lookRight)
        {
            lookRight = true;
        }
        flipVision();
        movement.Set(h, v, 0f);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    private void flipVision() {
        spriteRenderer.flipX = lookRight;
    }

    private void Death()
    {
        isDead = true;
        gameStateText.text = "IT'S GAME OVER MAN, GAME OVER! PRESS R TO TRY AGAIN";
        playerMovement = false;
        animateDeath();
        sendGameEvent("PlayerDied");
    }
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
    public void setPlayerMovement(bool value)
    {
        playerMovement = value;
    }
    public void gainHP(float amount)
    {
        playUtils.HPGrant(amount);
        HPChange = 1;
        healthSlider.value = playUtils.getHealth();
    }

    public void sendGameEvent(string eventName) {
        gameController.SendMessage(eventName);
    }
}
