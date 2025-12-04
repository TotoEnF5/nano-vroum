using TMPro;
using UnityEngine;

public class Chrono : MonoBehaviour
{

    TextMeshProUGUI text;
    float time = 0f;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        time += Time.deltaTime;

        int minutes = Mathf.FloorToInt(time / 60);
        int secondes = Mathf.FloorToInt(time % 60);
        text.text = minutes.ToString("D2") + ":" + secondes.ToString("D2");
    }
}


