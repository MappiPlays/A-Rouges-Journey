using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TilemapGenerator : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] TileBase[] groundTiles;
    [SerializeField] Tilemap borderTilemap;
    [SerializeField] TileBase borderTile;
    [SerializeField] GameObject spawnPointsParent;
    [SerializeField] GameObject spawnPointPrefab;
    [SerializeField] GameObject goldenKeyPrefab;
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
        PlaceGoldenKey();
    }

    void PlaceGroundTile(Vector3Int position, float noisevalue)
    {
        if (needToStayFree(position))
        {
            groundTilemap.SetTile(position, groundTiles[0]);
            return;
        }

        if (noisevalue < .65f && noisevalue > .35f)
        {
            if (noisevalue > .495f && noisevalue < .505f)
            {
                // create EnemySpawnpoints
                Instantiate(spawnPointPrefab, gameObject.GetComponent<Grid>().GetCellCenterWorld(position), Quaternion.identity, spawnPointsParent.transform);
            }
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
        if (needToStayFree(position))
        {
            return;
        }
        if (noisevalue > .7f || noisevalue < .3f)
        {
            if ((position.x + position.y) % 2 == 0)
                borderTilemap.SetTile(position, borderTile);
        }
    }

    bool needToStayFree(Vector3Int position)
    {
        if ((position.x <= 5 && position.y <= 5) || (position.x >= 42 && position.y >= 42))
        {
            return true;
        }

        return false;
    }

    void PlaceGoldenKey()
    {
        int x = 0, y = 0;
        float xCoord, yCoord, noisevalue = 0f;
        while (!(noisevalue < .65f && noisevalue > .35f))
        {
            x = Random.Range(0, width);
            y = Random.Range(0, height);
            xCoord = (float)x / width * scale + offsetX;
            yCoord = (float)y / height * scale + offsetY;
            noisevalue = Mathf.PerlinNoise(xCoord, yCoord);
        }
        Instantiate(goldenKeyPrefab, new Vector2(x+.5f, y+.5f), Quaternion.identity);
    }
}
