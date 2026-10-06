using UnityEngine;

public class MoveText : MonoBehaviour
{
    public float MovePercent;
    public float TimePeriod;
    private Vector3 Pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.Sin(Time.time * Mathf.PI * 2f / TimePeriod) * MovePercent;
        transform.position = Pos + new Vector3(0, y);
    }
}
