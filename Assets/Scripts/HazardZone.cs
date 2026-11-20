using UnityEngine;

public class HazardZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeliveryObject"))
        {
            Debug.Log("Delivery Failed!");

            GameManager.Instance.LoseAttempt();

            DeliveryObject delivery = other.GetComponent<DeliveryObject>();
            if (delivery != null)
            {
                delivery.ResetDelivery();
            }
        }
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoseAttempt();

            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ResetPlayer();
            }
        }   
    }
}
