using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    public string SaveState();
    public void LoadState(string jsonString);
    public void LoadByDefault();
}
