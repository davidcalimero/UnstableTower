using System.Runtime.InteropServices;
using UnityEngine;

public class GameStuff : MonoBehaviour
{
    public static float initialtime = 0;
    public float heightUntilStart = 3.0f;
    public static bool start = false;
    public PlayerMovement player;

    public GameObject leftJoyStick;
    public GameObject jumpButton;


    private float timePassed = 0;

    void Awake()
    {
        initialtime = 0;
        start = false;

        leftJoyStick.SetActive(IsMobilePlatform());
        jumpButton.SetActive(IsMobilePlatform());
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if(!start && player.m_Grounded && player.gameObject.transform.position.y >= heightUntilStart)
        {
            initialtime = Time.realtimeSinceStartup;
            start = true;
        }
    }

    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public bool IsMobilePlatform()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobile();
        #endif
        return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }
}