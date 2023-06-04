using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using UnityEngine;

public interface IGameLogic: IPoolable
{
    public void changeState(GameState newState);
}
public interface IPoolable
{
    public void DisPool();
    public void Inst();
}
[System.Serializable]
public struct spriteset
{
    public Sprite defaultSprite;
    public AnimationSet[] animations;
}
[System.Serializable]
public struct KeyFrame
{
    public Sprite sprite;
    public int frameTime;
}
[System.Serializable]
public struct AnimationSet
{
    public string animName;
    public KeyFrame[] frames;
}
[System.Serializable]
public class TransitionSet
{
    public string index;
    public LeanPlayer transitions;
    [Tooltip("In Seconds")]
    public float TransitionLength;
}
public enum GameState
{
    Play,
    Pause
}

public interface IReceiveCycleData
{
    public void passData(int curCycle);
}