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

        [Inject(Id = Constants.ECS_SISTEM_UPDATE_ID)]
        public IEcsSystems updateSystems { get => _updateSystems; set => _updateSystems = value; }
        
        [Inject(Id = Constants.ECS_SISTEM_FIXED_UPDATE_ID)]
        public IEcsSystems fixedUpdate1 { get => _fixedUpdate; set => _fixedUpdate = value; }

        private void Update() => updateSystems.Run();

        private void FixedUpdate() => fixedUpdate1.Run();
    }
}