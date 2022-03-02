using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

{
    public GameObject letra;                            // prefab da letra no jogo
    public GameObject centro;                           // objeto de texto que indica o centro da tela
    public GameObject localErradas;

    private string palavraOculta = "";                  // palavra a ser descoberta
    private string[] palavrasOcultas = new string[] { "carro", "casa", "batata", "palha", "elefante" };
    char[] letrasOcultas;                               // letras da palavra oculta
    bool[] letrasDescobertas;                           // indica quais letras foram descobertas
    int numErradas = 6;
    int contadorErradas = 1;

    // Start is called before the first frame update
    void Start()
    {
        centro = GameObject.Find("centroDaTela");       //busca o gameobject com o nome especificado e atribui à variável
        localErradas = GameObject.Find("botton");
        InitGame();
        InitLetras();

    }

    // Update is called once per frame
    void Update()
    {
        CheckTeclado();

    }

    void InitLetras()
    {
        int numLetras = palavraOculta.Length;
        for (int i = 0; i < numLetras; i++)
        {
            //para fazer as letras se deslocarem na direção de x no jogo :
            Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((i - numLetras / 2.0f) * 80), centro.transform.position.y, centro.transform.position.z);

            //copias do game object "letra" : (pra que serve isso?)
            GameObject l = (GameObject)Instantiate(letra, novaPosicao, Quaternion.identity);
            l.name = "letra " + (i + 1);        //nomeia na hierarquia cada GameObject letra -> para encontrar o elemento especifico depois
            l.transform.SetParent(GameObject.Find("Canvas").transform); //coloca as letras como filhas do canvas
        }


        for (int j = 0; j < numErradas; j++)
        {
            Vector3 posicaoErradas = new Vector3(localErradas.transform.position.x + (j * 80), localErradas.transform.position.y, localErradas.transform.position.z);

            GameObject e = (GameObject)Instantiate(letra, posicaoErradas, Quaternion.identity);
            e.name = "letraErrada" + (j + 1);
            e.transform.SetParent(GameObject.Find("Canvas").transform);
        }

    }

    //metodo que inicializa o jogo:
    void InitGame()
    {
        //palavraOculta = "batata";                                     // define a palavra oculta (usado como teste apenas)
        int numeroAleatorio = Random.Range(0, palavrasOcultas.Length);  // cria um numero aleatorio entre 0 e tamanho do vetor de palavras ocultas
        palavraOculta = palavrasOcultas[numeroAleatorio];               // atribui aleatoriamente uma palavra para "palavraOculta"
        palavraOculta = palavraOculta.ToUpper();                        // coloca todas as letras em maiuscula
        letrasOcultas = new char[palavraOculta.Length];                 // instancia o vetor de chars
        letrasDescobertas = new bool[palavraOculta.Length];             // instancia o vetor bool
        letrasOcultas = palavraOculta.ToCharArray();                    // coloca letra a letra no vetor de chars 
    }

    //funcao para ler entrada do teclado:
    void CheckTeclado()
    {
        if (Input.anyKeyDown)
        {
            char letraTeclada = Input.inputString.ToCharArray()[0]; //oq essa linha ta fazendo ?


            if (letraTeclada >= 'a' || letraTeclada <= 'z') //checa se foi apertado algo entre a e z no ASCII
            {

                for (int i = 0; i <= palavraOculta.Length; i++) //checa no tamanho da palavra oculta
                {

                    if (!letrasDescobertas[i])
                    {
                        letraTeclada = System.Char.ToUpper(letraTeclada); 


                        if (letrasOcultas[i] == letraTeclada) //se a letra teclada é igual a letra na posição i da palavra (transformada em vetor)
                        {
                            letrasDescobertas[i] = true;    //descobrimos uma letra
                            GameObject.Find($"letra {(i + 1)}").GetComponent<Text>().text = letraTeclada.ToString(); //transforma o texto do componente na letra que colocamos
                            Debug.Log($"{letraTeclada} atribuida");
                        }
                        
                        
                    }


                }
                
                
                
               
            }

        }

    }


}
