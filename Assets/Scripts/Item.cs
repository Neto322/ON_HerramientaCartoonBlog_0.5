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
                posiciontextoCuerpo = new Vector2(12f, 2f);
                textWidthHeightCuerpo = new Vector2(19f, 5.5f);
                posiciontextoCabecera = new Vector2(11.05f, 2.3f);
                textWidthHeightCabecera = new Vector2(18.39f, 5.5f);
                break;
            case 7:
                posiciontextoCuerpo = new Vector2(7.43f, 2.04f);
                textWidthHeightCuerpo = new Vector2(30f, 5.34f);
                break;
        }
    }
}
