using UnityEngine;

public class CustomerDestroy : MonoBehaviour
{
    public int deliveryCount = 0;
    public int maxDeliveries = 5;

    public void ReceiveDelivery()
    {
        deliveryCount++;
        Debug.Log("Delivered: " + deliveryCount);

        if (deliveryCount >= maxDeliveries)
        {
            Destroy(gameObject);
            Debug.Log("Customer removed after max deliveries");
        }
    }
}