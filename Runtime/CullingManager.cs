using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Culling
{
    public class CullingManager : MonoBehaviour
    {
        public enum CullingTargetsUpdateMode
        {
            /// <summary>
            /// A targets will be updated automatically at every update.
            /// </summary>
            EveryUpdate = 0,

            /// <summary>
            /// To update the targets, you need to explicitly call the update method.
            /// </summary>
            Manual = 1
        }

        CullingGroup cullingGroup;
        BoundingSphere[] boundingSpheres;
        List<ICullingTarget> cullingTargets = new();

        bool boundsDirty = false;

        void Start()
        {
            Ref.Register<CullingManager>(this);

            cullingGroup = new CullingGroup();
            cullingGroup.targetCamera = Camera.main;
            //cullingGroup.onStateChanged = OnStateChanged;
        }

        void OnDestroy()
        {
            cullingGroup.Dispose();

            Ref.Unregister<CullingManager>(this);
        }

        public void Register(ICullingTarget cullingTarget)
        {
            cullingTargets.Add(cullingTarget);
            UpdateBoundingSpheres();
        }

        public void Unregister(ICullingTarget cullingTarget)
        {
            cullingTargets.Remove(cullingTarget);
            UpdateBoundingSpheres();
        }

        public void UpdateBoundingSpheres()
        {
            if (cullingTargets.Count == 0)
            {
                return;
            }

            boundingSpheres = new BoundingSphere[cullingTargets.Count];
            for (int i = 0; i < cullingTargets.Count; i++)
            {
                boundingSpheres[i] = cullingTargets[i].boundingSphere;
            }

            cullingGroup.SetBoundingSpheres(boundingSpheres);
            boundsDirty = false;
        }
    }
}
