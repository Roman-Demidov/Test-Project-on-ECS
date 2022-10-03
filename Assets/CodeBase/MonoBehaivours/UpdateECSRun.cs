using codeBase.configs;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace codeBase.monoBehaivours
{
    public class UpdateECSRun : MonoBehaviour
    {
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdate;

        public void init(IEcsSystems updateSystems, IEcsSystems fixedUpdate)
        {
            _updateSystems = updateSystems;
            _fixedUpdate = fixedUpdate;
        }

        private void Update() => _updateSystems.Run();

        private void FixedUpdate() => _fixedUpdate.Run();
    }
}