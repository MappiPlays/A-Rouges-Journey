using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] TileBase[] groundTiles;
    [SerializeField] Tilemap borderTilemap;
    [SerializeField] TileBase borderTile;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float offsetX;
    [SerializeField] float offsetY;
    [SerializeField] float scale;


    void Start()
    {
        //groundTilemap.ClearAllTiles();

        offsetX = Random.Range(-99999f, 99999);
        offsetY = Random.Range(-99999f, 99999);

        Generate();
    }

    void Generate()
    {
        float x = 0f;
        float y, xCoord, yCoord, noisevalue;
        Vector3Int currentPos;
        while (x < width)
        {
            y = 0f;
            while (y < height)
            {
                currentPos = new Vector3Int((int)x, (int)y, 0);
                xCoord = x / width * scale + offsetX;
                yCoord = y / height * scale + offsetY;
                noisevalue = Mathf.PerlinNoise(xCoord, yCoord);
                
                PlaceGroundTile(currentPos, noisevalue);
                PlaceBorderTile(currentPos, noisevalue);
                
                y++;
            }
            x++;
        }
    }

    void PlaceGroundTile(Vector3Int position, float noisevalue)
    {
        if (position.x <= 5 && position.y <= 5)
        {
            groundTilemap.SetTile(position, groundTiles[0]);
            return;
        }

        if (noisevalue < .65f && noisevalue > .35f)
        {
            //Normal Path
            groundTilemap.SetTile(position, groundTiles[0]);
        }
        else
        {
            //Border
            groundTilemap.SetTile(position, groundTiles[1]);
        }
    }

    void PlaceBorderTile(Vector3Int position, float noisevalue)
    {
        if (position.x <= 5 && position.y <= 5)
        {
            return;
        }
        if (noisevalue > .7f || noisevalue < .3f)
        {
            if ((position.x + position.y) % 2 == 0)
                borderTilemap.SetTile(position, borderTile);
        }
    }
}
