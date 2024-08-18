using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]private TextAsset dialoguesFile;
    [SerializeField]private Dialogue dialogue;
    String[] dialogues;
    // Start is called before the first frame update
    void Start()
    {
        string content=dialoguesFile.text;
        dialogues = content.Split("/");
    }
    public void saySomething(int index){
        dialogue.print(dialogues[index]);
    }
    public void clearDialogue(){
        dialogue.clear();
    }

}
