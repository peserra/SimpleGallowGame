using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //INTERFACES DE LIGAÇÃO COM UNITY:

    public GameObject letra; //prefab
    public GameObject centro;

    //VARIAVEIS PARA PALAVRA:
    private string palavraOculta = ""; // palavra a ser descoberta
    char[] letrasOcultas;              // letras da palavra 1 a 1
    bool[] letrasDescobertas;          // indicador de quais letras foram descobertas

    //VARIAVEIS PARA CONTROLE DE SCORE:
    private int numTentativas;          // tentativas atuais
    private int maxTentativas;          // vida total 
    int score = 0;                      // pontuação


    void Start()
    {
        centro = GameObject.Find("centroDaTela");       //conecta o elemento centro da tela do unity na variavel com nome centro
        InitGame();
        InitLetras();
        numTentativas = 0;
        maxTentativas = 10;
        UpdateNumTentativas();
        UpdateScore();
    }


    void Update()
    {
        CheckTeclado();
    }

    void InitLetras()
    {
        int numeroLetras = palavraOculta.Length;        //numero de letras é o tamanho da palavra oculta
        for (int i = 0; i < numeroLetras; i++)
        {
            //cria um vetor no unity com o tamanho igual a quantiidade de letras, na posição centro:
            Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((i - numeroLetras / 2.0f) * 80), centro.transform.position.y, centro.transform.position.z); 
            
            //instancia gameobjects no unity, a serem colocados no vetor acima:
            GameObject l = (GameObject)Instantiate(letra, novaPosicao, Quaternion.identity);
            l.name = $"letra {i + 1}";
            l.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }

    //PREPARAÇÃO INICIAL DO JOGO
    void InitGame()
    {
        palavraOculta = PegaPalavraArquivo();                           // pego uma palavra direto do arquivo com a funcao
        palavraOculta = palavraOculta.ToUpper();                        // faz com que as letras sempre sejam caixa alta
        letrasOcultas = new char[palavraOculta.Length];                 // instancia vetor de caracteres do tamanho da palavra oculta
        letrasOcultas = palavraOculta.ToCharArray();                    // atribui letra a letra no vetor a palavra oculta
        letrasDescobertas = new bool[palavraOculta.Length];             // vetor que controla quais letras foram descobertas para exibir 
    }

    //CHECAGEM DE CADA INPUT DE LETRA DO USUÁRIO 
    void CheckTeclado()
    {
        if (Input.anyKeyDown)
        {
            char letraTeclada = Input.inputString.ToCharArray()[0];
            if (letraTeclada >= 'a' && letraTeclada <= 'z')
            {
                numTentativas++;
                UpdateNumTentativas();
                if (numTentativas > maxTentativas)
                {
                    SceneManager.LoadScene("Lab1_forca");              // condição de fim de jogo
                }
                for (int i = 0; i < palavraOculta.Length; i++)
                {
                    if (!letrasDescobertas[i])
                    {
                        letraTeclada = System.Char.ToUpper(letraTeclada);
                        if (letrasOcultas[i] == letraTeclada)
                        {
                            letrasDescobertas[i] = true;                //maneja quais letras foram encontradas na palavra até o momento
                            GameObject.Find($"letra {i + 1}").GetComponent<Text>().text = letraTeclada.ToString(); //substitui o texto padrao "_" pela letra correta
                            score = PlayerPrefs.GetInt("score");        //pega o valor da chave score armazenado no PlayerPrefs
                            score++;                                    //incrementa o score
                            PlayerPrefs.SetInt("score", score);         //aplica o novo valor do score na chave "score" em PlayerPrefs
                            UpdateScore();
                            VerificaDescoberta();
                        }
                    }
                }
            }
        }
    }

    //MÉTODOS DE EXIBIÇÃO DE TENTATIVAS E SCORE
    void UpdateNumTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = $"Tentativas: {numTentativas}|{maxTentativas}";
    }
    void UpdateScore() //mostra o valor do score na tela do unity
    {
        GameObject.Find("scoreUI").GetComponent<Text>().text = $"Score: {score}";
    }

    //MÉTODO QUE VERIFICA VITÓRIA
    void VerificaDescoberta()     
    {
        bool condicao = true;
        for (int i = 0; i < palavraOculta.Length; i++)
        {
            condicao = condicao && letrasDescobertas[i];                    //condicao só é true quando todas as letras forem true
        }

        if (condicao)
        {
            PlayerPrefs.SetString("ultimaPalavraOculta", palavraOculta);    //mostra a ultima palavra oculta  
            SceneManager.LoadScene("Lab1_salvo");
        }
    }

    //MÉTODO PARA PEGAR UMA PALAVRA ALEATÓRIA DENTRO DO BANCO DE PALAVRAS
    string PegaPalavraArquivo()
    {
        TextAsset t = (TextAsset)Resources.Load("palavras",typeof(TextAsset));      // leitura de arquivos
        string s = t.text;                                                          // coloca o conteudo carregado em uma string
        string []palavras = s.Split(' ');                                           // coloca a string num vetor, separando com o separador indicado "(' ')"
        int palavraAleatoria = Random.Range(0,palavras.Length+1);
        return (palavras[palavraAleatoria]);
    }
}
