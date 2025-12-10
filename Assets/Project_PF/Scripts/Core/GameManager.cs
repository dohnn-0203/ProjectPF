using UnityEngine;
using System.Collections.Generic;

// 게임의 전체 흐름, 상태, 입력 처리를 담당
public class Game : MonoBehaviour
{
    public Board BoardRef;
    public Piece[,] State = new Piece[8, 8];
    public PieceColor Turn = PieceColor.White;
    
    public List<Piece> WhitePieces = new List<Piece>();
    public List<Piece> BlackPieces = new List<Piece>();
    
    private Piece selected = null;
    private List<Vector2Int> moves = new List<Vector2Int>();

    // 게임 시작 시 말을 초기 위치에 배치하고 데이터 구조 설정
    void Start()
    {
        if (BoardRef == null) return;
    }

    // 매 프레임 사용자 입력을 확인하고 처리
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }
    
    // 마우스 클릭 위치를 감지하고 말 선택/이동 로직
    private void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector2Int gridPos = BoardRef.WorldToGrid(hit.point);
            Piece pieceAtGrid = State[gridPos.x, gridPos.y];
            
            // 말 선택 (첫 번째 클릭)
            if (selected == null)
            {
                if (pieceAtGrid != null && pieceAtGrid.Color == Turn)
                {
                    SelectPiece(pieceAtGrid);
                }
            }
            // 말 이동 (두 번째 클릭)
            else
            {
                if (moves.Contains(gridPos))
                {
                    MovePiece(selected, gridPos.x, gridPos.y);
                }
            }
        }
    }
    
    // 말을 선택하고 이동 가능한 위치 목록을 계산
    private void SelectPiece(Piece piece)
    {
        selected = piece;
        moves = selected.GetValidMoves(State);
    }
    
    // 선택된 말을 새 위치로 이동시키고 게임 상태를 업데이트
    private void MovePiece(Piece piece, int newX, int newY)
    {
        int oldX = piece.X;
        int oldY = piece.Y;

        // 3D 위치 이동
        piece.MoveTo(BoardRef.GridToWorld(newX, newY));

        // 보드 데이터 업데이트
        State[oldX, oldY] = null;
        State[newX, newY] = piece;
        piece.X = newX;
        piece.Y = newY;

        // 턴 교체
        Turn = (Turn == PieceColor.White) ? PieceColor.Black : PieceColor.White;
        
        // 초기화 및 다음 턴 준비
        selected = null;
        moves.Clear();
        
    }
}