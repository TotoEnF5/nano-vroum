using UnityEngine;

public class ScrollingSection : MonoBehaviour
{
    [SerializeField] private MoveCamera cameraScript;
    [SerializeField] private bool showInGame = false;
    [SerializeField] private float time = 2;
    [SerializeField, Min(0)] private float newCameraSize = 5;
    [SerializeField] private bool oneShot = true;

    private void Awake()
    {
        Trigger source = transform.GetChild(0).GetComponent<Trigger>();
        Trigger destination = transform.GetChild(1).GetComponent<Trigger>();
        
        source.cameraScript = cameraScript;
        source.showInGame = showInGame;
        source.time = time;
        source.oneShot = oneShot;
        source.newCameraSize = newCameraSize;
        source.Show(showInGame);
        
        destination.cameraScript = cameraScript;
        destination.showInGame = showInGame;
        destination.Show(showInGame);
    }

    public void ResetState()
    {
        Trigger source = transform.GetChild(0).GetComponent<Trigger>();
        Trigger destination = transform.GetChild(1).GetComponent<Trigger>();
        
        source.ResetState();
        destination.ResetState();
    }
}
