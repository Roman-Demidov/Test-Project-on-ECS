using UnityEngine;

namespace codeBase.scriptableObjects
{
    [CreateAssetMenu(fileName = "GameDara", menuName = "Game data/Game data")]
    public class GameDate : ScriptableObject
    {
        public LayerMask walkingLayer;
    }
}
