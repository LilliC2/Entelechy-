using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public float numOfRooms;
    public GameObject[] roomPrefabs;
    Vector3 lastRoomPos;
    public float distanceBetweenRooms;
    public GameObject roomParent;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int i = 0; i < numOfRooms; i++)
        {
            int r = Random.Range(0, roomPrefabs.Length);

            if(roomPrefabs[r].name.Contains("Small"))
            {
                distanceBetweenRooms = 50;
            }
            else if (roomPrefabs[r].name.Contains("Medium"))
            {
                distanceBetweenRooms = 75;
            }
            else if (roomPrefabs[r].name.Contains("Large"))
            {
                distanceBetweenRooms = 100;
            }
            else if (roomPrefabs[r].name.Contains("Large"))
            {
                distanceBetweenRooms = 150;
            }

                Vector3 newRoomPos = new Vector3(lastRoomPos.x + distanceBetweenRooms, lastRoomPos.y, lastRoomPos.z);
            Instantiate(roomPrefabs[r], newRoomPos, Quaternion.identity, roomParent.transform);
            lastRoomPos = newRoomPos;
        }

    }
}
