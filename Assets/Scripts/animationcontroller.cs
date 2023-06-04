using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationcontroller : MonoBehaviour
{
    [SerializeField]private AnimationSet[] animations;
    public SpriteRenderer[] subs;
    [Space] public TransitionSet[] availableTransitions;
    private bool transitionPlays;
    [SerializeField]private bool useSpriteBatch;
    private Sprite original_sprite;
    private bool animPlays;
    private string name_;
    private int transitionPriority = 0;
    #region localVariables
    
    private int f;
    private int t;
    private int k;
    private int l;
    private int i;
    private int ii;
    private int m;
    private int fr = 0;
    private int z = 0;
    private int s;
    private int j;
    #endregion
    
    public void uptodate(string objectName)
    {
        name_ = objectName;
        if (useSpriteBatch == false)
        {
            original_sprite = sprite_container.instance.GetDefaultSprite(objectName);
            for (i = 0; i < subs.Length; i++)
            {
                subs[i].sprite = original_sprite;
            }
        }
        animations = sprite_container.instance.request(objectName);
    }
    
    public void PlayAnimation(string animName)
    { 
        StartCoroutine(playAnimation(animName));
    }
    public void PlayAnimation(string animName, Action result)
    {
        StartCoroutine(playAnimation(animName, result));
        
    }
    private IEnumerator playAnimation(string animationName)
    {
        if (animPlays == true) yield break;
        for (i = 0; i < animations.Length; i++)
        {
            if (animations[i].animName != animationName) continue;
            animPlays = true;
            for (m = 0; m < animations[i].frames.Length; m++)
            {
                for (s = 0; s < subs.Length; s++)
                {
                    subs[s].sprite = animations[i].frames[m].sprite;
                }
                for (fr = 0; fr < animations[i].frames[m].frameTime; fr++)
                {
                    yield return null;
                }
            }
            for (s = 0; s < subs.Length; s++)
            {
                subs[s].sprite = original_sprite;
            }

            animPlays = false;
            yield break;
        }
    }
    private IEnumerator playAnimation(string animationName, Action result)
    {
        if (animPlays == true) yield break;
        for (i = 0; i < animations.Length; i++)
        {
            if (animations[i].animName != animationName) continue;
            animPlays = true;
            for (m = 0; m < animations[i].frames.Length; m++)
            {
                for (s = 0; s < subs.Length; s++)
                {
                    subs[s].sprite = animations[i].frames[m].sprite;
                }
                        
                for (fr = 0; fr < animations[i].frames[m].frameTime; fr++)
                {
                    yield return null;
                }
            }
            for (s = 0; s < subs.Length; s++)
            {
                subs[s].sprite = original_sprite;
            }

            animPlays = false;
            result.Invoke();
            yield break;
        }
    }
    /*public void playTransition(string trName, int priority)
    {
        if (transitionPlays == true && priority <= transitionPriority)
            return;
        for (f = 0; f < availableTransitions.Length; f++)
        {
            if (availableTransitions[f].index != trName) continue;
            transitionPriority = priority;
            availableTransitions[f].transitions.Begin();
            StopAllCoroutines();
            StartCoroutine(transitionDelay(availableTransitions[f].TransitionLength));
        }
    }
    IEnumerator transitionDelay(float time)
    {
        transitionPlays = true;
        yield return new WaitForSeconds(time);
        transitionPlays = false;
    }*/
}