using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridLayoutWidthScaler : MonoBehaviour
{
    [SerializeField] private Vector2 _defaultDisplaySize;

    private GridLayoutGroup _layoutGroup;
    private Vector2 _defaultCellSize;

    private void Awake()
    {
        _layoutGroup = GetComponent<GridLayoutGroup>();
        _defaultCellSize = _layoutGroup.cellSize;
    }

    private void Update()
    {
        float spacingsWidth = (_layoutGroup.constraintCount - 1) * _layoutGroup.spacing.x;
        float defaultRatio = (_defaultDisplaySize.x - spacingsWidth) / _defaultDisplaySize.y;
        float displayRatio = (float)Camera.main.pixelWidth / Camera.main.pixelHeight;
        float width = _defaultCellSize.x * displayRatio / defaultRatio;

        if (displayRatio < defaultRatio)
            _layoutGroup.cellSize = new Vector2(width, _layoutGroup.cellSize.y);
    }
}
