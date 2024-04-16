using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class dinamicaDoJogo : MonoBehaviour
{
    public GameObject MachineAux;
    public static GameObject Machine;  //Peça da máquina

    public static int Dificuldade = 1;

    public static int Jogada;
    public static int Andamento;  //Conta o número de jogadas que foram necessárias para o jogador ou a máquina vencerem o jogo

    public static int[] po = new int[10];//Cria um array com as posições do tabuleiro

    public static int jogadaMaquinaEm1;

    public static int vitoria = 0;
    public static float[] posicao = new float[18];

    public static float contador = 0f;

    public static Boolean velha = false;

    void Start()
    {
        //-------------------------------------SALVA AS POSIÇÕES ORIGINAIS DOS SLOTS NO CENÁRIO-----------------
        // Os slots são removidos da visão da tela depois que escolhidos, mas ainda existem. aqui armazena suas posições originais.
        for (int p = 0; p < 18; p += 2)
        {

            posicao[p] = GameObject.Find(String.Concat("Slot", (p / 2) + 1)).transform.position.x;
            posicao[p + 1] = GameObject.Find(String.Concat("Slot", (p / 2) + 1)).transform.position.y;

        }
        //Randomiza a jogada da máquina caso não tenha um slot prioritário
        jogadaMaquinaEm1 = UnityEngine.Random.Range(1, 10);
        Machine = MachineAux;


        Jogada = UnityEngine.Random.Range(1, 3); //ESCOLHE O PRIMEIRO A COMEÇAR 
    }

    void Update()
    {

        if (Andamento >= 3)  //Chegou em um momento do jogo no qual é possível vercer ou perder
        {

            TESTECONFLITO();
            //Testa se a condição de vitória foi alcançada nas linhas, colunas e diagonais
            vitoria = Vit(po[3], po[2], po[1]) +
                    Vit(po[4], po[5], po[6]) +
                    Vit(po[7], po[8], po[9]) +
                    Vit(po[1], po[4], po[7]) +
                    Vit(po[2], po[5], po[8]) +
                    Vit(po[3], po[6], po[9]) +
                    Vit(po[1], po[5], po[9]) +
                    Vit(po[3], po[5], po[7]);
            if (vitoria == 0)//Testa se deu velha ou se o jogo ainda não terminou
            {
                velha = true;
                for (int y = 1; y < 10; y++)
                {

                    if (po[y] != 3 && po[y] != 2)
                    {
                        velha = false;
                        break;
                    }

                }

                if (velha)//Testa se deu velha
                {
                    Reset();
                    SCORE.MarcaVelha();

                }

            }
            else if (vitoria == 2 || vitoria == 4)//Testa se o jogador venceu
            {
                SCORE.MarcaPlayerScore();
                Reset();
            }
            else if (vitoria == 3 || vitoria == 6)//Testa se a máquina venceu
            {
                SCORE.MarcaMachineScore();
                Reset();
            }
        }

        if (Jogada == 2) //Vez da máquina
        {
            JOGADAMAQUINA();
        }

    }

    public static void Reset()
    {
        Thread.Sleep(1000);

        //Resetando as variáveis
        Andamento = 0;
        vitoria = 0;
        velha = false;
        Jogada = UnityEngine.Random.Range(1, 3);

        //-------------------------------------RETOMA AS POSIÇÕES ORIGINAIS DOS SLOTS NO CENÁRIO-----------------

        for (int p = 0; p < 18; p += 2)
        {

            GameObject.Find(String.Concat("Slot", (p / 2) + 1)).transform.position = new Vector3(posicao[p], posicao[p + 1], -4.451769f);

        }

        for (int i = 0; i < po.Length; i++)
        {
            po[i] = 0;
        }

        //Tirando as peças do tabuleiro
        GameObject[] pecasDoJogo = GameObject.FindGameObjectsWithTag("PecaDoJogo");
        for (int i = 0; i < pecasDoJogo.Length; i++)
        {
            Destroy(pecasDoJogo[i]);
        }

    }

    public static double Fil(double a, double b)//Filtro que compara se dois slots tem o mesmo valor
    {
        double i = 0;
        if (a == b)
        {
            i = a;
        }
        return i * 2;
    }

    public static int Vit(int a, int b, int c)//Verificar quem vence esta partida
    {
        int resp = 0;
        if (a == b && a == c)
        {
            resp = a;
        }
        return resp;
    }

    public static void TESTECONFLITO()//Calcula se algum slot é decisivo para a vitória ou derrota da máquina através de uma fórmula matemática
    {
        if (po[1] == 0)
        {
            po[1] = (int)(Fil(po[3], po[2]) +
            Fil(po[4], po[7]) +
            Fil(po[5], po[9]));
        }
        if (po[2] == 0)
        {
            po[2] = (int)(Fil(po[5], po[8]) +
                Fil(po[1], po[3]));
        }
        if (po[3] == 0)
        {

            po[3] = (int)(Fil(po[1], po[2]) +
                Fil(po[5], po[7]) +
                Fil(po[6], po[9]));
        }
        if (po[4] == 0)
        {
            po[4] = (int)(Fil(po[1], po[7]) +
                Fil(po[5], po[6]));
        }
        if (po[5] == 0)
        {
            po[5] = (int)(Fil(po[4], po[6]) +
                Fil(po[2], po[8]) +
                Fil(po[1], po[9]) +
                Fil(po[3], po[7]));
        }
        if (po[6] == 0)
        {
            po[6] = (int)(Fil(po[3], po[9]) +
                Fil(po[4], po[5]));
        }
        if (po[7] == 0)
        {
            po[7] = (int)(Fil(po[1], po[4]) +
                Fil(po[8], po[9]) +
                Fil(po[3], po[5]));
        }
        if (po[8] == 0)
        {
            po[8] = (int)(Fil(po[2], po[5]) +
                Fil(po[7], po[9]));
        }
        if (po[9] == 0)
        {
            po[9] = (int)(Fil(po[7], po[8]) +
                Fil(po[1], po[5]) +
                Fil(po[3], po[6]));
        }
    }

    public static void JOGADAMAQUINA()
    {
        Boolean conflito = false;
        for (int busc = 1; busc < 10; busc++) //Dar preferência à vitória
        {

            if (po[busc] == 6 || po[busc] == 12)//Aqui o conflito pode levar a vitória da máquina
            {
                Instantiate(Machine, new Vector3(GameObject.Find(String.Concat("Slot", busc)).transform.position.x, GameObject.Find(String.Concat("Slot", busc)).transform.position.y, GameObject.Find(String.Concat("Slot", busc)).transform.position.z), Quaternion.identity);
                //Insere a peça da máquina na cena
                po[busc] = 3;  //Insere a peça da máquina na MATRIZ

                GameObject.Find(String.Concat("Slot", busc)).transform.position = new Vector3(1000f, 0f, 0f);

                conflito = true;
                break;
            }

        }

        if (conflito == false)
        {
            for (int busc = 1; busc < 10; busc++) //Aqui a máquina faz uma jogada defensiva caso o conflito ainda não a leve a vitória, mas sim a vitória do jogador
            {
                if (po[busc] == 4 || po[busc] == 8)
                {
                    Instantiate(Machine, new Vector3(GameObject.Find(String.Concat("Slot", busc)).transform.position.x, GameObject.Find(String.Concat("Slot", busc)).transform.position.y, GameObject.Find(String.Concat("Slot", busc)).transform.position.z), Quaternion.identity);
                    //Insere a peça da máquina na cena
                    po[busc] = 3;  //Insere a peça da máquina na MATRIZ
                    GameObject.Find(String.Concat("Slot", busc)).transform.position = new Vector3(1000f, 0f, 0f);
                    conflito = true;
                    break;
                }

            }
        }

        if (Dificuldade == 2 && conflito == false)
        {

            if (po[5] == 0)
            {
                Instantiate(Machine, new Vector3(GameObject.Find("Slot5").transform.position.x, GameObject.Find("Slot5").transform.position.y, GameObject.Find("Slot5").transform.position.z), Quaternion.identity); ;
                //Insere a peça da máquina na cena
                GameObject.Find("Slot5").transform.position = new Vector3(1000f, 0f, 0f);
                po[5] = 3;  //Insere a peça da máquina na MATRIZ
            }
            else if (po[1] == 0)
            {
                Instantiate(Machine, new Vector3(GameObject.Find("Slot1").transform.position.x, GameObject.Find("Slot1").transform.position.y, GameObject.Find("Slot1").transform.position.z), Quaternion.identity);
                //Insere a peça da máquina na cena
                GameObject.Find("Slot1").transform.position = new Vector3(1000f, 0f, 0f);
                po[1] = 3;  //Insere a peça da máquina na MATRIZ
            }
            else if (po[3] == 0)
            {
                Instantiate(Machine, new Vector3(GameObject.Find("Slot3").transform.position.x, GameObject.Find("Slot3").transform.position.y, GameObject.Find("Slot3").transform.position.z), Quaternion.identity);
                //Insere a peça da máquina na cena
                GameObject.Find("Slot3").transform.position = new Vector3(1000f, 0f, 0f);
                po[3] = 3;  //Insere a peça da máquina na MATRIZ
            }
            else if (po[7] == 0)
            {
                Instantiate(Machine, new Vector3(GameObject.Find("Slot7").transform.position.x, GameObject.Find("Slot7").transform.position.y, GameObject.Find("Slot7").transform.position.z), Quaternion.identity);
                //Insere a peça da máquina na cena
                GameObject.Find("Slot7").transform.position = new Vector3(1000f, 0f, 0f);
                po[7] = 3;  //Insere a peça da máquina na MATRIZ
            }
            else if (po[9] == 0)
            {
                Instantiate(Machine, new Vector3(GameObject.Find("Slot9").transform.position.x, GameObject.Find("Slot9").transform.position.y, GameObject.Find("Slot9").transform.position.z), Quaternion.identity);
                //Insere a peça da máquina na cena
                GameObject.Find("Slot9").transform.position = new Vector3(1000f, 0f, 0f);
                po[9] = 3;  //Insere a peça da máquina na MATRIZ
            }
            else  //Dificuldade Baixa
            {
                while (po[jogadaMaquinaEm1] != 0 && velha == false)
                {
                    jogadaMaquinaEm1 = UnityEngine.Random.Range(1, 10);//A máquina escolhe aleatóriamente na dificuldade baixa
                }
                Instantiate(Machine, new Vector3(GameObject.Find(String.Concat("Slot", jogadaMaquinaEm1)).transform.position.x, GameObject.Find(String.Concat("Slot", jogadaMaquinaEm1)).transform.position.y, GameObject.Find(String.Concat("Slot", jogadaMaquinaEm1)).transform.position.z), Quaternion.identity);
                //Insere a peça da máquina na cena
                GameObject.Find(String.Concat("Slot", jogadaMaquinaEm1)).transform.position = new Vector3(1000f, 0f, 0f);
                po[jogadaMaquinaEm1] = 3;  //Insere a peça da máquina na MATRIZ
            }

        }
        if (Dificuldade == 1 && conflito == false)  //Dificuldade Baixa
        {
            while (po[jogadaMaquinaEm1] != 0)
            {
                jogadaMaquinaEm1 = UnityEngine.Random.Range(1, 10);
            }

            Instantiate(Machine, new Vector3(GameObject.Find(String.Concat("Slot", jogadaMaquinaEm1)).transform.position.x, GameObject.Find(String.Concat("Slot", jogadaMaquinaEm1)).transform.position.y, GameObject.Find(String.Concat("Slot", jogadaMaquinaEm1)).transform.position.z), Quaternion.identity);
            //Insere a peça da máquina na cena
            GameObject.Find(String.Concat("Slot", jogadaMaquinaEm1)).transform.position = new Vector3(1000f, 0f, 0f);
            po[jogadaMaquinaEm1] = 3;  //Insere a peça da máquina na MATRIZ
        }
        Andamento++;//Dá prosseguinmento ao próximo turno
        Jogada = 1;


    }
}