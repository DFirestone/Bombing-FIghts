using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager
    : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;
        
        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int colums = 11;
    public int rows = 11;

    public Count wallCount = new Count(40, 60);

    public GameObject[] floorTiles;
    public GameObject[] solidWallTiles;
    public GameObject[] wallTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitializeList()
    {
        gridPositions.Clear();

        for (int x = 1; x < colums-1; x++)
        {
            for (int y = 1; y < rows-1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        if (boardHolder != null)
            Destroy(boardHolder.parent);
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < colums+1; x++)
        {
            for (int y = -1; y < rows+1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                if(x == -1 || x == colums || y == -1 || y == rows || (x > 0 && x < colums && y > 0 && y < rows && x % 2 == 1 && y % 2 == 1)  )
                {
                    toInstantiate = solidWallTiles[Random.Range(0, solidWallTiles.Length)];
                    if( (x > 0 && x < colums) || (y > 0 && y < rows))
                        gridPositions.Remove(new Vector3(x, y, 0f));
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.Remove(randomPosition);

        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();

            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }

    }
    
    public void SetupScene(int level)
    {
        InitializeList();
        BoardSetup();
        //LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum
    }


}
