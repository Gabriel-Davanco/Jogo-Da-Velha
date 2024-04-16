using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MENU_BUTTON : MonoBehaviour
{

    public void SAIR_JOGO()
    {
        dinamicaDoJogo.Reset();
        SceneManager.LoadScene("MENU");
    }
}
