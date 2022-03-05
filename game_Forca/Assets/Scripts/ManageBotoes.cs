using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageBotoes : MonoBehaviour
{

    /*
        Essa classe controla os botoes, quando ativada, o score é zerado e a cena "Lab1" é carregada (que é o jogo)
    */
    void Start()
    {
        PlayerPrefs.SetInt("score", 0);
    }     

    //MÉTODOS PARA APLICAR NOS BOTOES
    public void StartMundoGame()                        //método para mudar a cena para o jogo principal
    {
        SceneManager.LoadScene("Lab1");
    }

    public void AcessaCreditos()                        //método para mudar a cena para os créditos
    {
        SceneManager.LoadScene("Lab1_credito");
    }

    public void AcessaStart()                           //método para mudar a cena para start 
    {
        SceneManager.LoadScene("Lab1_start");
    }

    
}
