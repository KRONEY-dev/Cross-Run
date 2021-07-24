using UnityEngine;

public class Start_Button_Stript : MonoBehaviour
{
    public GameObject Background;
    public GameObject Click_Controll;
    public GameObject Score;
    public GameObject Spawn;

    void OnMouseUpAsButton()
    {
        Background.SetActive(false);
        Score.SetActive(true);
        Click_Controll.SetActive(true);
        Spawn.SetActive(true);
    }
}
