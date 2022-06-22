using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntajes : MonoBehaviour
{
    public Text Monedas;
    public Text Vida;

    private int MonedasTotales = 0;
    public int miVida = 5;

    void Start()
    {
        Monedas.text = "" + MonedasTotales;
        Vida.text = "x" + miVida;
    }

    /*Monedas*/
    public int getScoreMonedas()
    {
        return this.MonedasTotales;
    }
    public void SumMonedas(int score)
    {
        this.MonedasTotales += score;
        Monedas.text = "" + MonedasTotales;
    }

    /*Vidas*/
    public int getScoreVidas()
    {
        return this.miVida;
    }
    public void MenosVida(int score)
    {
        this.miVida -= score;
        Vida.text = "x" + miVida;
    }


}
