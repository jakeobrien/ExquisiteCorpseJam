using UnityEngine;
using System;
using System.Collections;

public class GameplaySettings : Settings<GameplaySettings>
{
    public event Action OnArmExtended = DefaultAction;
    public void ReportArmExtended() { OnArmExtended(); }
}

