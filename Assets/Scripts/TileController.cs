using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private Tile currentTile;
    [SerializeField] private int blueAmount = 5;
    [SerializeField] private int redAmount = 5;
    [SerializeField] private int yellowAmount = 5;
    [SerializeField] private GameObject blueIndicator;
    [SerializeField] private GameObject redIndicator;
    [SerializeField] private GameObject yellowIndicator;
    private bool tileSpawned = false;

    void Start()
    {
        menu.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                tileSpawned = false;
                var position = new Vector3(2 * i - 2, j, 0);
                while (!tileSpawned)
                {
                    Tile.Status status = (Tile.Status)Enum.ToObject(typeof(Tile.Status), UnityEngine.Random.Range(0, 3));
                    if ((status == Tile.Status.Red) && (redAmount != 0))
                    {
                        currentTile.changeStatus(Tile.Status.Red);
                        Instantiate(currentTile, position, Quaternion.identity);
                        tileSpawned = true;
                        redAmount--;
                    }
                    if ((status == Tile.Status.Blue) && (blueAmount != 0))
                    {
                        currentTile.changeStatus(Tile.Status.Blue);
                        Instantiate(currentTile, position, Quaternion.identity);
                        tileSpawned = true;
                        blueAmount--;
                    }
                    if ((status == Tile.Status.Yellow) && (yellowAmount != 0))
                    {
                        currentTile.changeStatus(Tile.Status.Yellow);
                        Instantiate(currentTile, position, Quaternion.identity);
                        tileSpawned = true;
                        yellowAmount--;
                    }
                }
            }
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var position = new Vector3(2 * i - 1, 2 * j - 2, 0);
                currentTile.changeStatus(Tile.Status.Block);
                Instantiate(currentTile, position, Quaternion.identity);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                var position = new Vector3(2 * i - 1, 2 * j - 1, 0);
                currentTile.changeStatus(Tile.Status.Blank);
                Instantiate(currentTile, position, Quaternion.identity);
            }
        }
    }

    private List<GameObject> GetTilesBelow(GameObject Indicator)
    {
        List<GameObject> tilesBelow = new List<GameObject>();
        var Correcting = new Vector3(0f, -0.5f, 0f);
        RaycastHit2D hit = Physics2D.Raycast((Indicator.transform.position + Correcting), Vector2.down);
        for (int i = 1; i < 6; i++)
        {
            if (hit.collider != null)
            {
                tilesBelow.Add(hit.collider.gameObject);
                hit = Physics2D.Raycast(hit.collider.gameObject.transform.position, Vector2.down);
            }
        }
        return tilesBelow;
    }

    private bool AreAllTilesBelowEqual()
    {
        List<GameObject> BlueRow = GetTilesBelow(blueIndicator);
        List<GameObject> RedRow = GetTilesBelow(redIndicator);
        List<GameObject> YellowRow = GetTilesBelow(yellowIndicator);
        for (int i = 1; i < 5; i++)
        {
            if ((BlueRow[i].GetComponent<SpriteRenderer>().sprite != BlueRow[i - 1].GetComponent<SpriteRenderer>().sprite)
                ||
                (RedRow[i].GetComponent<SpriteRenderer>().sprite != RedRow[i - 1].GetComponent<SpriteRenderer>().sprite)
                ||
                (YellowRow[i].GetComponent<SpriteRenderer>().sprite != YellowRow[i - 1].GetComponent<SpriteRenderer>().sprite))
            {
                return false;
            }
        }
        if ((BlueRow[0].GetComponent<SpriteRenderer>().sprite.name != "Blue")
                ||
                (RedRow[0].GetComponent<SpriteRenderer>().sprite.name != "Red")
                ||
                (YellowRow[0].GetComponent<SpriteRenderer>().sprite.name != "Yellow"))
        {
            return false;
        }
        return true;
    }

    private void Update()
    {
        if (AreAllTilesBelowEqual())
        {
            menu.SetActive(true);
        }
    }

    public void OnClick()
    {
        SceneManager.LoadScene("My Scene");
    }
}
