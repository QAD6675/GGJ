using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string TeleportToLevel;
    public bool locked=false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player=collision.gameObject.GetComponent<Player>();
            if(locked){
                if(player.hasKey){
                    SceneManager.LoadScene(TeleportToLevel);
                    return;
                }else{
                    player.triggerDialogue(6,6);///it says you need a key
                    return;
                }
            }else{
                SceneManager.LoadScene(TeleportToLevel);
            }
        }
    }
}
