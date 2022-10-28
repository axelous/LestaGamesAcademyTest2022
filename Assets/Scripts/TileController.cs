using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public class TileController : MonoBehaviour
{
    [SerializeField] private GameObject MyTile;
    [SerializeField] private Tile CurrentTile;
    [SerializeField] private int BlueAmount = 5;
    [SerializeField] private int RedAmount = 5;
    [SerializeField] private int YellowAmount = 5;
    private bool TileSpawned = false;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                TileSpawned = false;
                var position = new Vector3(2 * i - 2, j, 0);
                while (!TileSpawned)
                {
                    Tile.Status status = (Tile.Status)Enum.ToObject(typeof(Tile.Status), UnityEngine.Random.Range(0, 3));
                    if ((status == Tile.Status.Red) && (RedAmount != 0))
                    {
                        CurrentTile.changeStatus(Tile.Status.Red);
                        Instantiate(CurrentTile, position, Quaternion.identity);
                        TileSpawned = true;
                        RedAmount--;
                    }
                    if ((status == Tile.Status.Blue) && (BlueAmount != 0))
                    {
                        CurrentTile.changeStatus(Tile.Status.Blue);
                        Instantiate(CurrentTile, position, Quaternion.identity);
                        TileSpawned = true;
                        BlueAmount--;
                    }
                    if ((status == Tile.Status.Yellow) && (YellowAmount != 0))
                    {
                        CurrentTile.changeStatus(Tile.Status.Yellow);
                        Instantiate(CurrentTile, position, Quaternion.identity);
                        TileSpawned = true;
                        YellowAmount--;
                    }
                }
            }
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var position = new Vector3(2 * i - 1, 2 * j - 2, 0);
                CurrentTile.changeStatus(Tile.Status.Block);
                Instantiate(CurrentTile, position, Quaternion.identity);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                var position = new Vector3(2 * i - 1, 2 * j - 1, 0);
                CurrentTile.changeStatus(Tile.Status.Blank);
                Instantiate(CurrentTile, position, Quaternion.identity);
            }
        }
    }
    private void Update()
    {
     
    }
}
