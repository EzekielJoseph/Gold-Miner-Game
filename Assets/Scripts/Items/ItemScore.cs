using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScore : MonoBehaviour
{
    public float hook_Speed;
    public int score_Value;

    private void OnDisable()
    {
        GameplayManager.Instance.DisplayScore(score_Value);
    }
}
