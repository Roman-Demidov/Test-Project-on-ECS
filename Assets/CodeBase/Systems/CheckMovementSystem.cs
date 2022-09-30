using codeBase.components;
using codeBase.configs;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class CheckMovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<TransformComponent>().Inc<MovementComponent>().End();

            var movementPool = world.GetPool<MovementComponent>();
            var transformPool = world.GetPool<TransformComponent>();

            foreach (var entity in filter)
            {
                ref var movementEntity = ref movementPool.Get(entity);
                ref var tramsformEntity = ref transformPool.Get(entity);

                if (Vector3.Distance(movementEntity.targetPoint, tramsformEntity.transform.position) <= Constants.MIN_MOVEMENT_DISTANCE)
                {
                    movementPool.Del(entity);
                }
            }
        }
    }
}