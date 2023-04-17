using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private Room[,] grid;
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Returns a random room
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateMap()
    {
        //Clear out the grid - "column" is our X, "row" is our Y
        grid = new Room[cols, rows];

        //For each grid row...
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            //For each column in that row
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {
                //Figure out the location
                float xPosition = roomWidth * currentCol;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                //Create a new grid at the appropriate location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                //Set its parent
                tempRoomObj.transform.parent = this.transform;

                //Give it a meaningful name
                tempRoomObj.name = "Room_" + currentCol + "," + currentRow;

                //Get the room object
                grid[currentCol, currentRow] = tempRoom;

                //Open the doors
                //If we are on the bottom row, open the north door
                if (currentRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (currentRow == rows - 1)
                {
                    //Otherwise, if we are on the to row, open the south door
                    Destroy(tempRoom.doorSouth);
                }
                else
                {
                    //Otherwise, we are in the middle, so open both doors
                    Destroy(tempRoom.doorNorth);
                    Destroy(tempRoom.doorSouth);
                }

            }
        }
    }
}
