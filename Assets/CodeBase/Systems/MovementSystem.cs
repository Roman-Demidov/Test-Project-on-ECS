using codeBase.components;
using codeBase.configs;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class MovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<PositionComponent>().Inc<MovementComponent>().End();
            var positionPool = world.GetPool<PositionComponent>();
            var movementPool = world.GetPool<MovementComponent>();

            foreach (var entity in filter)
            {
                ref var positionComponent = ref positionPool.Get(entity);
                ref var movementComponent = ref movementPool.Get(entity);

                Vector3 currentPos = positionComponent.position;
                Vector3 targetPos = movementComponent.newPosition;
                float moveSpeed = movementComponent.moveSpeed * Constants.DELTATIME;

                positionComponent.position = Vector3.MoveTowards(currentPos, targetPos, moveSpeed);
            }
        }
    }
}