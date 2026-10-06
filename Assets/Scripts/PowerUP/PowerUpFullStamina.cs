using UnityEngine;

public class PowerUpFullStamina : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            pc.FillStamina();
            Destroy(gameObject);
        }

    }
}