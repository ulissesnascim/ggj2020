using UnityEngine;
using UnityEngine.UI;

public class SurvivedTime : MonoBehaviour
{
    public Text text;
    
    void Awake()
    {
        text.text = "Your time: " + Mathf.Round(PlayerPrefs.GetFloat("timeSurvived")).ToString();
        PlayerPrefs.SetFloat("timeSurvived", 0f);
    }
}
