using System;
using UnityEngine;

[Serializable]
public enum Step
{
    Pee,
    Coffee,
    WashMouth,
    Exit
}

public class Objectives : MonoBehaviour
{
    [SerializeField] private Step step = Step.Pee;

    public Step GetStep()
    {
        return step;
    }
}
