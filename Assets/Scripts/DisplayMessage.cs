using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayMessage : MonoBehaviour
{
    public Text uiSecondMessage;

    public IEnumerator Start()
    {
        GetComponent<Text>().text = "";
        yield return new WaitForSeconds(1);
        GetComponent<Text>().text = "Unstable Tower!";
        yield return new WaitForSeconds(4);
        GetComponent<Text>().text = "Good luck!";
        yield return new WaitForSeconds(3);
        GetComponent<Text>().text = "";
    }

    public void HasDied()
    {
        StopAllCoroutines();
        GetComponent<Text>().text = "Rest in pieces!";
        FindObjectOfType<DisplayCoins>().ChangeColor();
        StartCoroutine(DelayRestart());
    }

    private IEnumerator DelayRestart()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    public void UpdateSecondImage(int coins)
    {
        uiSecondMessage.GetComponent<Text>().text = (5 - (coins % 5)) + " coins to unlock one more extra jump!";
        if (coins % 5 == 0 && coins > 0)
        {
            FindObjectOfType<PlayerMovement>().maxJumps = Mathf.FloorToInt(coins / 5);
        }
    }

}
