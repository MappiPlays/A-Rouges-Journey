using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float offsetX;
    [SerializeField] float offsetY;
    [SerializeField] float scale;

    [SerializeField] TileBase[] tiles;
    private Tilemap groundTileMap;

    private void Awake()
    {
        groundTileMap = GetComponentInChildren<Tilemap>();
    }

    void Start()
    {
        groundTileMap.ClearAllTiles();

        offsetX = Random.Range(-99999f, 99999);
        offsetY = Random.Range(-99999f, 99999);

        Generate();
    }

    void Generate()
    {
        float x = 0f;
        float y, xCoord, yCoord;
        int tilenum;
        while (x < width)
        {
            y = 0f;
            while (y < height)
            {
                xCoord = x / width * scale + offsetX;
                yCoord = y / height * scale + offsetY;
                float noisevalue = Mathf.PerlinNoise(xCoord, yCoord);
                if (noisevalue < .7f && noisevalue > .3f)
                {
                    //int tilenum = Mathf.Clamp(Mathf.FloorToInt(noisevalue * tiles.Length), 0, tiles.Length - 1);
                    tilenum = 0;
                }
                else
                {
                    tilenum = 1;
                }
                groundTileMap.SetTile(new Vector3Int((int)x, (int)y, 0), tiles[tilenum]);
                y++;
            }
            x++;
        }
    }
}
