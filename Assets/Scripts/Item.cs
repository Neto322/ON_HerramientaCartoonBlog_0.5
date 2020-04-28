﻿using System.Collections;
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
    InputField text;

    // Start is called before the first frame update


    public void ChecarValor()
    {
        if (DpScenes.value >= 6)
        {
            text.interactable = true;
        }
        else
        {
            text.interactable = false;

        }
    }
}
