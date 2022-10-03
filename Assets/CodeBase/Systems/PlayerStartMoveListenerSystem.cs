using codeBase.components;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class PlayerStartMoveListenerSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var eventFilter = world.Filter<EventRaycastHitGroundComponent>().End();
            var playerFilter = world.Filter<PlayerComponent>().End();

            var movementPool = world.GetPool<MovementComponent>();
            var raycastPool = world.GetPool<EventRaycastHitGroundComponent>();
            var playerPool = world.GetPool<PlayerComponent>();

            foreach (var eventEntity in eventFilter)
            {
                foreach (var playerEntity in playerFilter)
                {
                    if (movementPool.Has(playerEntity))
                        movementPool.Del(playerEntity);
                    
                    movementPool.Add(playerEntity);
                    ref var movementComponent = ref movementPool.Get(playerEntity);

                    movementComponent.newPosition = raycastPool.Get(eventEntity).raycastHitPosition;
                    movementComponent.moveSpeed = playerPool.Get(playerEntity).moveSpeed;
                }
            }
        }
    }
}