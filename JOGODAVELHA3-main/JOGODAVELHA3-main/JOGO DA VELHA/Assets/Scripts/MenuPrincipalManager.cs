using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPrincipalManager : MonoBehaviour
{   // public para aparecerem no componente button
    [SerializeField] private string JogoDaVelha;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelDificuldade;
    [SerializeField] private GameObject painelSaida;

    public static int Dificuldade = 1;

    public void Jogar ()
    {
        SceneManager.LoadScene(JogoDaVelha);
    }

    public void AbrirDificuldade()
    {
        painelMenuInicial.SetActive(false);
        painelDificuldade.SetActive(true);
    }
    

    public void FecharDificuldade()
    {
        
        painelDificuldade.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void SairJogo()
    {
        painelMenuInicial.SetActive(false);
        painelSaida.SetActive(true);
        Application.Quit();
    }

    public void DificuldadeFacil()
    {
        dinamicaDoJogo.Dificuldade = 1;
        ResetarScores();
    }
    
    public void DificuldadeDificil()
    {
        dinamicaDoJogo.Dificuldade = 2;
        ResetarScores();
    }


    public void ResetarScores()
    {
        SCORE.PlayerScore = 0;
        SCORE.MachineScore = 0;
    }
}
