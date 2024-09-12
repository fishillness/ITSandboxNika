using UnityEngine;

public class PiecesSpawner : MonoBehaviour
{
    private int x;
    private int y;

    public int X => x;
    public int Y => y;

    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
