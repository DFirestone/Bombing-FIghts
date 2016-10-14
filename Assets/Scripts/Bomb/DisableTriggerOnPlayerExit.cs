
using UnityEngine;
using System.Collections;

public class DisableTriggerOnPlayerExit : MonoBehaviour
{

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        { // When the player exits the trigger area
            GetComponent<Collider2D>().isTrigger = false; // Disable the trigger
        }
    }
}