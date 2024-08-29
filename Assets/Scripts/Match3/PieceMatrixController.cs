using System.Collections.Generic;
using UnityEngine;

public class PieceMatrixController : MonoBehaviour
{
    [SerializeField] private PieceColorDictionary colorDictionary;
    [SerializeField] private BoosterDictionary boosterDictionary;
    [SerializeField] private PieceCounter pieceCounter;
    [SerializeField] private FieldController field;

    private Piece[,] pieces;

    public Piece[,] Pieces => pieces;

    public void InitMatrix(int xDim, int yDim)
    {
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

            newPiece.Colorable.SetColor((ColorType)UnityEngine.Random.Range(0, colorDictionary.NumberTypes));
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

        booster.SetBoosterType(type);
        booster.SetBoosterSprite(boosterDictionary.GetSpriteByT(type));
    }


    public void DeletePiece(int x, int y)
    {
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
}
