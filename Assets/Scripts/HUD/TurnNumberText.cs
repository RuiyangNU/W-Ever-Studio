using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnNumberText : MonoBehaviour
{
    private GameManager gameManager;

    private TextMeshProUGUI _text;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = "Turn " + gameManager.turnNumber;
    }
}
