using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPlayer : MonoBehaviour
{
    public Animator blackScreenAnim;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerObj"))
        {
            blackScreenAnim.SetTrigger("FloorEnd");
            StartCoroutine(Respawn());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            blackScreenAnim.SetTrigger("FloorEnd");
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.97f);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex, LoadSceneMode.Single);
    }
}
