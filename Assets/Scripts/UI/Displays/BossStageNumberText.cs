using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageNumberText : StageNumberText
{
    private void Start()
    {
        if (Stage.Number == Stage.BossNumber)
            SetText("Boss Stage!");
        else
            SetText($"Boss on {Stage.BossNumber + 1}");
    }
}
