using codeBase.components;
using codeBase.configs;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class RemoveMovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<TransformComponent>().Inc<MovementComponent>().End();
            var transformPool = world.GetPool<TransformComponent>();
            var movementPool = world.GetPool<MovementComponent>();

            foreach (var entity in filter)
            {
                ref var transformComponent = ref transformPool.Get(entity);
                ref var movementComponent = ref movementPool.Get(entity);

                Vector3 currentPos = transformComponent.transform.position;
                Vector3 targetPos = movementComponent.newPosition;
                
                if((currentPos - targetPos).sqrMagnitude <= Constants.MIN_MOVEMENT_DISTANCE * Constants.MIN_MOVEMENT_DISTANCE)
                {
                    movementPool.Del(entity);
                }
            }
        }
    }
}