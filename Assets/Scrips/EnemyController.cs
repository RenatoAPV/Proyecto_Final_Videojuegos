using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float Velocity = 10;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    
    private float videnemy = 3;
    private float videnemy2 = 6;


    private PlayerController _playerController;
    public void SetPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_sr.flipX == true)
        {
            _rb.velocity = new Vector2(Velocity * -1, _rb.velocity.y);
        }
        if (_sr.flipX == false)
        {
            _rb.velocity = new Vector2(Velocity * 1, _rb.velocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;

        if (tag == "Limite2")
        {
            _sr.flipX = true;
        }
        if (tag == "Limite")
        {
            _sr.flipX = false;
        }
        if (tag=="bola1")
        {
            videnemy -= 1;
            Debug.Log(videnemy);
            if (videnemy<=0)
            {
                Destroy(this.gameObject);
                
            }
            videnemy2 -= 1;
            Debug.Log(videnemy2);
            if (videnemy2<=0)
            {
                Destroy(this.gameObject);
            }
        }
        if (tag=="bola2")
        {
            videnemy -= 2;
            Debug.Log(videnemy);
            if (videnemy<=0)
            {
                Destroy(this.gameObject);
                
            }
            videnemy2 -= 2;
            Debug.Log(videnemy2);
            if (videnemy2<=0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
