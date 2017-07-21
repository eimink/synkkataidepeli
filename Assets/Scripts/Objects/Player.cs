using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    GameObject o;
    private Rigidbody2D rb;
    public float speed;

    void Start()
    {
        o = (GameObject)this.gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }
}