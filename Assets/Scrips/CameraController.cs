using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform jugador;
    void Start()
    {
           
    }
   
    // Update is called once per frame
    void Update()
    {
        var x = jugador.position.x + 2f;
        var y = jugador.position.y + 2f;
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
