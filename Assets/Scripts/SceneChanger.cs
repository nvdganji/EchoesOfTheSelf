using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Tooltip("The name of the scene you want to load")]
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🚪 Entered scene trigger. Loading: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
