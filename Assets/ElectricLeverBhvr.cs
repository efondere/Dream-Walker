using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using UnityEngine.XR;
using static UnityEditor.PlayerSettings;

public class ElectricLeverBhvr : MonoBehaviour
{
    [HideInInspector]public List<Vector2Int> peripheralPositions;

    private Inputs inputs;
    // 0 is off, 1 is on
    [HideInInspector] public List<ElectricBlockBehavior> electricBlocks;
    [HideInInspector]public int leverState = 0;
    TilemapCollider2D coll;
    private Tilemap solidTm;
    public Tile electricTile;

    public delegate void onChangeSignal();
    public static event onChangeSignal onChangeSignalEvent;

    void Start()
    {
        electricBlocks = new List<ElectricBlockBehavior>();
        inputs = new Inputs();
        inputs.Enable();
        solidTm = GameObject.FindWithTag("TilemapManager").transform.Find("Solid").GetComponent<Tilemap>();
    }

    private void OnEnable()
    {
        var pos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        peripheralPositions.Add(pos + new Vector2Int(-2, -1));
        peripheralPositions.Add(pos + new Vector2Int(1, -1));
        peripheralPositions.Add(pos + new Vector2Int(0, -2));
        peripheralPositions.Add(pos + new Vector2Int(-1, -2));
    }

    private void OnMouseOver()
    {
        transform.GetChild(leverState).gameObject.GetComponent<SpriteRenderer>().DOFade(0.5f, 0.5f);
        if (inputs.Mouse.mouseClick.WasPressedThisFrame())
        {
            ChangeCircuitState();
            ChangeSprite();
            onChangeSignalEvent?.Invoke();
        }
    }


    public void ChangeSprite()
    {
        transform.GetChild(leverState).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        if (leverState == 0)
        {
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void ChangeCircuitState()
    {
        if (leverState == 0)
        {
            leverState = 1;
        }
        else
        {
            leverState = 0;
        }
    }

    private void OnMouseExit()
    {
        transform.GetChild(leverState).gameObject.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
    }
}
