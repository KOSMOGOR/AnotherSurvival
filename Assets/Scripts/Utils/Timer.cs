using System;
using UnityEngine;

[Serializable]
public class Timer : ICloneable
{
    [field: SerializeField] public float MaxTime { get; protected set; } = 0f;
    [field: SerializeField] public bool CanOvercount { get; protected set; } = false;

    public float CurrentTime { get; protected set; } = 0f;

    /// <summary>
    /// Has Timer counted to MaxTime?
    /// </summary>
    public bool IsComplete => CurrentTime >= MaxTime;
    /// <summary>
    /// Not <see cref="IsComplete"/>
    /// </summary>
    public bool IsIncomplete => CurrentTime < MaxTime;

    public Timer() : this(0f) { }
    public Timer(float maxTime, bool canOvercount = false)
    {
        MaxTime = Mathf.Max(maxTime, 0);
        CanOvercount = canOvercount;
    }

    /// <summary>
    /// Increments <see cref="CurrentTime"/>. It will always be <c>&gt;= 0</c>
    /// (and <c>&lt;= MaxTime</c>, if it can't overcount).
    /// </summary>
    public void Update(float deltaTime)
    {
        CurrentTime += deltaTime;
        if (CurrentTime < 0f) CurrentTime = 0f;
        else if (!CanOvercount && CurrentTime > MaxTime) CurrentTime = MaxTime;
    }

    /// <summary>
    /// Resets <see cref="CurrentTime"/> to 0.
    /// </summary>
    /// <param name="toZero"> If set to false and timer overcounted, then just subtracts <see cref="MaxTime"/>. </param>
    public void Reset(bool toZero = true)
    {
        if (IsIncomplete || toZero || !CanOvercount) CurrentTime = 0f;
        else CurrentTime -= MaxTime;
    }

    /// <summary>
    /// Returns current complete status and resets if completed.
    /// </summary>
    /// <returns></returns>
    public bool IsCompleteAndReset()
    {
        if (IsComplete)
        {
            Reset();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets new max time and changes current if needed
    /// (<see cref="ScaleCurrentTime.Add"/> will add difference between max times and <see cref="ScaleCurrentTime.Scale"/> will... scale it).
    /// </summary>
    /// <param name="newMaxTime"></param>
    /// <param name="scaleCurrentTime"></param>
    /// <returns></returns>
    public float SetMaxTime(float newMaxTime, ScaleCurrentTime scaleCurrentTime = ScaleCurrentTime.None)
    {
        float oldMaxTime = MaxTime;
        MaxTime = Mathf.Max(newMaxTime, 0);
        switch (scaleCurrentTime)
        {
            case ScaleCurrentTime.None:
                if (!CanOvercount) CurrentTime = Mathf.Min(CurrentTime, MaxTime);
                break;
            case ScaleCurrentTime.Add:
                CurrentTime = Mathf.Max(CurrentTime + (MaxTime - oldMaxTime), 0);
                break;
            case ScaleCurrentTime.Scale:
                CurrentTime = MaxTime != 0 ? CurrentTime * MaxTime / oldMaxTime : 0;
                break;
        }
        return CurrentTime;
    }

    public void SetCanOvercount(bool newCanOvercount)
    {
        CanOvercount = newCanOvercount;
        if (!CanOvercount) CurrentTime = Mathf.Min(CurrentTime, MaxTime);
    }

    public object Clone()
    {
        Timer timer = new(MaxTime)
        {
            CurrentTime = CurrentTime
        };
        return timer;
    }

    public enum ScaleCurrentTime
    {
        None,
        Add,
        Scale
    }
}
