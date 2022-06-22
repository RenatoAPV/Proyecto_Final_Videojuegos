using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _renderer;
    private Animator _animator;
  
    
    public Text Vidas;
    public Text Monedas;
    
    public int vida = 8;
    public int monedas = 0;

    private string moneyData="";
    private string vidaData="";

    private int escalable = 0;
    
    
    public GameObject kunaiPrefabs;
    public GameObject bulletPrefabs2;
    
    public float velocity = 20;
    public float JumpForce = 10;

    public AudioClip[] audioClips;
        
    private static readonly int right = 1;
    private static readonly int left = -1;
        
         
    private static readonly int Animation_idle = 0;
    private static readonly int Animation_run = 1;
    private static readonly int Animation_jump = 2;
    private static readonly int Animation_escalera = 3;
    private static readonly int Animation_ataque = 4;
    private static readonly int Animation_paracaidas = 5;
    private static readonly int Animation_dead = 7;

    private int temporal = 0;
    private float time = 0f;
    private Puntajes Scors;

    private AudioSource audioSor;

    private void Awake()
    {
        loadData();
    }

    void Start()
    { 
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        Scors = FindObjectOfType<Puntajes>();
        audioSor = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (temporal == 1)
        {
	        audioSor.PlayOneShot(audioClips[5]);
            ChangeAnimation(Animation_paracaidas);
        }
        else
        {
            
            Vidas.text=""+vida;
            Monedas.text =""+ monedas;
           
            
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            ChangeAnimation(Animation_idle);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Desplazarse(right);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Desplazarse(left);
            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                ChangeAnimation(Animation_jump);
                _rigidbody2D.AddForce(Vector2.up*JumpForce,ForceMode2D.Impulse);
                audioSor.PlayOneShot(audioClips[0]);
            }
            if(Input.GetKeyUp(KeyCode.C))
            {
                ChangeAnimation(Animation_ataque);
                Disparar2();
                audioSor.PlayOneShot(audioClips[2]);
            }

            if (Input.GetKey(KeyCode.F))
            {
                audioSor.PlayOneShot(audioClips[5]);
                ChangeAnimation(Animation_paracaidas);
            }


            if (escalable == 1)
            {
                    Debug.Log(escalable);
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
			            audioSor.PlayOneShot(audioClips[4]);
                        ChangeAnimation(Animation_escalera);
                        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 5);
                    }
                    else
                    {
                        escalable = 0;
                    }
            }
            else
            {
                    escalable = 0;
                    Debug.Log(escalable);
            }
                
            
            if (Scors.miVida == 0)
            {
                audioSor.PlayOneShot(audioClips[1]);
                SceneManager.LoadScene("PrimerNivel");
            }

            timekey();
        }

       
    }
    void timekey()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            time = Time.time;

        }
         
        if(Input.GetKeyUp(KeyCode.X))
        {
            time = Time.time - time;
            Debug.Log("Pressed for : " + time + " Seconds");
            if (time >= 2f)
            {
                ChangeAnimation(Animation_ataque);
                audioSor.PlayOneShot(audioClips[2]);
                Disparar();
            }

        }
    }
    private void Desplazarse(int position)
    {
        _rigidbody2D.velocity = new Vector2(velocity * position, _rigidbody2D.velocity.y);
        _renderer.flipX = position == left;
        ChangeAnimation(Animation_run);
    }
    private void Disparar()
    {
        
        //crear elementos en tiempo de ejecuccion
        var x = this.transform.position.x;
        var y = this.transform.position.y;
       

        var bullgo=Instantiate(kunaiPrefabs,new Vector2(x,y),Quaternion.identity) as GameObject;
        var controller = bullgo.GetComponent<CucchilloController>();
        
        controller.SetController(this);
        
        if (_renderer.flipX)
        {
            
            controller.velocity = controller.velocity * -1;
        }
    }
    private void Disparar2()
    {
        //crear elementos en tiempo de ejecuccion
        var x = this.transform.position.x;
        var y = this.transform.position.y;

        var bullgo=Instantiate(bulletPrefabs2,new Vector2(x,y),Quaternion.identity) as GameObject;
        var controller = bullgo.GetComponent<CucchilloController>();
        
        controller.SetController(this);
        
        if (_renderer.flipX)
        {
            
            controller.velocity = controller.velocity * -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Pinchos")
        {
            SceneManager.LoadScene("PrimerNivel");            
        }
        if (tag == "Pinchos2")
        {
            SceneManager.LoadScene("SegundoNivel");            
        }
        if (tag == "Pinchos3")
        {
            SceneManager.LoadScene("TercerNivel");
        }
        if (tag == "Pinchos4")
        {
            SceneManager.LoadScene("NivelFinal");
        }
        if (tag == "Nivel1")
        {
            SceneManager.LoadScene("PrimerNivel");
        }
        if (tag == "Nivel2")
        {
            SceneManager.LoadScene("SegundoNivel");
        }
        if (tag == "Nivel3")
        {
            SceneManager.LoadScene("TercerNivel");
        }
        if (tag == "Nivel4")
        {
            SceneManager.LoadScene("NivelFinal");
        }
        if (tag == "moneda1")
        {
            monedas += 1;
            Destroy(other.gameObject);
            audioSor.PlayOneShot(audioClips[3]);
        }
        if (tag == "DisparoJefe")
        {
            Scors.MenosVida(3);
            vida -= 3;
            if (vida <= 0)
            {
                audioSor.PlayOneShot(audioClips[1]);
                ChangeAnimation(Animation_dead);
                SceneManager.LoadScene("PrimerNivel");
            }
        }
        if (tag == "Final")
        {
            SceneManager.LoadScene("Menus");
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        var tag = collision.gameObject.tag;
        if (tag == "Escalable")
        {
            escalable = 1;
        }
        if (tag == "activarParacaidas")
        {         
            temporal = 1;
        }     
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var tag = collision.gameObject.tag;
        
        if (tag == "DesactivarParacaidas")
        {            
            temporal = 0;
        }            
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var tag = other.gameObject.tag;
        if (tag=="Enemigo")
        {
            Scors.MenosVida(1);
            vida -= 1;
            if (vida <= 0)
            {
                audioSor.PlayOneShot(audioClips[1]);
                ChangeAnimation(Animation_dead);
                SceneManager.LoadScene("PrimerNivel");
            }
        }
        if (tag=="Enemigo2")
        {
            Scors.MenosVida(2);
            vida -= 2;
            if (vida <= 0)
            {
                audioSor.PlayOneShot(audioClips[1]);
                ChangeAnimation(Animation_dead);
                SceneManager.LoadScene("PrimerNivel");
            }
        }
        if(tag == "Jefe")
        {
            Scors.MenosVida(3);
            vida -= 3;
            if (vida <= 0)
            {
                audioSor.PlayOneShot(audioClips[1]);
                ChangeAnimation(Animation_dead);
                SceneManager.LoadScene("PrimerNivel");
            }
        }
        
    }
    

    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger("Estado",animation);
    }

    private void OnDestroy()
    {
        //saveData();
        loadData();
    }

    private void saveData()
    {
        PlayerPrefs.SetInt(moneyData,monedas);
        PlayerPrefs.SetInt(vidaData,vida);
    }

    private void loadData()
    {
        monedas = PlayerPrefs.GetInt(moneyData,0);
        vida=PlayerPrefs.GetInt(vidaData,8);
    }
}
