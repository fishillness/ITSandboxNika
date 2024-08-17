using System.Collections;
using UnityEngine;

public class MovablePiece : MonoBehaviour
{
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

        coroutine = StartCoroutine(Moving(newPos, time));
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
    }
}
