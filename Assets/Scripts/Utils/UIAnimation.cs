using System;
using UnityEngine;

public static class UIAnimation
{
    public static void EffectOnClick(RectTransform target, Action onComplete)
    {
        LeanTween.scale(target, target.localScale * 1.1f , .2f)
            .setEaseInOutCirc()
            .setOnComplete(() => { onComplete(); });
    }
}
