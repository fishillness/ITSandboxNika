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

    private string activeBoosterName;
    private bool isThereActiveBooster;

    public Piece[,] Pieces => pieces;
    public bool IsThereActiveBooster => isThereActiveBooster;


    public void InitMatrix(int xDim, int yDim)
    {
        this.xDim = xDim;
        this.yDim = yDim;

        pieces = new Piece[xDim, yDim];
    }

    public Piece SpawnNewPiece(int x, int y, PieceType type)
    {
        if (pieces[x,y] != null)
        {
            if (pieces[x, y].Type == PieceType.Empty)
                DeleteEmptyPiece(x, y);
            else 
                return null;
        }

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
        {
            if (!pieces[x, y].IsDestructible) return;

            if (pieces[x, y].Type == PieceType.Empty)
                DeleteEmptyPiece(x, y);
            else
            {
                pieces[x,y].Destructible.DestroyImmediately();
                pieces[x, y] = null;
            }
        }

        Piece boosterPiece = SpawnNewPiece(x, y, PieceType.Booster);

        boosterPiece.Booster.SetProperties(type, this);
        boosterPiece.Booster.SetBoosterSprite(boosterDictionary.GetSpriteByT(type));
    }

    public void DeleteEmptyPiece(int x, int y)
    {
        if (pieces[x, y] == null) return;
        if (pieces[x, y].Type != PieceType.Empty)
        {
            return;
        }

        Destroy(pieces[x, y].gameObject);
        pieces[x, y] = null;
    }

    public bool DeleteNotEmptyPiece(int x, int y, DestructionType destructionType)
    {
        if (pieces[x, y] == null) return false;
        if (pieces[x, y].Type == PieceType.Empty)
        {
            Debug.Log($"Попытка удалить пустую фишку: {x}, {y}");
            return false;
        }
        if (!pieces[x,y].IsDestructible) return false;

        if (pieces[x, y].Destructible.IsPieceDestroyThisType(destructionType))
        {
            pieces[x, y].Destructible.OnPieceDestroy.AddListener(RemoveDestroyingPieceFromMatrix);
            pieces[x, y].Destructible.DestroyPiece(destructionType);

            return true;
        }

        return false;
    }

    private void RemoveDestroyingPieceFromMatrix(Piece piece)
    {
        piece.Destructible.OnPieceDestroy.RemoveListener(RemoveDestroyingPieceFromMatrix);

        if (pieces[piece.X, piece.Y] == piece)
        {
            pieces[piece.X, piece.Y] = null;
            SpawnNewPiece(piece.X, piece.Y, PieceType.Empty);
        }

        field.StartDropPieces();
    }

    public bool CheckTypeOfPieceInGrid(int x, int y, PieceType type)
    {
        if (pieces[x, y] == null)
            return false;

        return pieces[x, y].Type == type;
    }

    public void SwapEmptyPieceWithNonEmpty(int xEmpty, int yEmpty, int xNonEmpty, int yNonEmpty)
    {
        DeleteEmptyPiece(xEmpty, yEmpty);
        pieces[xNonEmpty, yNonEmpty].Movable.Move(xEmpty, yEmpty, field.GetPiecePositionOnWorld(xEmpty, yEmpty), field.DroppingTime);
        pieces[xEmpty, yEmpty] = pieces[xNonEmpty, yNonEmpty];

        pieces[xNonEmpty, yNonEmpty] = null;
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

    public void DeleteSomePieces(List<Piece> pieces, DestructionType destructionType)
    {
        foreach (Piece piece in pieces)
        {
            DeleteNotEmptyPiece(piece.X, piece.Y, destructionType);
        }
    }

    public void DeleteRow(int x, int y, float time)
    {
        BoosterActivated(pieces[x, y].name);

        StartCoroutine(DeleteRowCoroutine(x, y, time));
    }

    private IEnumerator DeleteRowCoroutine(int x, int y, float time)
    {
        string boosterName = pieces[x,y].name;
        int xLeft = x - 1;
        int xRight = x + 1;

        DeleteNotEmptyPiece(x, y, DestructionType.Rocket);

        while (xLeft >= 0 || xRight < xDim)
        {
            if (xLeft >= 0 && pieces[xLeft, y] != null && pieces[xLeft, y].IsDestructible)
            {
                DeleteNotEmptyPiece(xLeft, y, DestructionType.Rocket);
                xLeft--;
            }

            if (xRight < xDim && pieces[xRight, y] != null && pieces[xRight, y].IsDestructible)
            {
                DeleteNotEmptyPiece(xRight, y, DestructionType.Rocket);
                xRight++;
            }

            yield return new WaitForSeconds(time);
        }

        if (boosterName == activeBoosterName)
        {
            isThereActiveBooster = false;
            field.StartDropPieces();
        }
    }

    public void DeleteColumn(int x, int y, float time)
    {
        BoosterActivated(pieces[x, y].name);

        StartCoroutine(DeleteColumnCoroutine(x, y, time));
    }

    private IEnumerator DeleteColumnCoroutine(int x, int y, float time)
    {
        string boosterName = pieces[x, y].name;
        int yAbove = y - 1;
        int yBelow = y + 1;

        DeleteNotEmptyPiece(x, y, DestructionType.Rocket);

        while (yAbove >= 0 || yBelow < yDim)
        {
            if (yAbove >= 0 && pieces[x, yAbove] != null && pieces[x, yAbove].IsDestructible)
            {
                DeleteNotEmptyPiece(x, yAbove, DestructionType.Rocket);
                yAbove--;
            }

            if (yBelow < yDim && pieces[x, yBelow] != null && pieces[x, yBelow].IsDestructible)
            {
                DeleteNotEmptyPiece(x, yBelow, DestructionType.Rocket);
                yBelow++;
            }

            yield return new WaitForSeconds(time);
        }

        if (boosterName == activeBoosterName)
        {
            isThereActiveBooster = false;
            field.StartDropPieces();
        }
    }

    public void DeleteNearPiece(int x, int y)
    {
        BoosterActivated(pieces[x, y].name);

        string boosterName = pieces[x, y].name;

        DeleteNotEmptyPiece(x, y, DestructionType.Bomb);

        if (pieces[x, y - 1] != null && pieces[x, y - 1].IsDestructible)
            DeleteNotEmptyPiece(x, y - 1, DestructionType.Bomb);

        if (pieces[x + 1, y] != null && pieces[x + 1, y].IsDestructible)
            DeleteNotEmptyPiece(x + 1, y, DestructionType.Bomb);

        if (pieces[x, y + 1] != null && pieces[x, y + 1].IsDestructible)
            DeleteNotEmptyPiece(x, y + 1, DestructionType.Bomb);

        if (pieces[x - 1, y] != null && pieces[x - 1, y].IsDestructible)
            DeleteNotEmptyPiece(x - 1, y, DestructionType.Bomb);


        if (boosterName == activeBoosterName)
        {
            isThereActiveBooster = false;
            field.StartDropPieces();
        }
    }

    public void DeleteManyNearPieces(int x, int y, float time)
    {
        BoosterActivated(pieces[x, y].name);

        StartCoroutine(DeleteManyNearPiecesCoroutine(x, y, time));
    }

    private IEnumerator DeleteManyNearPiecesCoroutine(int x, int y, float time)
    {
        string boosterName = pieces[x, y].name;

        int xMin = x - 2;
        int xMax = x + 2;

        int yMin = y - 2;
        int yMax = y + 2;

        DeleteNotEmptyPiece(x, y, DestructionType.Bomb);
        yield return new WaitForSeconds(time);

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

                if (pieces[i, j] != null && pieces[i, j].IsDestructible)
                {
                    DeleteNotEmptyPiece(i, j, DestructionType.Bomb);
                    yield return new WaitForSeconds(time);
                }
            }
        }

        if (boosterName == activeBoosterName)
        {
            isThereActiveBooster = false;
            field.StartDropPieces();
        }
    }

    public void DeleteAllPiecesByColor(int x, int y, Piece swapPiece, float time)
    {
        BoosterActivated(pieces[x, y].name);

        StartCoroutine(DeleteAllPiecesByColorCoroutine(x, y, swapPiece, time));
    }

    private IEnumerator DeleteAllPiecesByColorCoroutine(int x, int y, Piece swapPiece, float time)
    {
        string boosterName = pieces[x, y].name;

        ColorType color;
        if (!swapPiece.IsColorable)
            color = (ColorType)UnityEngine.Random.Range(0, colorDictionary.NumberTypes);
        else
            color = swapPiece.Colorable.Color;

        DeleteNotEmptyPiece(x, y, DestructionType.ByActivationByself);

        foreach(Piece piece in pieces)
        {
            if (piece == null) continue;

            if (piece.IsColorable && piece.IsDestructible && piece.Colorable.Color == color)
            {
                DeleteNotEmptyPiece(piece.X, piece.Y, DestructionType.Rainbow);
                yield return new WaitForSeconds(time);
            }
        }

        if (boosterName == activeBoosterName)
        {
            isThereActiveBooster = false;
            field.StartDropPieces();
        }
    }

    private void BoosterActivated(string boosterName)
    {
        activeBoosterName = boosterName;
        isThereActiveBooster = true;
    }
}
