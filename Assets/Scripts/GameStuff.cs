using UnityEngine;

public class GameStuff : MonoBehaviour
{
    public static float initialtime = 0;
    public float heightUntilStart = 3.0f;
    public static bool start = false;
    public static PlayerMovement player;
    private float timePassed = 0;

    void Awake()
    {
        initialtime = 0;
        start = false;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
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
}