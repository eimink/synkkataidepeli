using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float nextActionTime = 0.0f;
    public float period = 6.0f;
    public float health = 100.0f;
    private float tickDMG = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        return health -= amount;
    }

    public float gainHP(float amount)
    {
        return health -= amount;
    }
}