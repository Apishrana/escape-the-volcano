using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerController Player;
    [SerializeField]
    private TextMeshProUGUI PlayerHeightText;
    [SerializeField]
    private TextMeshProUGUI LavaHeightText;
    [SerializeField]
    private int LavaHeight;
    [SerializeField]
    private Slider LavaBar;
    [SerializeField]
    private float LavaRiseTime;
    [SerializeField]
    private Transform LavaObject;
    private float LavaTime;
    private float LavaTimePercent = 100f;

    void Start()
    {
        LavaTime = LavaRiseTime;
        LavaBar.interactable = false;
        LavaObject.position = new Vector3(0, LavaHeight - 4.5f);

    }

    void Update()
    {
        LavaBar.value = 100 - LavaTimePercent;
        if (LavaTimePercent < 0)
        {
            RaseLava();
        }
        PlayerHeightText.text = ((int)Mathf.Round(Player.transform.position.y)).ToString() + " m";
        LavaHeightText.text = LavaHeight.ToString() + " m";
    }
    void FixedUpdate()
    {
        LavaTime -= Time.fixedDeltaTime;
        LavaTimePercent = LavaTime / LavaRiseTime * 100;
    }
    void RaseLava()
    {
        LavaTime = LavaRiseTime;
        LavaTimePercent = 100f;
        LavaHeight++;
        LavaObject.position = new Vector3(0, LavaHeight - 4.5f);
    }
}
