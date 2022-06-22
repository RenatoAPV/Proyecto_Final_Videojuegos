using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoController : MonoBehaviour
{
    public float velocityX = 30f;

    private Rigidbody2D rb;
    private

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2);
    }

    void Update()
    {
        rb.velocity = Vector2.right * -velocityX;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            rb.velocity = Vector2.right * velocityX;
        }
    }
}
