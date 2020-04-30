using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Media;
using System.IO;
using Unity.Collections;
using TMPro;

public class Item : MonoBehaviour
{


    [SerializeField]
    Dropdown DpScenes;

    [SerializeField]
    Dropdown DpTransitions;

    [SerializeField]
    InputField inputfieldCuerpo;

    [SerializeField]
    InputField inputfieldCabecera;
    // Start is called before the first frame update

    public Vector2 posiciontextoCuerpo;

    public Vector2 textWidthHeightCuerpo;

    public Vector2 posiciontextoCabecera;

    public Vector2 textWidthHeightCabecera;

    void Start()
    {
        if (DpScenes.value >= 6)
        {
            inputfieldCuerpo.interactable = true;
            inputfieldCabecera.interactable = true;

        }
        else
        {
            inputfieldCuerpo.interactable = false;
            inputfieldCabecera.interactable = false;
        }
    }

    public void ChecarValor()
    {
        if (DpScenes.value >= 6)
        {
            inputfieldCuerpo.interactable = true;
            inputfieldCabecera.interactable = true;

        }
        else
        {
            inputfieldCuerpo.interactable = false;
            inputfieldCabecera.interactable = false;


        }

        switch (DpScenes.value)
        {
            case 6:
                posiciontextoCuerpo = new Vector2(12.2f, 4.71f);
                textWidthHeightCuerpo = new Vector2(19.3f, 5.36f);


                posiciontextoCabecera = new Vector2(8.29f, 6.28f);
                textWidthHeightCabecera = new Vector2(11f, 5.14f);

                break;
            case 7:
                posiciontextoCuerpo = new Vector2(7.47f, 3.44f);
                textWidthHeightCuerpo = new Vector2(30f, 6f);

                posiciontextoCabecera = new Vector2(3.06f, 5.86f);
                textWidthHeightCabecera = new Vector2(21.43f, 7f);
                break;
        }
    }
}
