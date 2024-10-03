using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MovablePiece : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<Piece> OnMoveEnd;

    private Piece piece;
    private Coroutine coroutine;

    private void Awake()
    {
        piece = GetComponent<Piece>();
    }

    public void Move(int newX, int newY, Vector2 newPos, float time)
    {
        piece.SetX(newX);
        piece.SetY(newY);
        piece.UpdateName();

        if (coroutine != null)
            StopCoroutine(coroutine);

        if (time != 0)
        {
            coroutine = StartCoroutine(Moving(newPos, time));
        }
        else
            piece.transform.position = newPos;
    }

    private IEnumerator Moving(Vector2 newPos, float time)
    {
        Vector2 startingPos = transform.position;

        for (float t = 0; t <= 1 * time; t += Time.deltaTime)
        {
            piece.transform.position = Vector3.Lerp(startingPos, newPos, t / time);
            yield return 0;
        }

        piece.transform.position = newPos;
        OnMoveEnd?.Invoke(piece);
    }
}
