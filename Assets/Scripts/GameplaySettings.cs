using UnityEngine;
using System;
using System.Collections;

public class GameplaySettings : Settings<GameplaySettings>
{
    public static void DefaultAction( Ball ball ) {}

    public event Action OnArmExtended = DefaultAction;
    public void ReportArmExtended() { OnArmExtended(); }

    public event Action<Ball> OnBallDestroyed = DefaultAction;
    public void ReportBallDestroyed( Ball ball ) { OnBallDestroyed( ball ); }
}

