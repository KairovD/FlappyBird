using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using UnityEngine;

public class DoubleSideLeanTransition : MonoBehaviour, IDoubleSideTransition
{
    [SerializeField]
    LeanPlayer StartTransition;
    [SerializeField]
    LeanPlayer ReverseTransition;


    [HideInInspector]
    public ref LeanPlayer ForwardTransition { get { return ref StartTransition; } }
    [HideInInspector]
    public ref LeanPlayer BackwardTransition { get { return ref ReverseTransition; } }

    public void StartForwardTransition()
    {
        ForwardTransition.Begin();
    }
    public void StartReverseTransition()
    {
        BackwardTransition.Begin();
    }
}

public interface IDoubleSideTransition
{
    public void StartForwardTransition();
    public void StartReverseTransition();
}
