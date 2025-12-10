using UnityEngine;
using System.Collections.Generic;

// 체스 말 종류
public enum PieceType {None, King, Queen, Rook, Bishop, Knight, Pawn}
// 체스 진영
public enum PieceColor {None, White, Black}

public abstract class Piece : MonoBehaviour
{
  public PieceType Type;
  public PieceColor Color;
  public int X { get; set; }
  public int Y { get; set; }

  public bool Moved = false;

  // 현재 보드 상태에서 말이 이동 가능한 좌표 계산
  public abstract List<Vector2Int> GetValidMoves (Piece[,] board);
  
  // 말을 좌표이동 시키고 Moved 상태를 업데이트
  public void MoveTo(Vector3 worldPos)
  {
    transforjm.position = worldPos;
    Moved = true;
  }
}
