using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void Walk(bool status)
    {
        anim.SetBool("Walking", status);
    }
    public void Death()
    {
        anim.SetTrigger("Death");
    }
    public void Jump()
    {
        anim.SetTrigger("Jump");
    }
    public void Falling(bool status)
    {
        anim.SetBool("Falling", status);
    }

}
