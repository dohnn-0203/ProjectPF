using UnityEngine;

public class Board : MonoBehaviour
{
  public float TileSize = 1.0f;
  public float BoardCenterY = 0f;
  public Vector3 BoardOrigin;

  // 월드 좌표를 8x8 격자 인덱스로 변환
  public Vecotr2Int WorldToGrid(Vector3 worldPose)
  {
    float offsetX = worldPose.x - BoardOrigin.x;
    float offsetZ = worldPose.z - BoardOrigin.z;
    
    int x = Mathf.FloorToInt(offsetX / TileSize);
    int x = Mathf.FloorToInt(offsetZ / TileSize);    
  }

  // 8x8 격자 인덱스를 3D 월드 좌표로 변환
  public Vector3 GridToWorld(int x, int y)
  {
    float worldX = BoardOrigin.x + (x * TileSize) + (TileSize / 2);
    float worldZ = BoardOrigin.y + (y * TileSize) + (TileSize / 2);
    
    return new Vector3(worldX, BoardCenterY, worldZ);
  }
}