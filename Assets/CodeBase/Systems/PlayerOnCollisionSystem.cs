using codeBase.components;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class PlayerOnCollisionSystem : IEcsRunSystem
    {
        private EcsPool<CollisionWithPlayerComponent> _collisionWithPlayerPool;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<RadiusComponent> _radiusPool;
        private EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            var playerFilter = _world.Filter<PlayerComponent>().Inc<PositionComponent>().Inc<RadiusComponent>().End();
            var objectfilter = _world.Filter<PositionComponent>().Inc<RadiusComponent>().Exc<PlayerComponent>().End();
            
            _positionPool = _world.GetPool<PositionComponent>();
            _radiusPool = _world.GetPool<RadiusComponent>();
            _collisionWithPlayerPool = _world.GetPool<CollisionWithPlayerComponent>();
            
            foreach (var plauerEntity in playerFilter)
            {
                var playerPos = _positionPool.Get(plauerEntity).position;
                var playerRadius = _radiusPool.Get(plauerEntity).radius;

                foreach (var objectEntity in objectfilter)
                {
                    var objectPos = _positionPool.Get(objectEntity).position;
                    var dictance = _radiusPool.Get(objectEntity).radius + playerRadius;
                    bool isCollision = (playerPos - objectPos).sqrMagnitude <= dictance * dictance;

                    if (isCollision)
                    {
                        OnCollision<EventPlayerCollisionEnterComponent>(objectEntity, isCollision);
                    }
                    else
                    {
                        OnCollision<EventPlayerCollisionExitComponent>(objectEntity, isCollision);
                    }
                }
            }
        }

        private void OnCollision<EventType>(int objectEntity, bool isCollision) where EventType : struct
        {
            if (_collisionWithPlayerPool.Has(objectEntity) != isCollision)
            {
                var eventEntity = _world.NewEntity();

                _world.GetPool<EventType>().Add(eventEntity);
                AddOrDel<CollisionWithPlayerComponent>(objectEntity, isCollision);
            }
        }

        private void AddOrDel<T>(int entity, bool add) where T : struct
        {
            var pool = _world.GetPool<T>();
            if (add)
            {
                pool.Add(entity);
            }
            else
            {
                pool.Del(entity);
            }
        }
    }
}