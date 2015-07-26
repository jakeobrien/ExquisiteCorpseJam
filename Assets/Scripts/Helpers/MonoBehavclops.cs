using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MonoBehavclops : MonoBehaviour 
{

    public static bool DoImmediately = true;
    public static bool DoAfterFirstInterval = false;
    public static bool Smoothed = true;
    public static float Forever = -1f;
    public static float NoVariance = 0f;
    
    public delegate void RepeatAction();
    public delegate void TweenAction(float t);

    private Dictionary<Type,Component> _cachedComponents;

    private GameObject _gameObject;
    public GameObject GameObject
    {
        get
        {
            if (_gameObject == null) _gameObject = gameObject;
            return _gameObject;
        }
    }

    public T Cached<T>() where T : Component
    {
        if (_cachedComponents == null) _cachedComponents = new Dictionary<Type,Component>();
        if (_cachedComponents.ContainsKey(typeof(T))) return (T)_cachedComponents[typeof(T)];
        T component = GetComponent<T>();
        _cachedComponents.Add(typeof(T),component);
        return component;
    }

    public Vector3 LocalPosition
    { 
        get { return Cached<Transform>().localPosition; } 
        set { Cached<Transform>().localPosition = value; }
    }

    public Vector3 Position
    { 
        get { return Cached<Transform>().position; } 
        set { Cached<Transform>().position = value; }
    }

    public Vector3 LocalEulerAngles
    { 
        get { return Cached<Transform>().localEulerAngles; } 
        set { Cached<Transform>().localEulerAngles = value; }
    }

    public Vector3 EulerAngles
    { 
        get { return Cached<Transform>().eulerAngles; } 
        set { Cached<Transform>().eulerAngles = value; }
    }

    public Vector3 LocalScale
    { 
        get { return Cached<Transform>().localScale; } 
        set { Cached<Transform>().localScale = value; }
    }

    public Vector3 LossyScale
    { 
        get { return Cached<Transform>().lossyScale; } 
    }

    public float LocalRotation2D
    {
        get { return LocalEulerAngles.z; }
        set { LocalEulerAngles = new Vector3(LocalEulerAngles.x, LocalEulerAngles.y, value); }
    }
    
    public float Rotation2D
    {
        get { return EulerAngles.z; }
        set { EulerAngles = new Vector3(EulerAngles.x, EulerAngles.y, value); }
    }

    public Vector2 LocalScale2D
    {
        get { return new Vector2(LocalScale.x, LocalScale.y); }
        set { LocalScale = new Vector3(value.x, value.y, LocalScale.z); }
    }

    public float SymmetricLocalScale2D
    {
        get { return LocalScale.x; }
        set { LocalScale = new Vector3(value, value, LocalScale.z); }
    }

    public void LocalMove(Vector3 newPosition, float time, Action completion) 
    {
        LocalMove(newPosition, time, false, completion);
    }

    public void LocalMove(Vector3 newPosition, float time, bool smoothed = false, Action completion = null) 
    {
        var origPosition = LocalPosition;
        Tween(time, smoothed, (t)=> {
            LocalPosition = Vector3.Lerp(origPosition, newPosition, t);
        }, completion);
    }

    protected void Delay(float delay, Action action)
    {
        if (!gameObject.activeSelf) return;
        StartCoroutine(DelayEnumerator(delay, action));
    }

    private IEnumerator DelayEnumerator(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }


    protected void Repeat(float interval, RepeatAction action) 
    { 
        Repeat(interval, NoVariance, Forever, DoImmediately, action); 
    }

    protected void Repeat(float interval, float variance, RepeatAction action) 
    {
        Repeat(interval, variance, Forever, DoImmediately, action); 
    }
    
    protected void Repeat(float interval, bool doImmediately, RepeatAction action) 
    {
        Repeat(interval, NoVariance, Forever, doImmediately, action); 
    }

    protected void Repeat(float interval, float variance, bool doImmediately, RepeatAction action) 
    { 
        Repeat(interval, variance, Forever, doImmediately, action); 
    }

    protected void Repeat(float interval, float variance, float duration, bool doImmediately, RepeatAction action)
    {
        if (!GameObject.activeSelf) return;
        StartCoroutine(RepeatEnumerator(interval, variance, duration, doImmediately, action));
    }

    private IEnumerator RepeatEnumerator(float interval, float variance, float duration, bool doImmediately, RepeatAction action)
    {
        if (doImmediately) action();
        float startTime = Time.time;
        float variedInterval = interval;
        float minInterval = Mathf.Max(interval - Mathf.Abs(variance), 0f);
        float maxInterval = interval + Mathf.Abs(variance);
        while (duration < 0f || Time.time - startTime <= duration) {
            if (variance != 0f) variedInterval = UnityEngine.Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(variedInterval);
            action();
        }
    }

    // TODO: add ease types
    protected void Tween(float duration, Action<float> action)
    {
        Tween(duration, false, action, null);
    }

    protected void Tween(float duration, bool smoothed, Action<float> action)
    {
        Tween(duration, smoothed, action, null);
    }

    protected void Tween(float duration, Action<float> action, Action completion)
    {
        Tween(duration, false, action, completion);
    }

    protected void Tween(float duration, bool smoothed, Action<float> action, Action completion)
    {
        if (!gameObject.activeSelf) return;
        if (duration <= 0f) return;
        StartCoroutine(TweenEnumerator(duration, smoothed, action, completion));
    }

    private IEnumerator TweenEnumerator(float duration, bool smoothed, Action<float> action, Action completion)
    {
        float time = 0f;
        while (time < duration) {
            float t = time/duration;
            if (smoothed) t = t * t * (3 - 2 * t);
            action(t);
            time += Time.deltaTime;
            yield return null;
        }
        action(1f);
        if (completion != null) completion();
    }

}
