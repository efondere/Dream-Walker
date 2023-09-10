using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class ElectricBlocksManager : MonoBehaviour
{
    GameObjectPool goPool;
    public TileBase electricTile;
    public GameObject lightEffect;

    public class Circuit
    {
        public List<GameObject> levers = new List<GameObject>();
        public List<Vector2Int> eBlockPositions = new List<Vector2Int>();
        public List<ElectricReceiverBhvr> receivers = new List<ElectricReceiverBhvr>();
    }

    List<Circuit> circuits = new List<Circuit>();
    public Tilemap solidTm;


    void Awake()
    {
        PlaceableTile.onPlaceEvent += UpdateCircuit;
        TileRemover.OnRemove += UpdateCircuit;
        ElectricLeverBhvr.onChangeSignalEvent += UpdateRenderer;
        ElectricLeverBhvr.onChangeSignalEvent += UpdateReceivers;
        GameObjectPool.CreatePool(ref goPool, lightEffect);
    }

    void UpdateCircuit(Vector2Int posNotUsed, TilePreview tilePreviewNotUsed)
    {
        if (FindCircuits())
        {
            FindConnectedLevers();
            FindConnectedReceivers();
            UpdateRenderer();
            UpdateReceivers();
        }
    }

    bool FindCircuits()
    {
        circuits.Clear();
        var eTilePositions = RetreiveAllPositions();

        if (eTilePositions.Count == 0)
        {
            return false;
        }
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


            for (int i = eTilePositions.Count - 1; i >= 0; i--)
            {
                var pos = eTilePositions[i];
                if ((Mathf.Abs(pos.y - currPos.y) == 1 && Mathf.Abs(pos.x - currPos.x) == 0) || (Mathf.Abs(pos.x - currPos.x) == 1 && Mathf.Abs(pos.y - currPos.y) == 0))
                {
                    currCircuit.eBlockPositions.Add(pos);
                    positionsInQueue.Add(pos);
                    eTilePositions.RemoveAt(i);
                }
            }

        }        
        return true;


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

    void FindConnectedReceivers()
    {
        var allReceivers = GameObject.FindObjectsByType(typeof(ElectricReceiverBhvr), FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Circuit circuit in circuits)
        {
            foreach (ElectricReceiverBhvr receiver in allReceivers)
            {
                if (receiver.peripheralPositions.Intersect(circuit.eBlockPositions).Any())
                {
                    circuit.receivers.Add(receiver.GetComponent<ElectricReceiverBhvr>());
                }
            }
        }
    }

    void UpdateRenderer()
    {
        goPool.ClearAll();
        foreach (Circuit circuit in circuits)
        {
            if (GetSignal(circuit) == 1)
            {
                foreach (var pos in circuit.eBlockPositions)
                {
                    goPool.Instantiate(new Vector3((float)pos.x + 0.5f, (float)pos.y + 0.5f,0), Quaternion.identity);
                }
            }
        }
    }

    void UpdateReceivers()
    {
        foreach (Circuit circuit in circuits)
        {
            int signal = GetSignal(circuit);
            foreach (var receiver in circuit.receivers)
            {
                receiver.ReceiveSignal(signal);
            }

        }
    }

    int GetSignal(Circuit circuit)
    {
        foreach (GameObject lever in circuit.levers)
        {
            if (lever.GetComponent<ElectricLeverBhvr>().leverState == 1)
                 return 1;
        }
        return 0;
    }

    List<Vector2Int> RetreiveAllPositions()
    {
        List<Vector2Int> positions = new List<Vector2Int>();
        foreach (var pos in solidTm.cellBounds.allPositionsWithin)
        {
            if (solidTm.GetTile(pos) == electricTile)
            {
                positions.Add((Vector2Int)pos);
            }
        }
        return positions;
    }
}
