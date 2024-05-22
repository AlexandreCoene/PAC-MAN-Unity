using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadHomePage()
    {
        SceneManager.LoadScene("Home Page");
    }
}
