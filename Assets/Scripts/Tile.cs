using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Status { Red, Blue, Yellow, Block, Blank };
    public static Status _status = Status.Blank;
    public bool isMovable = true;
    public SpriteRenderer tileSprite;
    [SerializeField] private Sprite[] sprites;
    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
    private static Tile previousSelected = null;
    private bool isSelected = false;
    private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    void Awake()
    {
        tileSprite = GetComponent<SpriteRenderer>();
    }
    private void Select()
    {
        isSelected = true;
        tileSprite.color = selectedColor;
        previousSelected = gameObject.GetComponent<Tile>();
    }
    private void Deselect()
    {
        isSelected = false;
        tileSprite.color = Color.white;
        previousSelected = null;
    }
    void OnMouseDown()
    {
        // Ќевыбираемые клетки (блок)
        if (!isMovable)
        {
            return;
        }

        if (isSelected)
        { //  летка уже выбрана?
            Deselect();
        }
        else
        {
            if (previousSelected == null)
            { // Ёто перва€ выбранна€ клетка?
                Select();
            }
            else
            {
                if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
                { // Ёто соседн€€ клетка?
                    SwapSprite(previousSelected.tileSprite);
                    previousSelected.Deselect();
                }
                else
                {
                    previousSelected.GetComponent<Tile>().Deselect();
                    Select();
                }
            }
        }
    }

    public void SwapSprite(SpriteRenderer render2)
    {
        if (tileSprite.sprite == render2.sprite)
        {
            return;
        }

        Sprite tempSprite = render2.sprite;
        render2.sprite = tileSprite.sprite;
        tileSprite.sprite = tempSprite;
    }

    private GameObject GetAdjacent(Vector2 castDir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private List<GameObject> GetAllAdjacentTiles()
    {
        List<GameObject> adjacentTiles = new List<GameObject>();
        for (int i = 0; i < adjacentDirections.Length; i++)
        {
            if (GetAdjacent(adjacentDirections[i]) != null)
                {
                    adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
                }
            }
            return adjacentTiles;
    }

    public void changeStatus(Status status)
    {
        _status = status;
        switch (status)
        {
            case Status.Red:
                tileSprite.sprite = sprites[0];
                break;
            case Status.Blue:
                tileSprite.sprite = sprites[1];
                break;
            case Status.Yellow:
                tileSprite.sprite = sprites[2];
                break;
            case Status.Block:
                tileSprite.sprite = sprites[3];
                isMovable = false;
                break;
            case Status.Blank:
                tileSprite.sprite = sprites[4];
                isMovable = true;
                break;
        }
    }
}
