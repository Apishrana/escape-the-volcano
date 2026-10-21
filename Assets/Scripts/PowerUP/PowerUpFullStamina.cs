using UnityEngine;
using EasyUI.Toast;

public class PowerUpFullStamina : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            pc.FillStamina();
            Toast.Show("Stamina Refilled", 2f, HexColor("#000000a0"));
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