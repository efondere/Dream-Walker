using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Pausable))]
public class PopUpManager : MonoBehaviour
{
    public enum PopUpPosition
    {
        World,
        Canvas,
    }

    private struct InstantiatedPopUp
    {
        public GameObject gameObject;
        public PopUpPosition popUpPosition;
        public Vector2 worldPosition;
    }

    private static Canvas _canvas;
    private static Camera _camera;
    private static List<InstantiatedPopUp> _instantiatedPopUps = new List<InstantiatedPopUp>();
    private static GameObject _popUpPrefab;

    [SerializeField] private GameObject popUpPrefab;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _popUpPrefab = popUpPrefab;
        _instantiatedPopUps.Clear();
    }

    private void Update()
    {
        foreach (var p in _instantiatedPopUps)
        {
            if (p.popUpPosition == PopUpPosition.World)
            {
                p.gameObject.transform.GetComponent<RectTransform>().localPosition = WorldToCanvas(p.worldPosition);
            }
        }
    }

    public static int CreatePopUp(string text, Vector2 position, PopUpPosition positionType)
    {
        if (positionType == PopUpPosition.Canvas)
        {
            return CreatePopUpCanvas(text, position);
        }
        return CreatePopUpWorld(text, position);
    }

    // position is in the range [-1, 1]
    private static int CreatePopUpCanvas(string text, Vector2 position)
    {
        _instantiatedPopUps.Add(
            new InstantiatedPopUp
            {
                gameObject = Instantiate(_popUpPrefab, _canvas.transform),
                popUpPosition = PopUpPosition.Canvas,
                worldPosition = new Vector2(0, 0),
            });
        var instanceID = _instantiatedPopUps.Count - 1;
        SetUIElements(instanceID, text, CanvasPercentToPosition(position));
        return instanceID;
    }

    private static int CreatePopUpWorld(string text, Vector2 position)
    {
        _instantiatedPopUps.Add(
            new InstantiatedPopUp
            {
                gameObject = Instantiate(_popUpPrefab, _canvas.transform),
                popUpPosition = PopUpPosition.World,
                worldPosition = position,
            });
        var instanceID = _instantiatedPopUps.Count - 1;
        SetUIElements(instanceID, text, WorldToCanvas(position));
        return instanceID;
    }

    public static void RemovePrefab(int instanceID)
    {
        _instantiatedPopUps.RemoveAt(instanceID);
    }

    public void OnPause()
    {
        foreach (var p in _instantiatedPopUps)
        {
            p.gameObject.SetActive(false);
        }
    }

    public void OnResume()
    {
        foreach (var p in _instantiatedPopUps)
        {
            p.gameObject.SetActive(true);
        }
    }

    private static Vector2 CanvasPercentToPosition(Vector2 percentages)
    {
        var canvasRect = _canvas.GetComponent<RectTransform>().rect;
        return new Vector2(canvasRect.width * percentages.x / 2.0f, canvasRect.height * percentages.y / 2.0f);
    }

    private static Vector2 WorldToCanvas(Vector2 worldPosition)
    {
        return _camera.WorldToScreenPoint(worldPosition);
    }

    private static void SetUIElements(int instanceID, string text, Vector2 canvasPosition)
    {
        var go = _instantiatedPopUps[instanceID].gameObject;
        go.GetComponent<RectTransform>().localPosition = canvasPosition;

        go.transform.GetChild(0).GetComponent<TMP_Text>().text = text;
        var size = go.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta;
        size.x = go.transform.GetChild(0).GetComponent<TMP_Text>().textBounds.size.y + 20;
        go.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = size;
    }
}
