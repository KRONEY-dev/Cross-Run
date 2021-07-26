using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Game_Observer : MonoBehaviour
{
    public GameObject Main_Actor;
    public GameObject Die_Screan;
    public GameObject Background;
    public GameObject Click_Controll;
    public GameObject Score;
    public GameObject Spawn;

    private bool Just_die = true;

    private void Start()
    {
        LoadSave();
    }

    private void Update()
    {
        if (!Main_Actor.activeInHierarchy)
        {
            if (Just_die)
            {
                Just_die = false;
                async_Die_Method();
            }
        }
    }

    async void async_Die_Method()
    {
        SaveGame();
        Physics2D.gravity = new Vector2(0, -9);
        await Task.Delay(2000);
        if (!Main_Actor.activeSelf)
        {
            Die_Screan.SetActive(true);
        }
    }

    void SaveGame()
    {
        string _score_text = Score.GetComponentInChildren<Text>().text;
        _score_text = _score_text.Remove(0, _score_text.IndexOf(":") + 2);
        int _score_temp = int.Parse(_score_text);
        if (PlayerPrefs.GetInt("Max_Score") < _score_temp)
        {
            PlayerPrefs.SetInt("Max_Score", _score_temp);
        }
    }

    void LoadSave()
    {
        if (PlayerPrefs.GetInt("Max_Score") > 0)
        {
            GameObject.Find("Max_Score").GetComponent<Text>().text = $"MAX SCORE: {PlayerPrefs.GetInt("Max_Score")}";
        }
    }

    public void OnButton_Start_Press()
    {
        Background.SetActive(false);
        Score.SetActive(true);
        Click_Controll.SetActive(true);
        Spawn.SetActive(true);
    }

    public void OnButton_Restart_Press()
    {
        State.Setting_state();
        SceneManager.LoadScene(0);
    }
}
