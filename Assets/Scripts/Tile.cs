using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Status { Red, Blue, Yellow, Block, Blank };
    public static Status _status = Status.Blank;
    public bool isMovable = true;
    public SpriteRenderer TileSprite;
    [SerializeField] private Sprite[] Sprites;
    [SerializeField] private GameObject MyTile;
    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
    private static Tile previousSelected = null;
    /*private SpriteRenderer render;*/
    private bool isSelected = false;
    private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    void Awake()
    {
        TileSprite = GetComponent<SpriteRenderer>();
    }
    private void Select()
    {
        isSelected = true;
        TileSprite.color = selectedColor;
        previousSelected = gameObject.GetComponent<Tile>();
        /*Debug.Log("SELECTED");*/
    }
    private void Deselect()
    {
        isSelected = false;
        TileSprite.color = Color.white;
        previousSelected = null;
        /*Debug.Log("DESELECTED");*/
    }
    void OnMouseDown()
    {
        // Not Selectable conditions
        if (TileSprite == null || !isMovable)
        {
            /*Debug.Log("NOT MOVABLE");*/
            return;
        }

        if (isSelected)
        { // Is it already selected?
            Deselect();
        }
        else
        {
            if (previousSelected == null)
            { // Is it the first tile selected?
                Select();
            }
            else
            {
                if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
                { // Is it an adjacent tile?
                    /*Debug.Log("SWAPPIN 1");*/
                    SwapSprite(previousSelected.TileSprite);
                    previousSelected.Deselect();
                }
                else
                {
                    /*Debug.Log("TOO FAR, BRO");*/
                    previousSelected.GetComponent<Tile>().Deselect();
                    Select();
                }
            }
        }
    }

    public void SwapSprite(SpriteRenderer render2)
    {
        if (TileSprite.sprite == render2.sprite)
        {
            /*Debug.Log("EQUAL SPRITES");*/
            return;
        }

        Sprite tempSprite = render2.sprite;
        render2.sprite = TileSprite.sprite;
        TileSprite.sprite = tempSprite;
        /*Debug.Log("SWAPPED");*/
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
            Debug.Log(GetAdjacent(adjacentDirections[i]).name);
            Debug.Log(GetAdjacent(adjacentDirections[i]).transform.position);
            adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
        }
        return adjacentTiles;
    }

    public void changeStatus(Status status)
    {
        _status = status;
        switch (status)
        {
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
