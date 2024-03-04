using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    private Transform _tr;
    private TextMeshPro _text;

    private float speed = 3.0f;
    private int lifetime = 120; // Fixed frames


    private void Awake()
    {
        _tr = GetComponent<Transform>();
        _text = GetComponent<TextMeshPro>();
    }

    public void SetProperties(Vector3 pos, string text, Color color)
    {
        _tr.position = new Vector3(pos.x, 10, pos.z);
        _text.text = text;
        _text.color = color;
    }

    private void FixedUpdate()
    {
        _tr.position += speed * Time.fixedDeltaTime * Vector3.forward;

        lifetime--;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
