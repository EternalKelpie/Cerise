using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class HideCollider : MonoBehaviour
{
    private TilemapRenderer tilemapRender;
    private void Awake()
    {
        tilemapRender = GetComponent<TilemapRenderer>();
    }
    private void Start()
    {
        tilemapRender.enabled = false;
    }

}
