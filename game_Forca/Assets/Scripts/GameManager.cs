using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //interfaces de ligação
    public GameObject letra; //prefab
    public GameObject centro;

    private string palavraOculta = ""; // palavra a ser descoberta
    char[] letrasOcultas;              // letras da palavra 1 a 1
    bool[] letrasDescobertas;          // indicador de quais letras foram descobertas


    void Start()
    {
        centro = GameObject.Find("centroDaTela");
        InitGame();
        InitLetras();
    }


    void Update()
    {
        CheckTeclado();
    }

    void InitLetras()
    {
        int numeroLetras = palavraOculta.Length;
        for (int i = 0; i < numeroLetras; i++)
        {
            Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((i - numeroLetras / 2.0f) * 80), centro.transform.position.y, centro.transform.position.z);
            GameObject l = (GameObject)Instantiate(letra, novaPosicao, Quaternion.identity);
            l.name = $"letra {i + 1}";
            l.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }

    void InitGame()
    {
        palavraOculta = "Elefante";
        palavraOculta = palavraOculta.ToUpper();
        letrasOcultas = new char[palavraOculta.Length];
        letrasOcultas = palavraOculta.ToCharArray();
        letrasDescobertas = new bool [palavraOculta.Length];
    }

    void CheckTeclado()
    {
        if(Input.anyKeyDown)
        {
            char letraTeclada = Input.inputString.ToCharArray()[0];
            if(letraTeclada >= 'a' && letraTeclada <= 'z')
            {
                for (int i = 0; i < palavraOculta.Length; i++)
                {
                   if(!letrasDescobertas[i])
                   {
                       letraTeclada = System.Char.ToUpper(letraTeclada);
                       if(letrasOcultas[i] == letraTeclada)
                       {
                           letrasDescobertas[i] = true;
                           GameObject.Find($"letra {i + 1}").GetComponent<Text>().text = letraTeclada.ToString();
                       }
                   } 
                }
            }
        }
    }
}
