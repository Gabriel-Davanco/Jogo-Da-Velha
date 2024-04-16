using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SLOTNUM : MonoBehaviour
{
    public GameObject Jogador;

    // Start is called before the first frame update

    void OnMouseDown()
    {

        if (dinamicaDoJogo.Jogada == 1)
        {
            for (int u = 0; u < 10; u++)
            {
                if (this.gameObject.name == String.Concat("Slot", u)) //Slot clicado  resgatado
                {
                    dinamicaDoJogo.po[u] = 2;  //A matriz do script dinamicaDoJogo recebe a pea do jogador na respectiva posio clicada
                    Instantiate(Jogador, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);

                    //Coloca a pe�a CUBO na posio clicada
                    dinamicaDoJogo.Jogada = 2;
                    dinamicaDoJogo.Andamento++;
                    this.gameObject.transform.position = new Vector3(1000f, 0f, 0f);

                }
            }

        }

    }
}
