using System.Collections;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private float LifeTime;
    [SerializeField] private GameObject self;
    void Start()
    {
        self.transform.SetPositionAndRotation(transform.position, transform.rotation);
        self.transform.localScale = transform.localScale;
        self.transform.parent = transform.parent;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Ground Check"))
        {
            StartCoroutine(StartDecay());
        }
    }
    IEnumerator StartDecay()
    {
        yield return new WaitForSeconds(LifeTime);
        BoxCollider2D bc = gameObject.GetComponent<BoxCollider2D>();
        bc.isTrigger = true;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(DestroyObj());
    }

    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(5f);
        // Instantiate(self);
        Destroy(gameObject);
    }
}
