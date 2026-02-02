using UnityEngine;
using UnityEngine.Events;

namespace Culling
{
    [ExecuteAlways]
    public class CullingTarget : MonoBehaviour, ICullingTarget
    {
        [SerializeField]
        CullingGroupDefinition cullingGroup;

        public TransformUpdateMode boundingSphereUpdateMode => TransformUpdateMode.Dynamic;
        public BoundingSphere boundingSphere { get; private set; }
        public UnityEngine.CullingGroup.StateChanged onStateChanged { get; set; }

        public bool IsVisible() => false;
        public int GetDistance() => 0;
        public BoundingSphere UpdateAndGetBoundingSphere() => boundingSphere;
    }
}
