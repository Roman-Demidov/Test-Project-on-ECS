using codeBase.components;
using codeBase.scriptableObjects;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class InitMainCameraSystem : IEcsInitSystem
    {
        private CameraSettings _cameraSettings;
        private Transform _player;

        public InitMainCameraSystem(CameraSettings cameraSettings, Transform player)
        {
            _cameraSettings = cameraSettings;
            _player = player;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var cameraEntity = world.NewEntity();

            var cameraPool = world.GetPool<MainCameraComponent>();
            var transformPool = world.GetPool<TransformComponent>();
            var targerPool = world.GetPool<TransformTargetComponent>();

            transformPool.Add(cameraEntity).transform = Camera.main.transform;
            targerPool.Add(cameraEntity).target = _player;
            cameraPool.Add(cameraEntity);

            ref var cameraComponent = ref cameraPool.Get(cameraEntity);

            cameraComponent.offset = _cameraSettings.offset;
            cameraComponent.cameraSmoothness = _cameraSettings.cameraSmoothness;
            cameraComponent.curVelocity = _cameraSettings.curVelocity;
        }
    }
}