using UnityEngine;

namespace codeBase.scriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "SO Settings/Player Settings")]
    public class PlayerSettings: ScriptableObject
    {
        public float moveSpeed;
        public float radius;
    }
}
