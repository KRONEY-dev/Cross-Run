using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Main_Actor;
    [SerializeField]
    private GameObject Die_Screan;
    [SerializeField]
    private GameObject Background;
    [SerializeField]
    private GameObject Click_Controll;
    [SerializeField]
    private GameObject Score;
    [SerializeField]
    private GameObject Spawn;

    [SerializeField]
    private Text Timer_Text_Object;
    [SerializeField]
    private GameObject[] Energy_Bars;
    [SerializeField]
    private Slider Timer_Slider;

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

        if (Timer_Slider.value < 0.2f) // 0 energy
        {
            Energy_Bar_Switcher(0);
        }
        else if (Timer_Slider.value < 0.4f) // 1 energy
        {
            Energy_Bar_Switcher(1);
        }
        else if (Timer_Slider.value < 0.6f) // 2 energy
        {
            Energy_Bar_Switcher(2);
        }
        else if (Timer_Slider.value < 0.8f) // 3 energy
        {
            Energy_Bar_Switcher(3);
        }
        else if (Timer_Slider.value < Timer_Slider.maxValue) // 4 energy
        {
            Energy_Bar_Switcher(4);
        }
        else if (Timer_Slider.value == Timer_Slider.maxValue) // 5 energy
        {
            Energy_Bar_Switcher(5);
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
        int counter = 0;
        foreach (var item in Energy_Bars)
        {
            if (item.activeInHierarchy == true)
            {
                counter++;
            }
        }
        if (counter > 0) //Can click
        {
            Minus_1_Energy_Bar(counter);
            Background.SetActive(false);
            Score.SetActive(true);
            Click_Controll.SetActive(true);
            Spawn.SetActive(true);
        }
    }

    public void OnButton_Restart_Press()
    {
        State.Setting_state();
        SceneManager.LoadScene(0);
    }

    private void Minus_1_Energy_Bar(int counter)
    {
        Energy_Bar_Counter(false, counter);
        Timer_Text_Object.GetComponent<Timer_Script>().Timer_Slider_Decriminate();
        Timer_Text_Object.GetComponent<Timer_Script>().Run_Timer = true;
    }

    public void Energy_Bar_Counter(bool Switcher, int Counter)
    {
        switch (Counter)
        {
            case 1:
                Energy_Bar_Switcher(1, Switcher);
                break;

            case 2:
                Energy_Bar_Switcher(2, Switcher);
                break;

            case 3:
                Energy_Bar_Switcher(3, Switcher);
                break;

            case 4:
                Energy_Bar_Switcher(4, Switcher);
                break;

            case 5:
                Energy_Bar_Switcher(5, Switcher);
                break;
        }
    }

    private void Energy_Bar_Switcher(int Energy_Bar_Count, bool Switcher)
    {
        if (Switcher)
        {
            Energy_Bars[Energy_Bar_Count - 1].SetActive(Switcher);
        }
        else
        {
            Energy_Bars[Energy_Bar_Count - 1].SetActive(Switcher);
        }
    }

    private void Energy_Bar_Switcher(int Energy_Bar_Count_To_Enable)
    {
        if (Energy_Bar_Count_To_Enable == 0)//Full enable
        {
            foreach (var item in Energy_Bars)
            {
                item.SetActive(false);
            }
        }
        else if (Energy_Bar_Count_To_Enable == Energy_Bars.Length)//Full disable
        {
            foreach (var item in Energy_Bars)
            {
                item.SetActive(true);
            }
        }
        else //Custom "enable/disable"
        {
            for (int i = 0; i < Energy_Bar_Count_To_Enable; i++)
            {
                Energy_Bars[i].SetActive(true);
            }
            for (int i = Energy_Bar_Count_To_Enable; i < Energy_Bars.Length; i++)
            {
                Energy_Bars[i].SetActive(false);
            }
        }
    }
}
