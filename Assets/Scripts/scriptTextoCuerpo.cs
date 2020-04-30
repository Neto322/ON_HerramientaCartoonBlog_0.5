using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scriptTextoCuerpo : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshPro displaytextCuerpo;

    Material mat;

    public float MaterialControl1;

    public bool MaterialControlBool;

    [SerializeField]
    public Animator anim; 
    
    void Start()
    {
        gameObject.SetActive(false);
        displaytextCuerpo.enabled = false;
        displaytextCuerpo = GetComponent<TextMeshPro>();
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    public void EsteticaDeTexto(int valorDp)
    {
        if(valorDp == 6)
        {
            displaytextCuerpo.color = new Color(0, 72, 255);

            displaytextCuerpo.alignment = TextAlignmentOptions.Left;

            gameObject.SetActive(MaterialControlBool);

            mat.SetFloat(ShaderUtilities.ID_OutlineWidth, MaterialControl1);

            mat.SetFloat(ShaderUtilities.ID_UnderlaySoftness , MaterialControl1);


        }
    }
}
