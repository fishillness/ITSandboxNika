using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static FieldController;

public class PieceCounter : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<PieceType> OnPieceAdded;
    [HideInInspector]
    public UnityEvent<PieceType> OnPieceRemoved;

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
            OnPieceAdded?.Invoke(piece.Type);
        }
    }

    private void RemovePiece(Piece piece)
    {
        PieceType type = piece.Type;
        piece.Clerable.OnPieceClear.RemoveListener(RemovePiece);
        pieceCount[type] -= 1;
        OnPieceRemoved?.Invoke(type);
    }
}
