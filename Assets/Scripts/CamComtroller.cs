using UnityEngine;

public class CamComtroller : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float minY;
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;
    [SerializeField, Range(0f, 1f)]
    private float parallaxAmount = 0.3f;
    private Transform BG;
    private Vector3 bgStartPosition;
    private Vector3 cameraStartPosition;

    void Start()
    {
        BG = transform.GetChild(0);
        bgStartPosition = BG.position;
        cameraStartPosition = transform.position;
    }

    void Update()
    {
        float x = Mathf.Clamp(player.position.x, minX, maxX);
        float y = Mathf.Clamp(player.position.y, minY, 1000);
        transform.position = new Vector3(x, y, -10);
        float cameraDeltaX = transform.position.x - cameraStartPosition.x;
        BG.position = new Vector3(
        bgStartPosition.x + cameraDeltaX * parallaxAmount,
        transform.position.y,
        bgStartPosition.z
    );
    }
}
