using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace codeBase.scriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "SO Settings/Player Settings")]
    public class PlayerSettings: ScriptableObject
    {
        public float moveSpeed;
    }
}
