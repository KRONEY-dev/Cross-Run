using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Script : MonoBehaviour
{
    private bool run_timer;
    public bool Run_Timer
    {
        get => run_timer;
        set
        {
            if (value == true)
            {
                run_timer = true;
                if (Timer_Mechanism_Curent != null)
                {
                    StopCoroutine(Timer_Mechanism_Curent);
                }
                Timer += Timer_Full_Time;
                Timer_Mechanism_Curent = StartCoroutine(Timer_Mechanism());
            }
        }
    }
    private Text Timer_Text;

    [SerializeField]
    private Slider Timer_Slider;
    private float Timer_Slider_Tick;
    [SerializeField]
    private int Full_Energy_Count;
    private float Timer_Slider_Decriment;

    private static readonly TimeSpan Timer_Full_Time = new TimeSpan(0, 0, 50);
    private static readonly TimeSpan Timer_Tick = new TimeSpan(0, 0, 1);
    private static readonly TimeSpan Timer_Finish = new TimeSpan(0, 0, 0);

    private TimeSpan Timer = new TimeSpan();//Value overwrite

    private Coroutine Timer_Slider_Mechanism_Curent;
    private Coroutine Timer_Mechanism_Curent;


    void OnEnable()
    {
        Timer_Slider_Decriment = Timer_Slider.maxValue / Full_Energy_Count;
        Timer_Text = gameObject.GetComponent<Text>();
        Load();
    }

    void OnDisable()
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetString("Timer_Prefs", Timer.ToString());
        PlayerPrefs.SetString("DataTime_Last_Prefs", DateTime.Now.ToString());
        PlayerPrefs.SetFloat("Timer_Slider.value_Prefs", Timer_Slider.value);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("Timer_Prefs"))
        {
            TimeSpan Time_Difference = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("DataTime_Last_Prefs"));
            if (Time_Difference > TimeSpan.Parse(PlayerPrefs.GetString("Timer_Prefs")))
            {
                Timer = TimeSpan.Zero;
                Timer_Slider.value = 1;
            }
            else
            {
                Timer = TimeSpan.Parse(PlayerPrefs.GetString("Timer_Prefs")) - Time_Difference;
                Timer = new TimeSpan(0, Timer.Minutes, Timer.Seconds);
                Timer_Mechanism_Curent = StartCoroutine(Timer_Mechanism());
                Timer_Slider.value = PlayerPrefs.GetFloat("Timer_Slider.value_Prefs") + PlayerPrefs.GetFloat("Timer_Slider_Tick_Prefs") * (float)Time_Difference.TotalSeconds;
                Timer_Slider_Mechanism_Curent = StartCoroutine(Timer_Slider_Mechanism());
            }
        }
    }

    public void Timer_Slider_Decriminate()
    {
        Timer_Slider.value -= Timer_Slider_Decriment;
        if (Timer_Slider_Mechanism_Curent != null)
        {
            StopCoroutine(Timer_Slider_Mechanism_Curent);
        }
        Timer_Slider_Mechanism_Curent = StartCoroutine(Timer_Slider_Mechanism());
    }

    private IEnumerator Timer_Slider_Mechanism()
    {
        Timer_Slider_Tick = Timer_Slider_Decriment / (float)Timer_Full_Time.TotalSeconds;
        PlayerPrefs.SetFloat("Timer_Slider_Tick_Prefs", Timer_Slider_Tick);
        while (Timer_Slider.value <= 1)//Slider timer body
        {
            Timer_Slider.value += Timer_Slider_Tick;
            yield return new WaitForSeconds(1);
        }
        yield break;
    }

    private IEnumerator Timer_Mechanism()
    {
        while (Timer >= Timer_Finish)//Timer body
        {
            Timer_Text.text = Timer.ToString(@"mm\:ss");
            Timer -= Timer_Tick;
            yield return new WaitForSeconds(1); 
        }
        yield break;
    }
}
