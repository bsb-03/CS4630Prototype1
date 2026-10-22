using UnityEngine;

public class GoalZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeliveryObject"))
        {
            Debug.Log("Delivery Successful!");

            GameManager.Instance.AddPoint();
            Destroy(other.gameObject);
        }
    }
}
