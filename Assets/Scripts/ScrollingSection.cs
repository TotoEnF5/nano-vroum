using UnityEngine;

public class ScrollingSection : MonoBehaviour
{
    [SerializeField] private MoveCamera cameraScript;
    [SerializeField] private float time;

    private void Awake()
    {
        transform.GetChild(0).GetComponent<Trigger>().SetCameraScript(cameraScript);
        transform.GetChild(0).GetComponent<Trigger>().SetTime(time);
        transform.GetChild(1).GetComponent<Trigger>().SetCameraScript(cameraScript);
        transform.GetChild(1).GetComponent<Trigger>().SetTime(time);
    }
}
