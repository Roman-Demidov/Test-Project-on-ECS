using codeBase.components;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<PlayerComponent>().Inc<MovementComponent>().Inc<TransformComponent>().End();
            var playerPool = world.GetPool<PlayerComponent>();
            var transformPool = world.GetPool<TransformComponent>();
            var movementPool = world.GetPool<MovementComponent>();

            foreach (var entity in filter)
            {
                ref var playerEntity = ref playerPool.Get(entity);
                ref var transformEntity = ref transformPool.Get(entity);
                ref var movementEntity = ref movementPool.Get(entity);

                Vector3 currentPos = transformEntity.transform.position;
                Vector3 targetPos = movementEntity.targetPoint;
                Vector3 movementDirection = (targetPos - currentPos).normalized;

                transformEntity.transform.rotation = Quaternion.LookRotation(movementDirection);
                transformEntity.transform.position = Vector3.MoveTowards(currentPos, targetPos, playerEntity.moveSpeed * Time.deltaTime);
            }
        }
    }
}