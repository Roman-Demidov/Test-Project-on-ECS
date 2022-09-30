using codeBase.components;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<MainCameraComponent>().Inc<TransformComponent>().Inc<TransformTargetComponent>().End();
            var cameraPool = world.GetPool<MainCameraComponent>();
            var transformPool = world.GetPool<TransformComponent>();
            var targetPool = world.GetPool<TransformTargetComponent>();

            foreach (var entity in filter)
            {
                ref var cameraComponent = ref cameraPool.Get(entity);
                ref var transformComponent = ref transformPool.Get(entity);
                ref var targetComponent = ref targetPool.Get(entity);

                Vector3 currentPosition = transformComponent.transform.position;
                Vector3 targetPoint = targetComponent.target.position + cameraComponent.offset;

                transformComponent.transform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref cameraComponent.curVelocity, cameraComponent.cameraSmoothness);
            }
        }
    }
}