using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class GameRules : MonoBehaviour
{
    public UnityAction finishLinePassed;
    public UnityAction gameReset;

    List<GameObject> objectsToDestroyOnReset = new List<GameObject>();

    [SerializeField]
    Text display_Text;

    int amountOfLapsPassed = 0;
    
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Reset"))
        {
            gameReset.Invoke();
        }
    }

    void OnEnable()
    {
        finishLinePassed += OnFinishLinePassed;
        gameReset += OnGameReset;
    }

    void OnDisable()
    {
        finishLinePassed -= OnFinishLinePassed;
        gameReset -= OnGameReset;
    }

    void OnFinishLinePassed()
    {
        amountOfLapsPassed++;
        display_Text.text = "Laps count: " + amountOfLapsPassed;
    }

    void OnGameReset()
    {
        amountOfLapsPassed = 0;
        display_Text.text = "Laps count: 0";
        for (int i = 0, length = objectsToDestroyOnReset.Count; i < length; i++)
        {
            Destroy(objectsToDestroyOnReset[i]);
        }
        objectsToDestroyOnReset.Clear();
    }

    public void AddItemToDestroyList(GameObject objectToAdd)
    {
        objectsToDestroyOnReset.Add(objectToAdd);
    }

}
