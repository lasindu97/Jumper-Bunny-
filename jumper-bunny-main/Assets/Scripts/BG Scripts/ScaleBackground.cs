using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBackground : MonoBehaviour
{
    
    void Start()
    {
        SpriteRenderer _sRenderer = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector3(1, 1, 1);

        float width = _sRenderer.bounds.size.x;
        float height = _sRenderer.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 tempScale = transform.localScale;
        tempScale.x = worldScreenWidth / width + 0.1f;
        tempScale.y = worldScreenHeight / height + 0.1f;

        transform.localScale = tempScale;
    }
}// class
