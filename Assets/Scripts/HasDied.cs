using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDied : MonoBehaviour
{
    public Rigidbody rigidBody;

    private bool hasDied;
    public ParticleSystem blood;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("cube") && !hasDied)
        {
            GetComponent<SoundManager>().PlaySound();

            hasDied = true;
            blood.Play();
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;

            FindObjectOfType<DisplayMessage>().HasDied();
        }
    }

    private void Update()
    {
        if(transform.position.y < -10 && !hasDied)
        {
            GetComponent<SoundManager>().PlaySound();
            hasDied = true;
            blood.Play();
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;

            FindObjectOfType<DisplayMessage>().HasDied();
        }
    }
}
