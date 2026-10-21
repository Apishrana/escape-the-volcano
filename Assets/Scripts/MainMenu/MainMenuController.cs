using UnityEngine;
using EasyUI.Toast;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLevel");
    }
    public void Options()
    {
        Toast.Show("You have no Options \n Free will isn't real \n  Just play the game", 2f);
        StartCoroutine(toast2());

    }
    IEnumerator toast2()
    {
        yield return new WaitForSeconds(2.5f);
        Toast.Show("If you don't wanna u will be forced to in about 2 sec", 1.5f);
        StartCoroutine(toast3());
    }
    IEnumerator toast3()
    {
        yield return new WaitForSeconds(2f);
        Toast.Show("Just kidding i wont do such a thing \n OR WILL I ;)", 3f);
        yield return new WaitForSeconds(3.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLevel");
    }
}
