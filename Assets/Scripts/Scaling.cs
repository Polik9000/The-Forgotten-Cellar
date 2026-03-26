using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public int Width;
    public int Height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] allobjects = UnityEngine.Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        gameObjects.AddRange(allobjects);
        Width = Screen.width;
        Height = Screen.height;
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.transform.localScale = new Vector2 (gameObject.transform.localScale.x*Width/16,gameObject.transform.localScale.y*Width/9);
        }
    }
}
