 using UnityEngine;
 using System.Collections;
 using UnityEngine.UI;

public class FPSCounter : MonoBehaviour 
{
    public Text label;
	float deltaTime = 0;
	
	IEnumerator Start ()
	{
        yield return new WaitForSeconds (1.0f);
		while (true) {
			label.text = ((int)(1.0f / deltaTime)).ToString();
			yield return new WaitForSeconds (2.0f);
		}
	}

    void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}
}
