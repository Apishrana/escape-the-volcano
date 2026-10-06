using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private float LifeTime;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Contains("Player"))
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
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
