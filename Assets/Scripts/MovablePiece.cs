using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePiece : MonoBehaviour
{
    private GamePiece _pieceItem;
    private IEnumerator _coroutine;

    private void Awake()
    {
        _pieceItem = GetComponent<GamePiece>();
    }

    public void MovePiece(int x, int y, float ti) {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = MoveC(x, y, ti);
        StartCoroutine(_coroutine);
    }

    private IEnumerator MoveC(int x, int y, float ti)
    {
        _pieceItem.X = x;
        _pieceItem.Y = y;

        var startP = transform.position;
        var endP = _pieceItem.GridRef.GetWorldPosition(x, y);

        for (float t = 0; t <= 1 * ti; t += Time.deltaTime) {
            _pieceItem.transform.position = Vector3.Lerp(startP, endP, t/ti);
            yield return 0;
        }

        _pieceItem.transform.position = endP;
    }
}
