using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public abstract class ZoneBehavior : MonoBehaviour
{
    // Summary: This gets the information on the block that is placed
    // Summary(cont'd): (i.e. number of tiles the block has).
    // Summary(cont'd): Declares the RespondToTrigger and RemoveBlock methods

    public UnityEvent onPlaced;

    [HideInInspector]public bool canPlace;
    [HideInInspector]public int nbTriggeredZoneTiles;
    public Transform spritesContainer;
    public Tilemap solidTm;

    protected List<Vector2Int> tilePosInZone = new();
    
    protected List<Vector2Int> zonePositions = new();
    
    // to make sure that RespondToTrigger only calls once during a trigger call
    private bool canRespondToTrigger = true;

    private void Awake()
    {
        SetAllZonePositions();
        PlaceableTile.onPlaceEvent += OnPlace;
        TileRemover.OnRemove += OnRemove;
    }

    void OnRemove(Vector2Int posNotUsed, TilePreview tilePreviewNotUsed)
    {
        UpdateZoneTiles();
    }


    void OnPlace(Vector2Int pos, TilePreview tilePreview)
    {
        if (CheckOverlap(pos, tilePreview))
        {
            if (CheckValidPlacement(pos, tilePreview))
            {
                UpdateZoneTiles();
                RespondToTrigger();
            }
            else
            {
                Camera.main.DOShakePosition(0.4f);
            }
        }

    }


    bool CheckOverlap(Vector2Int pos, TilePreview tilePreview)
    {
        var gridExtension = (int)tilePreview.grid.GetExtension();
        for (int x = -gridExtension; x <= gridExtension; x++)
        {

            for (int y = -gridExtension; y <= gridExtension; y++)
            {
                if (tilePreview.GetTile(x, y) >= 0)
                {
                    if (zonePositions.Contains(pos + new Vector2Int(x, y)))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    bool CheckValidPlacement(Vector2Int pos, TilePreview tilePreview)
    {
        var gridExtension = (int)tilePreview.grid.GetExtension();


            for (int x = -gridExtension; x <= gridExtension; x++)
            {
                for (int y = -gridExtension; y <= gridExtension; y++)
                {
                    if (tilePreview.GetTile(x, y) >= 0)
                    {

                        if (!zonePositions.Contains(pos + new Vector2Int(x, y)))
                        {
                            return false;
                        }

                    }
                }
            }
        return true;
    }

    void SetAllZonePositions()
    {
        foreach (var transform in spritesContainer.GetComponentsInChildren<Transform>())
        {
            if (transform.name == "Sprites")
            {
                continue;
            }
            zonePositions.Add(new Vector2Int((int)(transform.position.x -0.5f), (int)(transform.position.y-0.5f)));
        }
    }


    void UpdateZoneTiles()
    {
            foreach (var pos in zonePositions)
            {
                if (solidTm.HasTile((Vector3Int)pos) && !tilePosInZone.Contains(pos))
                {
                    tilePosInZone.Add(pos);
                }
                else if (tilePosInZone.Contains(pos) && !solidTm.HasTile((Vector3Int)pos))
                {
                    tilePosInZone.Remove(pos);
                }
            }
    }


    private void Start()
    {
        canPlace = true;
        nbTriggeredZoneTiles = 0;
    }



    public abstract void RespondToTrigger();

    public abstract void OnBlockRemoved();









//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (canRespondToTrigger)
//        {
//            StartCoroutine(TriggerResponse(collision));
//        }
//        else
//        {
//            return;
//        }
//        canRespondToTrigger = false;
//    }
//
//    public IEnumerator TriggerResponse(Collider2D collider)
//    {
//        yield return new WaitForEndOfFrame();
//        RespondToTrigger(collider.transform.parent.gameObject);
//        canRespondToTrigger = true;
//    }

}











//public class PuzzleZone : ZoneBehavior
//{
//    private Vector2Int[] zoneSquares;
//    private Vector2Int[] puzzleSquares;
//    private int nbBlocksInsideZone = 0;
//    private int wellPlacedBlockCount;
//
//    public void Start()
//    {
//        zoneSquares = new Vector2Int[transform.childCount];
//        puzzleSquares = new Vector2Int[transform.childCount];
//        SurveyArea(zoneSquares, transform, 0);
//    }
//
//    void SurveyArea(Vector2Int[] positionArray, Transform transform, int offset
//    {
//        for (int i = 0; i < transform.childCount;
//        {
//            if (i + offset <= puzzleSquares.Length)
//            {
//                positionArray[i + offset] = new Vector2Int((int)
//            }
//            if (positionArray == puzzleSquares)
//            {
//                CheckForMistake(positionArray[i + offset]);
//                nbBlocksInsideZone++;
//            }
//
//        }
//    }
//
//    void CheckForMistake(Vector2Int position)
//    {
//        bool mistakeMade = true;
//        for (int i = 0; i < transform.childCount;
//        {
//            if (zoneSquares[i] == position)
//            {
//                mistakeMade = false;
//            }
//        }
//
//        if (mistakeMade)
//        {
//            Camera.main.DOShakePosition(0.2f, 10, 7);
//            // Indicate mistake + cannot place any other block
//        }
//        else
//        {
//            wellPlacedBlockCount++;
//        }
//    }
//
//
//    public override void Detect()
//    {
//        SurveyArea(puzzleSquares, collider2D.transform, nbBlocksInsideZone);
//
//        if (wellPlacedBlockCount == transform.childCount)
//        {
//            Debug.Log("Success");
//        }
//    }
//
//
//    public override void OnBlockRemoved()
//    {
//
//    }
//
//
//
//
//}





