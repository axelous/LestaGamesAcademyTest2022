using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Status { Red, Blue, Yellow, Block, Blank };
    public static Status _status = Status.Blank;
    public bool isMovable = true;
    public SpriteRenderer TileSprite;
    [SerializeField] private Sprite[] Sprites;
    [SerializeField] private GameObject MyTile;
    
    public void changeStatus(Status status)
    {
        _status = status;
        switch (status) {
            case Status.Red:
                TileSprite.sprite = Sprites[0];
                break;
            case Status.Blue:
                TileSprite.sprite = Sprites[1];
                break;
            case Status.Yellow:
                TileSprite.sprite = Sprites[2];
                break;
            case Status.Block:
                TileSprite.sprite = Sprites[3];
                isMovable = false;
                break;
            case Status.Blank:
                TileSprite.sprite = Sprites[4];
                break;
        }
    }
}
