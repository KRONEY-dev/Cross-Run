using UnityEngine.UI;
using UnityEngine;

public class Max_Score_Script : MonoBehaviour
{
    private void OnEnable()
    {
        LoadSave();
    }
    
    void LoadSave()
    {
        if (PlayerPrefs.GetInt("Max_Score") > 0)
        {
            gameObject.GetComponent<Text>().text = $"MAX SCORE: {PlayerPrefs.GetInt("Max_Score")}";
        }
    }
}
