using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void GoToScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
