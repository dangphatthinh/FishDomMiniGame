using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Athena.MiniGame.Fishdom2.Data;
using DG.Tweening;

namespace Athena.MiniGame.Fishdom2.CameraManager
{
    public class CameraController : MonoBehaviour
    {
        private Camera _camCtrl;
        private float transitionDuration = 1.5f;

        private void Awake()
        {
            _camCtrl = GetComponent<Camera>();
        }
        public void UpdateCameraState(int state, LevelData data)
        {
            float xPos = data.CameraPos[state-1].X;
            float yPos = data.CameraPos[state-1].Y;
            float zPos = data.CameraPos[state-1].Z;
            float size = data.CameraPos[state-1].Size;
            Vector3 target = new Vector3(xPos, yPos, zPos);
            StartCoroutine(DGCameraMove(target, size));
        }
        IEnumerator DGCameraMove(Vector3 target, float size)
        {
            _camCtrl.DOOrthoSize(size, transitionDuration).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.5f);
            transform.DOMove(target, transitionDuration).SetEase(Ease.OutQuad);
        }
    }

}
