using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
    public Player player;
    public int dialogueIndex;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "player") {
            player.triggerDialogue(dialogueIndex);
        }
    }
}
