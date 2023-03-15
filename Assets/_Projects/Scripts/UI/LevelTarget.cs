using UnityEngine;

namespace UI
{
    public class LevelTarget : MonoBehaviour
    {
        public UI.XTextMesh Count;

        [SerializeField] GameObject[] _targetIcons;

        public void Init()
        {
        }

        public void UpdateCount(int count)
        {
            Count.Value = count.ToString();
        }
    }
}