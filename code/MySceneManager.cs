using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public void OnClickPlay()
    {
        SceneManager.LoadScene("FirstScene");
    } 
}
