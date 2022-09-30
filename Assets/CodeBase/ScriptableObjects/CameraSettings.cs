using UnityEngine;

namespace codeBase.scriptableObjects
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "SO Settings/Camera Settings")]
    public class CameraSettings : ScriptableObject
    {
        public Vector3 offset;
        public Vector3 curVelocity;
        public float cameraSmoothness;
    }
}
