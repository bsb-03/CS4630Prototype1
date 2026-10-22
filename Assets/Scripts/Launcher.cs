using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private float launchSpeed = 15f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeliveryObject"))
        {
            Rigidbody rb = collision.rigidbody;

            if (rb != null)
            {
                // Clear existing motion so the launch is consistent
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // Launch along the launcher's local +Z
                rb.AddForce(transform.forward * launchSpeed, ForceMode.VelocityChange);
            }
        }
    }
}
