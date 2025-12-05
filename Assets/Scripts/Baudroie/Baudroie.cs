using DG.Tweening;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Baudroie : MonoBehaviour
{
    public Transform character;

    private bool _animationTriggered = false;
    
    private void Awake()
    {
        transform.GetComponentInChildren<DeathZone>().baudroie = this;
    }
    
    public void DoAnimation()
    {
        if (_animationTriggered)
        {
            return;
        }
        
        float angle = Vector3.Angle(transform.position, character.position);
        Vector3 goalAngles = new Vector3(0, 0, -angle);
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(goalAngles, 0.3f))
            .Append(transform.DOMove(character.position, 0.5f));

        _animationTriggered = true;
    }
}
