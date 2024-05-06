using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearablePiece : MonoBehaviour
{

    public AnimationClip clearAnimation;

    protected GamePiece piece;
    private bool _itemBeingCleared = false;

    public bool IsBeingCleared => _itemBeingCleared; 


    private void Awake()
    {
        piece = GetComponent<GamePiece>();
    }

    public void Clear()
    {
        _itemBeingCleared = true;
        StartCoroutine(CoroutineOfClearing());
    }

    private IEnumerator CoroutineOfClearing()
    {
        var animatorComponent = GetComponent<Animator>();

        if (animatorComponent) {
            animatorComponent.Play(clearAnimation.name);
            yield return new WaitForSeconds(clearAnimation.length);
            Destroy(gameObject);
        }
    }
}
