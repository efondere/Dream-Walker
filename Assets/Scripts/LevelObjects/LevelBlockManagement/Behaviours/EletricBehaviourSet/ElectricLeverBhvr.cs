using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ElectricLeverBhvr : MonoBehaviour
{
    [HideInInspector] public List<Vector2Int> peripheralPositions;

    // 0 is off, 1 is on
    [HideInInspector] public int leverState = 0;
    public Tile electricTile;

    public delegate void onChangeSignal();
    public static event onChangeSignal onChangeSignalEvent;

    private bool _wasMouseJustPressed = false;

    void Start()
    {
        //TODO: remove the callback on delete
        InputManager.objectClickEvent += OnObjectClick;
    }

    private void LateUpdate()
    {
        _wasMouseJustPressed = false;
    }

    private void OnEnable()
    {
        var pos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        peripheralPositions.Add(pos + new Vector2Int(-2, -1));
        peripheralPositions.Add(pos + new Vector2Int(1, -1));
        peripheralPositions.Add(pos + new Vector2Int(0, -2));
        peripheralPositions.Add(pos + new Vector2Int(-1, -2));
    }

    private void OnObjectClick(bool wasPressed)
    {
        if (wasPressed)
        {
            _wasMouseJustPressed = true;
        }
    }

    private void OnMouseOver()
    {
        transform.GetChild(leverState).gameObject.GetComponent<SpriteRenderer>().DOFade(0.5f, 0.5f);
        if (_wasMouseJustPressed)
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
