using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTrigger : MonoBehaviour
{

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if(player && player.horizontalMove != 0)
            {
                
                Vector3 playerPos = other.transform.position;
                playerPos.y = 0;
                Vector3 triggerPos = transform.position;
                triggerPos.y = 0;

                float distance = Vector3.Distance(triggerPos, playerPos);
                if(distance > 1)
                {
                    return;
                }


                float targetAngle = other.transform.rotation.eulerAngles.y + (player.horizontalMove > 0 ? -90.0f : 90.0f);

                //Debug.Log((triggerPos - playerPos).normalized);
                Debug.Log(distance);

                float angle = Mathf.LerpAngle(targetAngle, other.transform.rotation.eulerAngles.y, distance);
                if(targetAngle - angle <= targetAngle -  other.transform.rotation.eulerAngles.y)
                {
                    other.transform.RotateAround(other.transform.position, other.transform.up, angle);
                }

                //Debug.Log(Vector3.SignedAngle((triggerPos - playerPos).normalized, transform.forward, Vector3.up));
                

            }
        }
    }
}
