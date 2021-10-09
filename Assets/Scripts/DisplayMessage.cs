using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayMessage : MonoBehaviour
{
    public Text uiSecondMessage;
    public Text extraJumpCounts;

    private int coinToExtraJump = 8;

    Text textComponent;

    public IEnumerator Start()
    {
        textComponent = GetComponent<Text>();
        textComponent.text = "";
        yield return new WaitForSeconds(1);
        textComponent.text = "Unstable Tower!";
        yield return new WaitForSeconds(4);
        textComponent.text = "Good luck!";
        yield return new WaitForSeconds(3);
        textComponent.text = "";

        extraJumpCounts.text = "2 Jumps";
    }

    public void HasDied()
    {
        StopAllCoroutines();
        textComponent.text = "Rest in pieces!";
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
        uiSecondMessage.GetComponent<Text>().text = (coinToExtraJump - (coins % (float)coinToExtraJump)) + " coins to unlock one more extra jump!";
        if (coins % coinToExtraJump == 0 && coins > 0)
        {
            //FindObjectOfType<PlayerMovement>().maxJumps = Mathf.CeilToInt((coins / (float)coinToExtraJump) + 1);
            //extraJumpCounts.text = Mathf.CeilToInt((coins / (float)coinToExtraJump) + 2) + " Jumps";
        }
    }

}
