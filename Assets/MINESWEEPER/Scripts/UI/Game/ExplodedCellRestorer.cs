using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodedCellRestorer : MonoBehaviour
{
    private Cell _explodedCell;

    public void SetCell(Cell cell)
    {
        if (_explodedCell != null)
            _explodedCell = null;

        _explodedCell = cell;
    }

    public void RestoreCell()
    {
        if (_explodedCell != null)
            _explodedCell.SwitchBombStatus();

        _explodedCell = null;
    }
}
