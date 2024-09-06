using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public Popup popup;
    public TMP_Text txt;

    public void print(string str){
        popup.gameObject.SetActive(true);
        popup.pop();
        txt.text=str;
    }
    public void clear(){
        popup.close();
    }
}
