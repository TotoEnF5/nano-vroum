using UnityEngine;

public class ScrollingSection : MonoBehaviour
{
    [SerializeField] private MoveCamera cameraScript;

    private void Awake()
    {
        transform.GetChild(0).GetComponent<Trigger>().SetCameraScript(cameraScript);
        transform.GetChild(1).GetComponent<Trigger>().SetCameraScript(cameraScript);
    }
}
