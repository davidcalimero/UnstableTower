using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchCoin : MonoBehaviour
{
    private bool hastriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hastriggered)
        {
            hastriggered = true;
            GetComponentInChildren<Animation>().Play("coinCatch");
            FindObjectOfType<DisplayCoins>().AddCoin();
            GetComponent<SoundManager>().PlaySound();
        }
    }

}
