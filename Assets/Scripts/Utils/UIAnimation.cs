using System;
using UnityEngine;

public static class UIAnimation
{
    public static void EffectOnClick(RectTransform _target, Action _onComplete)
    {
        Debug.Log("Anim");
        LeanTween.scale(_target, new Vector3(1.1f, 1.1f, 1), .1f).setEaseInOutCirc().setLoopPingPong(2).setOnStart(() => { Debug.Log("Satrt"); }).setOnComplete(() =>
        {
            _onComplete();
        });
    }
}
