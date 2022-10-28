using UnityEngine;

public class TileSlider : MonoBehaviour
{
    [SerializeField] private Tile Tile /*= null*/;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray1 = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit1 = Physics2D.Raycast(ray1.origin, ray1.direction);
            Vector2 pos1 = hit1.transform.position;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray2 = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit2 = Physics2D.Raycast(ray2.origin, ray2.direction);
                Vector2 pos2 = hit2.transform.position;
                if (hit2)
                {
                    if ((Vector2.Distance(pos1, pos2) < 2) && (Tile.isMovable== true))
                    {
                        Debug.Log(pos1);
                        Vector2 LastEmptySpacePosition = pos1;
                        pos1 = pos2;
                        pos2 = LastEmptySpacePosition;
                    }
                    /*Debug.Log(hit.transform.name);*//*
                    Debug.Log(Tile.transform.position);
                    Debug.Log(hit.transform.position);*/
                    Debug.Log(pos1);
                    Debug.Log(pos2);
                }
            }
        }
    }
}
