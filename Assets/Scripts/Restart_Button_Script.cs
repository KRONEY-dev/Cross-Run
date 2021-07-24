using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_Button_Script : MonoBehaviour
{
    public void OnButtonPress()
    {
        State.Setting_state();
        SceneManager.LoadScene(0);
    }
}
