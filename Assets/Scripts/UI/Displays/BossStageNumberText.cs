using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageNumberText : StageNumberText
{
    private string AlternativePhrase;

    protected override void Init()
    {
        AlternativePhrase = "Boss Stage!";
    }

    private void OnEnable()
    {
        Phrase = $"Boss on stage {Stage.BossNumber + 1}";
        UpdateText();
    }

    private void UpdateText()
    {
        if (Stage.Number == Stage.BossNumber)
            SetText(AlternativePhrase);
        else
            SetText(Phrase);
    }
}
