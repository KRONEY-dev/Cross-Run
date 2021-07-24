using System.Threading.Tasks;
using UnityEngine;
using System;   
using UnityEngine.UI;

public class Main_Actor_Script : MonoBehaviour
{
    public GameObject Die_Screan;
    public GameObject score_text;

    private void OnDisable()
    {
        async_Die_Method();
    }

    async void async_Die_Method()
    {
        Physics2D.gravity = new Vector2(0, -9);
        await Task.Delay(2000);
        SaveGame();
        if (!gameObject.activeSelf)
        {
            Die_Screan.SetActive(true);
        }
    }

    void SaveGame()
    {
        string _score_text = score_text.GetComponent<Text>().text;
        _score_text = _score_text.Remove(0, _score_text.IndexOf(":") + 2);
        int _score_temp = int.Parse(_score_text);
        if (PlayerPrefs.GetInt("Max_Score") < _score_temp)
        {
            PlayerPrefs.SetInt("Max_Score", _score_temp);
        }

    }
}
