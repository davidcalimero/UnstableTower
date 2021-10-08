using UnityEngine;

public class RotationTrigger : MonoBehaviour
{

    private GameObject target;
    private float currentAngle;

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        PlayerMovement player = other.GetComponent<PlayerMovement>();

    //        print(player.horizontalMove);

    //        Vector3 playerPos = other.transform.position;
    //        playerPos.y = 0;
    //        Vector3 triggerPos = transform.position;
    //        triggerPos.y = 0;

    //        float targetAngle = (player.horizontalMove > 0 ? -90.0f : 90.0f);
    //        print(targetAngle);
    //        StartCoroutine(RotatePlayer(other.gameObject, targetAngle));

    //    }
    //}

    //private IEnumerator RotatePlayer(GameObject player, float targetRotation)
    //{
    //    float currentAngle = player.transform.eulerAngles.y;
    //    float elapsed = 0;
    //    while(elapsed < 1)
    //    {
    //        elapsed += Time.deltaTime / .2f;

    //        player.transform.eulerAngles = new Vector3(0, Mathf.Lerp(currentAngle, currentAngle + targetRotation, elapsed), 0);
    //        yield return null;
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;
            currentAngle = -Vector3.SignedAngle(transform.right, Vector3.right, Vector3.up);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            target = null;
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            PlayerMovement player = target.GetComponent<PlayerMovement>();
            float targetAngle = (player.horizontalMove > 0 ? -90.0f : 90.0f);

            Vector3 playerPos = target.transform.position;
            playerPos.y = 0;
            Vector3 triggerPos = transform.position;
            triggerPos.y = 0;

            Vector3 corner = triggerPos + transform.forward - transform.right;
            float distance = Vector3.Distance(corner, playerPos);

            float angle = (Mathf.Clamp(Vector3.SignedAngle(transform.right.normalized, (playerPos - corner).normalized, Vector3.up), 0, 90));
            player.transform.eulerAngles = new Vector3(0, currentAngle + angle, 0);

            Vector3 newPos = corner + (playerPos - corner).normalized;
            player.transform.position = new Vector3(newPos.x, player.transform.position.y, newPos.z);
        }
    }
}
