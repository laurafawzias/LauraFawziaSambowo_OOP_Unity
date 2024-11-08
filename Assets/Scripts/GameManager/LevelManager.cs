using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (animator != null)
        {
            animator.gameObject.SetActive(false);
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (animator != null)
        {
            animator.gameObject.SetActive(true);
            animator.SetTrigger("In");
        }

        yield return new WaitForSeconds(1);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (Player.Instance != null)
        {
            Player.Instance.transform.position = new Vector3(0, -4.5f);
        }

        if (animator != null)
        {
            animator.SetTrigger("Out");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            animator.gameObject.SetActive(false);
        }
    }

    public void LoadScene(string Main)
    {
        StartCoroutine(LoadSceneAsync(Main));
    }
}
