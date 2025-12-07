using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatScript : MonoBehaviour
{
    public Animator blackScreenAnim;
    public bool surviveBetweenScenes = false;

    private void Awake()
    {
        if (surviveBetweenScenes)
            DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            blackScreenAnim.SetTrigger("FloorEnd");
            StartCoroutine(Next());
        }

        if (surviveBetweenScenes)
            DontDestroyOnLoad(gameObject);

        blackScreenAnim = FindObjectOfType<Animator>();
    }

    private IEnumerator Next()
    {
        yield return new WaitForSeconds(0.97f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
