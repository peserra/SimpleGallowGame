using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostraUltimaPalavra : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetString("ultimaPalavraOculta"); //pega valor da chave "ultimaPalavraOculta" no PlayerPrefs
    }

    
}
