using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]private TextAsset dialoguesFile;
    [SerializeField]private TMP_Text textDisplay;
    String[] dialogues;
    // Start is called before the first frame update
    void Start()
    {
        string content=dialoguesFile.text;
        dialogues = content.Split("/");
        textDisplay.enabled=false;
        // textDisplay.text =dialogues[0];
    }
    public void saySomething(int index){
        textDisplay.enabled=true;
        textDisplay.text =dialogues[index];
    }

}
