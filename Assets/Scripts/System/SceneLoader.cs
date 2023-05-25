using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneNames _scene = default;

    public void LoadToScene()
    {
        SceneManager.LoadScene(Consts.Scenes[_scene]);
    }
}