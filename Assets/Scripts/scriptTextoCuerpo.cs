using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scriptTextoCuerpo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TextMeshPro displaytextCuerpo;

    Material mat;

    public float MaterialControl1;

    public bool MaterialControlBool;

    [SerializeField]
    public Animator anim; 
    
    void Start()
    {
        mat = GetComponent<Renderer>().material;


       

    }

    public void EsteticaDeTexto(int valorDp)
    {
        if(valorDp == 6)
        {
            Debug.Log("PUUUTTAAAA");

            displaytextCuerpo.alignment = TextAlignmentOptions.Left;


            displaytextCuerpo.color = new Color(0, 0, 255);

            displaytextCuerpo.alignment = TextAlignmentOptions.Left;



        }
    }
}
