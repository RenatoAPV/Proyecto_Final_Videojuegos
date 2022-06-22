using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private float tiempo;

    public GameObject disparo;
    public Transform player;
    public Rigidbody2D rb;

    private float vidaJefe = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        tiempo += Time.deltaTime;
        if (tiempo >= 1)
        {
            var position = new Vector2(transform.position.x, transform.position.y);
            var rotation = disparo.transform.rotation;
            Instantiate(disparo, position, rotation);
            tiempo = 0;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "bola1")
        {
            vidaJefe -= 1;
            if (vidaJefe <= 0)
            {
                Destroy(this.gameObject);
            }            
        }
        if (tag == "bola2")
        {
            vidaJefe -= 2;
            if (vidaJefe <= 0)
            {
                Destroy(this.gameObject);

            }
        }
    }
}
