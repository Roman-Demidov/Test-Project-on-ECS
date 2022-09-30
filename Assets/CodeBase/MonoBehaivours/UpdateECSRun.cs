using codeBase.components;
using codeBase.scriptableObjects;
using codeBase.systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;
using Zenject;

namespace codeBase.monoBehaivours
{
    public class UpdateECSRun : MonoBehaviour
    {
        private IEcsSystems _updateSystems;

        [Inject]
        private void constructor(IEcsSystems updateSystems) => _updateSystems = updateSystems;

        private void Update() => _updateSystems.Run();
    }
}