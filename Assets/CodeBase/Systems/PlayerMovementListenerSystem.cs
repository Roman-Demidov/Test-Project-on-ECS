using codeBase.components;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class PlayerMovementListenerSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var eventFilter = world.Filter<RaycastEventComponent>().End();
            var playerFilter = world.Filter<PlayerComponent>().End();
            var movementPool = world.GetPool<MovementComponent>();
            var raycastPool = world.GetPool<RaycastEventComponent>();

            foreach (var eventEntity in eventFilter)
            {
                foreach (var playerEntity in playerFilter)
                {
                    if (movementPool.Has(playerEntity))
                        movementPool.Del(playerEntity);
                    
                    movementPool.Add(playerEntity).targetPoint = raycastPool.Get(eventEntity).raycastHitPosition;
                }
            }
        }
    }
}