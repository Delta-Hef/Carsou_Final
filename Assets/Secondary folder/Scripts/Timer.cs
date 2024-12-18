using UnityEngine;
using TMPro;  // Make sure TextMeshPro is imported

public class Timer : MonoBehaviour
{
    private float timer = 0f;

    public TextMeshProUGUI timerText; 

    void Update()
    {
        timer += Time.deltaTime;

        timerText.text = "Time: " + Mathf.Floor(timer / 60).ToString("00") + ":" + Mathf.Floor(timer % 60).ToString("00");
    }
}
