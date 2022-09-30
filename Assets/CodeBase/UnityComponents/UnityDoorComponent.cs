using codeBase.configs;
using UnityEngine;

namespace codeBase.unityComponents
{
    public class UnityDoorComponent : MonoBehaviour
    {
        public Transform doorTransform;
        public Transform openTransform;
        public Transform closeTransform;
        public float moveSpeed;
        public ColorType colorType;
    }
}