using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static FieldController;

public class PieceCounter : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<Piece> OnPieceAdded;
    [HideInInspector]
    public UnityEvent<Piece> OnPieceRemoved;

    private Dictionary<PieceType, int> pieceCount;

    public void InitDictionery(PiecePrefab[] piecePrefabs)
    {
        pieceCount = new Dictionary<PieceType, int>();

        foreach (var piece in piecePrefabs)
        {
            if (!pieceCount.ContainsKey(piece.type))
            {
                pieceCount.Add(piece.type, 0);
            }
        }
    }

    public void AddPiece(Piece piece)
    {
        if (pieceCount.ContainsKey(piece.Type) && piece.IsClerable)
        {
            pieceCount[piece.Type] += 1;
            piece.Clerable.OnPieceClear.AddListener(RemovePiece);
            OnPieceAdded?.Invoke(piece);
        }
    }

    private void RemovePiece(Piece piece)
    {
        PieceType type = piece.Type;
        piece.Clerable.OnPieceClear.RemoveListener(RemovePiece);
        pieceCount[type] -= 1;
        OnPieceRemoved?.Invoke(piece);
    }
}
