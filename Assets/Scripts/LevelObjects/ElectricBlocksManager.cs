using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ElectricBlocksManager : MonoBehaviour
{
    public TileBase electricTile;
    public class Circuit
    {
        public List<GameObject> levers = new List<GameObject>();
        public List<Vector2Int> eBlockPositions = new List<Vector2Int>();
    }

    List<Circuit> circuits = new List<Circuit>();
    public Tilemap solidTm;



    // Start is called before the first frame update
    void Awake()
    {
        PlaceableTile.onPlaceEvent += FindCircuits;
    }


    void FindCircuits(Vector2Int posNotUsed)
    {
        circuits.RemoveRange(0, circuits.Count);
        var eTilePositions = RetreiveAllPositions();
        int initBlockCount = eTilePositions.Count;
        Circuit currCircuit;
        Circuit firstCircuit = new Circuit();
        currCircuit = firstCircuit;
        currCircuit.eBlockPositions.Add(eTilePositions[0]);
        List<Vector2Int> positionsInQueue = new List<Vector2Int>();
        Vector2Int currPos;
        while (eTilePositions.Count > 0)
        {
            if (positionsInQueue.Count > 0)
            {
                currPos = positionsInQueue[0];
                positionsInQueue.RemoveAt(0);
            }
            else
            {
                currPos = eTilePositions[0];
                if (initBlockCount > eTilePositions.Count)
                {
                    Circuit newCircuit = new Circuit();
                    currCircuit = newCircuit;
                    currCircuit.eBlockPositions.Add(currPos);
                }
                eTilePositions.RemoveAt(0);
                circuits.Add(currCircuit);
            }

            for (int i = 0; i < eTilePositions.Count; i++)
            {
                var pos = eTilePositions[i];
                
                if (Vector2Int.Distance(pos, currPos) == 1)
                {
                    currCircuit.eBlockPositions.Add(pos);
                    eTilePositions.RemoveAt(eTilePositions.IndexOf(pos));
                    positionsInQueue.Add(pos);
                }
            }

            if (positionsInQueue.Count > 0)
            {
                positionsInQueue.Remove(currPos);
            }
        }

        for(int i = 0; i < circuits.Count; i++)
        {

            Debug.Log("circuit " + i + "eBlockCount " + circuits[i].eBlockPositions.Count);

        }

        FindConnectedLevers();
    }

    void FindConnectedLevers()
    {
        var allLeverPosition = GameObject.FindGameObjectsWithTag("Lever");
        foreach (Circuit circuit in circuits) 
        {
            foreach (GameObject lever in allLeverPosition)
            {
                if (lever.GetComponent<ElectricLeverBhvr>().peripheralPositions.Intersect(circuit.eBlockPositions).Any())
                {
                    circuit.levers.Add(lever);
                }

            }

        }

    }

    List<Vector2Int> RetreiveAllPositions()
    {
        var cellBounds = new BoundsInt(-solidTm.cellBounds.xMax, -solidTm.cellBounds.yMax,0, 2*solidTm.cellBounds.size.x, 2*solidTm.cellBounds.size.y,1);
        List<Vector2Int> positions = new List<Vector2Int>();
        foreach (var pos in cellBounds.allPositionsWithin)
        {
            if (solidTm.GetTile(pos) == electricTile)
            {
                positions.Add((Vector2Int)pos);
            }
        }
        return positions;
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    

    List<Vector2Int> GetSurroundingLevers(Vector2Int pos)
    {
        List<Vector2Int> positions = new List<Vector2Int>();
        var vector3IntPos = (Vector3Int)pos;
        var downTile = solidTm.GetTile(vector3IntPos + Vector3Int.down);
        var upTile = solidTm.GetTile(vector3IntPos + Vector3Int.up);
        var leftTile = solidTm.GetTile(vector3IntPos + Vector3Int.left);
        var rightTile = solidTm.GetTile(vector3IntPos + Vector3Int.right);

        if (downTile == electricTile)
        {
            positions.Add(pos + Vector2Int.down);
        }

        if (upTile == electricTile)
        {
            positions.Add(pos + Vector2Int.up);
        }
        if (leftTile == electricTile)
        {
            positions.Add(pos + Vector2Int.left);
        }
        if (rightTile == electricTile)
        {
            positions.Add(pos + Vector2Int.right);
        }
        return positions;

    }

}
