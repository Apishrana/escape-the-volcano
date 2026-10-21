using UnityEngine;
using EasyUI.Toast;

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
            Toast.Show("Stamina Increased", 2f, HexColor("#000000a0"));
            Destroy(transform.parent.gameObject);
        }

    }
    Color HexColor(string hex)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}