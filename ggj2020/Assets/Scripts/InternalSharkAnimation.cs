using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InternalSharkAnimation : MonoBehaviour
{
    private Sequence _sequence;
    private float _initialVerticalPos;

    // Start is called before the first frame update
    void Start()
    {
        _initialVerticalPos = transform.position.y;

        _sequence = DOTween.Sequence();
        _sequence.Join(transform.DOMove(new Vector3(transform.position.x, _initialVerticalPos + 0.7f, transform.position.z), 2f));
        _sequence.Append(transform.DOMove(new Vector3(transform.position.x, _initialVerticalPos, transform.position.z), 1.5f)).OnComplete(ChangeDirection);
    }

    void ChangeDirection()
    {
        
    }
}
