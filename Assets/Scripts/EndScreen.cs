using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
       text = GetComponent<TextMeshProUGUI>(); 
    }


    private void OnEnable()
    {
        Chrono chrono = FindFirstObjectByType<Chrono>();
        chrono.SaveTimeScore();
        text.text = "Votre temps : " + chrono.getTimeText();
        float time = PlayerPrefs.GetFloat("time");
        int minutes = Mathf.FloorToInt(time / 60);
        int secondes = Mathf.FloorToInt(time % 60);
        text.text += "Meilleur temps : "+  minutes.ToString("D2") + ":" + secondes.ToString("D2");

    }
}
