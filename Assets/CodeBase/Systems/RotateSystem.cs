using codeBase.components;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class RotateSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<RotationComponent>().Inc<MovementComponent>().Inc<PositionComponent>().End();
            var rotationPool = world.GetPool<RotationComponent>();
            var movementPool = world.GetPool<MovementComponent>();
            var positionPool = world.GetPool<PositionComponent>();

            foreach (var entity in filter)
            {
                ref var positionComponent = ref positionPool.Get(entity);
                ref var movementComponent = ref movementPool.Get(entity);

                Vector3 currentPos = positionComponent.position;
                Vector3 targetPos = movementComponent.newPosition;
                Vector3 movementDirection = (targetPos - currentPos).normalized;

                rotationPool.Get(entity).rotation = Quaternion.LookRotation(movementDirection);
            }
        }
    }
}