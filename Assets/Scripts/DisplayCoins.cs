using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCoins : MonoBehaviour
{
    public int coinsNumber = 0;

    public void Start()
    {
        if (coinsNumber == 0) GetComponent<Text>().text = "";
    }

    public void AddCoin()
    {
        coinsNumber++;
        if(coinsNumber == 1)
        {
            GetComponent<Text>().text = "Collected 1 coin";
        }
        else
        {
            GetComponent<Text>().text = "Collected " + coinsNumber +" coins";
            FindObjectOfType<DisplayMessage>().UpdateSecondImage(coinsNumber);

        }
    }

    public void ChangeColor()
    {
        GetComponent<Text>().color = Color.green;
    }
}
