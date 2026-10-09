using UnityEngine;

public class PowerUpDoubleJump : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            pc.GiveDoubleJump();
            Destroy(transform.parent.gameObject);
        }

    }
}