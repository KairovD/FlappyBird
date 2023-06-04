using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour, IPoolable, IReceiveCycleData
{
    [SerializeField] private float baseDifference = 6.5f;
    private float passageSize;
    [SerializeField]private Transform TopBorder;
    [SerializeField]private Transform BottomBorder;
    private float upperMostPos;
    private float lowerMostPos;
    [SerializeField]private animationcontroller animController;
    public void DisPool()
    {
        
    }

    public void Inst()
    {
        passageSize = GameDifficulty.instance.getValue("PassageSize");
        upperMostPos = GameDifficulty.instance.getValue("UpperBound");
        lowerMostPos = GameDifficulty.instance.getValue("LowerBound");
        TopBorder.localPosition =
            new Vector2(TopBorder.transform.localPosition.x, Random.Range(lowerMostPos, upperMostPos));
        BottomBorder.localPosition =
            new Vector2(BottomBorder.transform.localPosition.x, TopBorder.localPosition.y - passageSize - baseDifference);
    }

    public void passData(int curCycle)
    {
        animController.uptodate(curCycle == LevelManager.instance.record ? "GoldPipe" : "Pipe");
    }
}
