using UnityEngine;
using System.Collections;

public class ScreenHelper : MonoBehaviour 
{

    public static ScreenHelper _instance;
    public static ScreenHelper Instance
    {
        get { return _instance; }
    }

    public Camera cam;
    public float border;

    private Rect _rect;
    public Rect Rect
    {
        get { return _rect; }
    }

    public void Awake()
    {
        _instance = this;
        if (cam == null) cam = Camera.main;
        Vector2 min = cam.ScreenToWorldPoint(Vector3.zero);
        Vector2 max = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        _rect = Rect.MinMaxRect(min.x - border, min.y - border, max.x + border, max.y + border);
    }

    public bool IsOnScreen(Vector3 pos)
    {
        return _rect.Contains(pos);
    }

}
