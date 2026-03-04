using UnityEngine;
using UnityEngine.InputSystem;

public class Driver : MonoBehaviour
{
    [Header("Movement")]
    public float rotation = 60f;
    public float regularSpeed = 5f;
    public float boostSpeed = 10f;

    [Header("Boost & HP")]
    public bool hasBoost = false;
    public float hpPotionCount = 0;
    public BoostSpawner boostSpawnerScript;
    public PlayerHealth playerHealth; // Drag your PlayerHealth component here

    void Update()
    {
        float move = 0f;
        float steer = 0f;

        if (Keyboard.current.wKey.isPressed) move = 1f;
        if (Keyboard.current.sKey.isPressed) move = -1f;
        if (Keyboard.current.aKey.isPressed) steer = 2.5f;
        if (Keyboard.current.dKey.isPressed) steer = -2.5f;

        // Always use currentSpeed from PlayerHealth
        transform.Translate(0, move * playerHealth.currentSpeed * Time.deltaTime, 0);
        transform.Rotate(0, 0, steer * rotation * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boost"))
        {
            hasBoost = true;
            Destroy(collision.gameObject);
            playerHealth.currentSpeed = boostSpeed;
        }

        if (collision.CompareTag("HpPotion") && playerHealth.currentHealth < playerHealth.maxHealth)
        {
            Destroy(collision.gameObject);
            playerHealth.Heal(20);
            hpPotionCount -= 1;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        playerHealth.TakeDamage(5);
        playerHealth.currentSpeed = regularSpeed;

        if (collision.gameObject.CompareTag("Wall"))
        {
            hasBoost = false;
            if (boostSpawnerScript != null)
                boostSpawnerScript.countBoostItem = 0;
        }
    }
}