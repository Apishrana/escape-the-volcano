using UnityEngine;

public class PowerUpDoubleJump : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            // Debug.Log('s');
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            pc.GiveDoubleJump();
            Destroy(gameObject);
        }

    }
}