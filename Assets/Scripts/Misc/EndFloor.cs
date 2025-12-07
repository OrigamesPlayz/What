using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndFloor : MonoBehaviour
{
    public Animator screenAnim;
    public GameObject quitButton;
    public PlayerCam playerCam;
    private CheatScript cheatScript;
    private StopWatch sw;

    int nextIndex;
    bool hasNextScene;

    private void Start()
    {
        cheatScript = FindObjectOfType<CheatScript>();
        sw = FindObjectOfType<StopWatch>();

        nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        hasNextScene = nextIndex < SceneManager.sceneCountInBuildSettings;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerObj"))
        {
            screenAnim.SetTrigger("FloorEnd");
            sw.stopwatchActive = false;

            if (hasNextScene)
                StartCoroutine(End());
            else
                StartCoroutine(EndGame());
        }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(0.97f);
        SceneManager.LoadScene(nextIndex);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0.97f);

        if (quitButton != null)
        {
            yield return new WaitForSeconds(1f);
            playerCam.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            if (cheatScript != null)
            {
                cheatScript.enabled = false;
            }

            quitButton.SetActive(true);
        }
    }
}
