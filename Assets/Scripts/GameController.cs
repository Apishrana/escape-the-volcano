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
    private float PlayTime = 0;
    private float MaxPlayerHeight;

    void Start()
    {
        LavaTime = LavaRiseTime;
        LavaBar.interactable = false;
        LavaObject.position = new Vector3(0, LavaHeight - 4.5f);

    }

    void LateUpdate()
    {
        LavaBar.value = 100 - LavaTimePercent;
        if (LavaTimePercent < 0)
        {
            RaseLava();
        }
        PlayerHeightText.text = ((int)Mathf.Round(Player.transform.position.y)).ToString() + " m";
        LavaHeightText.text = LavaHeight.ToString() + " m";
        MaxPlayerHeight = Mathf.Max(MaxPlayerHeight, Player.transform.position.y);
    }
    void FixedUpdate()
    {
        LavaTime -= Time.fixedDeltaTime;
        LavaTimePercent = LavaTime / LavaRiseTime * 100;
        PlayTime += Time.fixedDeltaTime / 10;
    }
    void RaseLava()
    {
        LavaTime = LavaRiseTime;
        LavaTimePercent = 100f;
        LavaHeight++;
        LavaObject.position = new Vector3(0, LavaHeight - 4.5f);
    }
    public void GameOver()
    {
        float BestPlayTime = Mathf.Max(PlayerPrefs.GetFloat("BestPlayTime"), PlayTime);
        float BestMaxPlayerHeight = Mathf.Max(PlayerPrefs.GetFloat("BestMaxPlayerHeight"), MaxPlayerHeight);
        float BestLavaHeight = Mathf.Max(PlayerPrefs.GetFloat("BestLavaHeight"), LavaHeight);
        print(PlayTime);
        print(BestPlayTime);
        print(MaxPlayerHeight);
        print(BestMaxPlayerHeight);
        print(LavaHeight);
        print(BestLavaHeight);
        PlayerPrefs.SetFloat("BestPlayTime", BestPlayTime);
        PlayerPrefs.SetFloat("BestMaxPlayerHeight", BestMaxPlayerHeight);
        PlayerPrefs.SetFloat("BestLavaHeight", BestLavaHeight);
        Destroy(Camera.main.GetComponent<CamComtroller>());
        Destroy(this);
    }
}
