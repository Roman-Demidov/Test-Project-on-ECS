using codeBase.components;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class PlayerCollisionEnterSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var playerfilter = world.Filter<PlayerComponent>().Inc<PositionComponent>().Inc<RadiusComponent>().End();
            var objectfilter = world.Filter<PositionComponent>().Inc<RadiusComponent>().Exc<CollisionWithPlayerComponent>().End();
            var positionPool = world.GetPool<PositionComponent>();
            var radiusPool = world.GetPool<RadiusComponent>();
            var eventPool = world.GetPool<EventPlayerCollisionEnterComponent>();
            var collisionWithPlayerPool = world.GetPool<CollisionWithPlayerComponent>();

            foreach (var plauerEntity in playerfilter)
            {
                var playerPos = positionPool.Get(plauerEntity).position;
                var playerRadius = radiusPool.Get(plauerEntity).radius;

                foreach (var objectEntity in objectfilter)
                {
                    var objectPos = positionPool.Get(objectEntity).position;
                    var dictance = radiusPool.Get(objectEntity).radius + playerRadius;

                    if ((playerPos - objectPos).sqrMagnitude <= dictance * dictance)
                    {
                        var eventEntity = world.NewEntity();
                        
                        eventPool.Add(eventEntity);
                        collisionWithPlayerPool.Add(objectEntity);
                    }
                }
            }
        }
    }
}