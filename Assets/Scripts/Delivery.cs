// using UnityEngine;
// using UnityEngine.InputSystem;
// using System.Collections;

// using TMPro;

// public class Delivery : MonoBehaviour
// {
//   public RoadSpawner roadSpawnerScript;
//   public bool hasBox = false;
//   [SerializeField] TMP_Text boxText;
//   [SerializeField] TMP_Text deliveryText;

// IEnumerator HideAfterSeconds(float seconds)
// {
//     yield return new WaitForSeconds(seconds);
//     boxText.gameObject.SetActive(false);
//     deliveryText.gameObject.SetActive(false);
// }
// void Start() {
//   boxText.gameObject.SetActive(false);
//   deliveryText.gameObject.SetActive(false);
// }
//   void OnTriggerEnter2D(Collider2D collision)
// {
//     if (collision.CompareTag("Package") && !hasBox)
//     {
//       hasBox = true;
//       GetComponent<ParticleSystem>().Play();
//       boxText.gameObject.SetActive(true);
//       StartCoroutine(HideAfterSeconds(2f));
//       Destroy(collision.gameObject); 
//     }
    
    
//     if(collision.CompareTag("Customer") && hasBox)
//     {
      
//       //Destroy(collision.gameObject); 
//       hasBox = false;
//       ScoreManager.instance.AddScore(150);
//       GetComponent<ParticleSystem>().Stop();
//       deliveryText.gameObject.SetActive(true);
//       StartCoroutine(HideAfterSeconds(2f));


//       if (roadSpawnerScript != null)
//        {
//           roadSpawnerScript.count = 0;
//        }
//     }
   

// }

// }
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class Delivery : MonoBehaviour
{
    public RoadSpawner roadSpawnerScript;
    public bool hasBox = false;

    [SerializeField] TMP_Text boxText;
    [SerializeField] TMP_Text deliveryText;

    IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        boxText.gameObject.SetActive(false);
        deliveryText.gameObject.SetActive(false);
    }

    void Start()
    {
        boxText.gameObject.SetActive(false);
        deliveryText.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Pickup box (destroy visually, carry virtually)
        if (collision.CompareTag("Package") && !hasBox)
        {
            hasBox = true;
            Destroy(collision.gameObject); // Box disappears
            GetComponent<ParticleSystem>().Play();
            boxText.gameObject.SetActive(true);
            StartCoroutine(HideAfterSeconds(2f));
        }

        // Deliver box to customer
        if (collision.CompareTag("Customer") && hasBox)
        {
            // Only declare customer once
            CustomerDestroy customer = collision.GetComponent<CustomerDestroy>();
            if (customer != null)
            {
                // Increment delivery count and destroy customer if max reached
                customer.ReceiveDelivery();
            }

            hasBox = false;
            ScoreManager.instance.AddScore(150);
            GetComponent<ParticleSystem>().Stop();
            deliveryText.gameObject.SetActive(true);
            StartCoroutine(HideAfterSeconds(2f));

            if (roadSpawnerScript != null)
            {
                roadSpawnerScript.count = 0;
            }
        }
    }
}