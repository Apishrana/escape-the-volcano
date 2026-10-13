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
    [SerializeField]
    private TextMeshProUGUI GameOverLavaHeight;
    [SerializeField]
    private TextMeshProUGUI GameOverPlayerHeight;
    [SerializeField]
    private TextMeshProUGUI GameOverPlayTime;
    [SerializeField]
    private TextMeshProUGUI GameOverMaxLavaHeight;
    [SerializeField]
    private TextMeshProUGUI GameOverMaxPlayerHeight;
    [SerializeField]
    private TextMeshProUGUI GameOverMaxPlayTime;
    [SerializeField]
    private GameObject GameOverRect;
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
        PlayTime += Time.fixedDeltaTime;
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
        GameOverPlayTime.text = (Mathf.Round(PlayTime * 10) / 10).ToString() + " s";
        GameOverMaxPlayTime.text = (Mathf.Round(BestPlayTime * 10) / 10).ToString() + " s";
        GameOverPlayerHeight.text = (Mathf.Round(MaxPlayerHeight * 10) / 10).ToString() + " m";
        GameOverMaxPlayerHeight.text = (Mathf.Round(BestMaxPlayerHeight * 10) / 10).ToString() + " m";
        GameOverLavaHeight.text = (Mathf.Round(LavaHeight * 10) / 10).ToString() + " m";
        GameOverMaxLavaHeight.text = (Mathf.Round(BestLavaHeight * 10) / 10).ToString() + " m";
        PlayerPrefs.SetFloat("BestPlayTime", BestPlayTime);
        PlayerPrefs.SetFloat("BestMaxPlayerHeight", BestMaxPlayerHeight);
        PlayerPrefs.SetFloat("BestLavaHeight", BestLavaHeight);
        Destroy(Camera.main.GetComponent<CamComtroller>());
        GameOverRect.SetActive(true);
        Destroy(this);
    }
    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLevel");
    }
    public void Exit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
