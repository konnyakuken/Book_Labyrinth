using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Dice
{

    public static class DiceThrowerExt
    {
        public static TweenerCore<float, float, FloatOptions> DOStayRotate(this Transform transform, Vector3 axis, float degree, float duration)
        {
            var q = Quaternion.identity;
            return DOTween.To(
                () =>
                {
                    q = transform.rotation;
                    return 0;
                },
                value =>
                {
                    transform.rotation = q * Quaternion.Euler(axis * value);
                },
                degree, duration
            );
        }
    }


    public class DiceThrower : MonoBehaviour
    {
        //各目の面の設定。y+面が1、x-面が2、z-面が3...に設定済み。サイコロモデルによって再設定可。
        [SerializeField]
        private Vector2[] pipRotates = new[]
        {
            new Vector2(0,0),     //1の目の面。up(y+)方向
            new Vector2(270, 90), //2の面。まずup(y+)を軸に90度回転させて、right(x+)を軸に270度回転させた面
            new Vector2(90, 0),   //3の面。right(x+)方向を軸に90度回転させた面
            new Vector2(270, 0),  //以下同様
            new Vector2(90, 90),
            new Vector2(180,0),
        };

        public delegate void OnComplete(GameObject gameObject, int pip);

        public void Throw(int pip, float degree, OnComplete callBack = null)
        {
            if (pip < 1 || 6 < pip) throw new ArgumentException($"pip should be specified between 1 and 6 => {pip}");
            var t = transform;
            var diceRotate = pipRotates[pip - 1];
            var dir = Quaternion.Euler(0, degree, 0) * Vector3.forward;
            var rotateAxis = Vector3.right;

            t.eulerAngles = new Vector3(0, degree, 0) + new Vector3(0, diceRotate.y);
            var seq = DOTween.Sequence();
            var pos = t.position + dir;
            seq.Append(t.DOJump(pos, 2, 1, 1).SetEase(Ease.Linear));
            seq.Join(t.DOStayRotate(rotateAxis, 360 * 3, 1).SetEase(Ease.Linear));

            pos += dir * 2;
            seq.Append(t.DOJump(pos, 1, 1, 0.5f).SetEase(Ease.Linear));
            seq.Join(t.DOStayRotate(rotateAxis, 360, 0.5f).SetEase(Ease.Linear));

            var xRotate = diceRotate.x;
            if (xRotate < 180) xRotate += 360;
            var rate = xRotate / 360f;
            pos += dir * 4 * rate;
            seq.Append(t.DOMove(pos, 1f * rate));
            seq.Join(t.DOStayRotate(rotateAxis, xRotate, 1f * rate));
            seq.OnComplete(() => { callBack?.Invoke(gameObject, pip); });
        }




        //使い方
        //Throw(3, 0); //forward(z+)方向に投げて3が出る
        //Throw(1, 90, (o, pip) => Debug.Log(pip)); //right(x+)方向に投げて1が出る。サイコロが止まったらコンソールに1を出す。
    }
}