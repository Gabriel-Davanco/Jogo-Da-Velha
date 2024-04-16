using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SCORE : MonoBehaviour
{
    public static int PlayerScore = 0;
    public static int MachineScore = 0;

    private static TMP_Text pontuaPlayer;
    private static TMP_Text pontuaMachine;
    // Start is called before the first frame update
    void Start()
    {
        pontuaPlayer = GameObject.Find("TextoEsq").GetComponent<TMP_Text>();
        pontuaMachine = GameObject.Find("TextoDir").GetComponent<TMP_Text>();

        
    }


    public static void MarcaVelha()
    {
        pontuaPlayer.color = new Color(1f, 1f, 1f);
        pontuaMachine.color = new Color(1f, 1f, 1f);
    }

    public static void MarcaPlayerScore()
    {
        PlayerScore++;
        pontuaPlayer.text = PlayerScore.ToString();
        pontuaPlayer.color = new Color(0f, 1f, 0.041f);
        pontuaMachine.color = new Color(1f, 1f, 1f);
    }
    public static void MarcaMachineScore()
    {
        MachineScore++;
        pontuaMachine.text = MachineScore.ToString();
        pontuaMachine.color = new Color(0f, 1f, 0.041f);
        pontuaPlayer.color = new Color(1f, 1f, 1f);
    }
}
