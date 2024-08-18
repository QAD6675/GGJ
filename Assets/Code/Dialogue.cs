using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public Popup popup;

    void print(string txt){
        popup.pop();
        txt.text=txt;
    }
    void clear(){
        popup.close();
    }
}
