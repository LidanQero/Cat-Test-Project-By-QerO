using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{ 
    private void Start()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
