using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyThis : MonoBehaviour
{      
   GameObject[] objetoMusica; 
    //MÉTODO PARA FAZER MUSICA DO MENU TOCAR INDEFINIDAMENTE NO MENU E NOS CRÉDITOS
    private void Awake()
    {
        objetoMusica = GameObject.FindGameObjectsWithTag("MenuMusic");                    
        if(objetoMusica.Length >1)
        {
            Destroy(this.gameObject);          //impede que duas musicas toquem em conjunto, evita sobreposição de musicas quando volta pro menu
        }
        DontDestroyOnLoad(this.gameObject);    //impede que musica do menu pare quando troca de cena    
        
    }

    //MÉTODO PARA PARAR A MUSICA DO MENU QUANDO O JOGO COMEÇA
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Lab1")
        {
            Destroy(this.gameObject);
        }
    }

    
}
