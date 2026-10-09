using UnityEngine;

public class PowerUpIncreaseStamina : MonoBehaviour
{
    [SerializeField]
    private int IncreasePercent;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            pc.IncreaseStamina(IncreasePercent);
            Destroy(transform.parent.gameObject);
        }

    }
}