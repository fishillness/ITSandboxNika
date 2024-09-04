using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMatrixController : MonoBehaviour
{
    [SerializeField] private PieceColorDictionary colorDictionary;
    [SerializeField] private BoosterDictionary boosterDictionary;
    [SerializeField] private PieceCounter pieceCounter;
    [SerializeField] private FieldController field;

    private Piece[,] pieces;
    private int xDim;
    private int yDim;

    public Piece[,] Pieces => pieces;

    public void InitMatrix(int xDim, int yDim)
    {
        this.xDim = xDim;
        this.yDim = yDim;

        pieces = new Piece[xDim, yDim];
    }


    public Piece SpawnNewPiece(int x, int y, PieceType type)
    {
        Piece newPiece = Instantiate(field.PiecePrefabDict[type],
            field.GetPiecePositionOnWorld(x, y), Quaternion.identity);
        newPiece.transform.parent = transform;
        newPiece.Init(x, y, type);

        if (newPiece.Colorable != null)
        {
            if (!newPiece.Colorable.IsColorDictionarySet)
                newPiece.Colorable.SetColorDictionary(colorDictionary);

            newPiece.Colorable.SetColor(colorDictionary.GetRandomColorFromDictionaty());
        }

        pieces[x, y] = newPiece;
        pieceCounter.AddPiece(newPiece);
        return newPiece;
    }
    public void SpawnNewBooster(int x, int y, BoosterType type)
    {
        if (pieces[x, y] != null)
            DeletePiece(x, y);

        Piece boosterPiece = SpawnNewPiece(x, y, PieceType.Booster);
        Booster booster = boosterPiece.GetComponent<Booster>();

        booster.SetProperties(type, this);
        booster.SetBoosterSprite(boosterDictionary.GetSpriteByT(type));
    }


    public void DeletePiece(int x, int y)
    {
        if (!pieces[x, y].IsClerable && pieces[x, y].Type != PieceType.Empty) return;

        PieceType type = pieces[x, y].Type;

        if (pieces[x, y].Clerable == null)
            Destroy(pieces[x, y].gameObject);
        else
            pieces[x, y].Clerable.ClearPiece();

        pieces[x, y] = null;
        if (type != PieceType.Empty)
        {
            SpawnNewPiece(x, y, PieceType.Empty);
        }
    }

    public bool CheckTypeOfPieceInGrid(int x, int y, PieceType type)
    {
        if (pieces[x, y] == null)
            return false;

        return pieces[x, y].Type == type;
    }

    public void SwapEmptyPieceWithNonEmpty(int xEmpty, int yEmpty, int xNonEmpty, int yNonEmpty)
    {
        DeletePiece(xEmpty, yEmpty);
        pieces[xNonEmpty, yNonEmpty].Movable.Move(xEmpty, yEmpty, field.GetPiecePositionOnWorld(xEmpty, yEmpty), field.DroppingTime);
        pieces[xEmpty, yEmpty] = pieces[xNonEmpty, yNonEmpty];

        SpawnNewPiece(xNonEmpty, yNonEmpty, PieceType.Empty);
    }

    public void SwapPiecesOnlyInMatrix(Piece piece1, Piece piece2)
    {
        pieces[piece1.X, piece1.Y] = piece2;
        pieces[piece2.X, piece2.Y] = piece1;
    }

    public void SetPieceNull(int x, int y)
    {
        pieces[x, y] = null;
    }

    public void DeleteSomePieces(List<Piece> pieces)
    {
        foreach (Piece piece in pieces)
        {
            DeletePiece(piece.X, piece.Y);
        }
    }

    public void DeleteRow(int x, int y)
    {
        StartCoroutine(DeleteRowCoroutine(x, y, field.DroppingTime));
    }

    private IEnumerator DeleteRowCoroutine(int x, int y, float time)
    {
        int xLeft = x - 1;
        int xRight = x + 1;

        DeletePiece(x, y);

        while (xLeft >= 0 || xRight < xDim)
        {
            if (xLeft >= 0 && pieces[xLeft, y] != null && pieces[xLeft, y].IsClerable)
            {
                DeletePiece(xLeft, y);
                xLeft--;
            }

            if (xRight < xDim && pieces[xRight, y] != null && pieces[xRight, y].IsClerable)
            {
                DeletePiece(xRight, y);
                xRight++;
            }

            yield return new WaitForSeconds(time);
        }
    }


    public void DeleteColumn(int x, int y)
    {
        StartCoroutine(DeleteColumnCoroutine(x, y, field.DroppingTime));
    }

    private IEnumerator DeleteColumnCoroutine(int x, int y, float time)
    {
        int yAbove = y - 1;
        int yBelow = y + 1;

        DeletePiece(x, y);

        while (yAbove >= 0 || yBelow < yDim)
        {
            if (yAbove >= 0 && pieces[x, yAbove] != null && pieces[x, yAbove].IsClerable)
            {
                DeletePiece(x, yAbove);
                yAbove--;
            }

            if (yBelow < yDim && pieces[x, yBelow] != null && pieces[x, yBelow].IsClerable)
            {
                DeletePiece(x, yBelow);
                yBelow++;
            }

            yield return new WaitForSeconds(time);
        }
    }

    public void DeleteNearPiece(int x, int y)
    {
        DeletePiece(x, y);

        if (pieces[x, y - 1] != null && pieces[x, y - 1].IsClerable) 
            DeletePiece(x, y - 1);

        if (pieces[x + 1, y] != null && pieces[x + 1, y].IsClerable)
            DeletePiece(x + 1, y);

        if (pieces[x, y + 1] != null && pieces[x, y + 1].IsClerable)
            DeletePiece(x, y + 1);

        if (pieces[x - 1, y] != null && pieces[x - 1, y].IsClerable)
            DeletePiece(x - 1, y);
    }

    public void DeleteManyNearPieces(int x, int y)
    {
        int xMin = x - 2;
        int xMax = x + 2;

        int yMin = y - 2;
        int yMax = y + 2;

        DeletePiece(x, y);

        for (int i = xMin; i <= xMax; i++)
        {
            for (int j = yMin; j <= yMax; j++)
            {
                if (i == xMin && j == yMin) continue;
                if (i == xMax && j == yMax) continue;
                if (i == xMin && j == yMax) continue;
                if (i == xMax && j == yMin) continue;
                if (i < 0 || i >= xDim) continue;
                if (j < 0 || j >= yDim) continue;
                if (i == x && j == y) continue;

                if (pieces[i, j] != null && pieces[i, j].IsClerable)
                {
                    DeletePiece(i, j);
                }
            }
        }
    }

    public void DeleteAllPiecesByColor(int x, int y, Piece swapPiece)
    {
        ColorType color;
        if (!swapPiece.IsColorable)
            color = (ColorType)UnityEngine.Random.Range(0, colorDictionary.NumberTypes);
        else
            color = swapPiece.Colorable.Color;

        DeletePiece(x, y);

        foreach(Piece piece in pieces)
        {
            if (piece == null) continue;

            if (piece.IsColorable && piece.IsClerable && piece.Colorable.Color == color)
            {
                DeletePiece(piece.X, piece.Y);
            }
        }
    }
}
