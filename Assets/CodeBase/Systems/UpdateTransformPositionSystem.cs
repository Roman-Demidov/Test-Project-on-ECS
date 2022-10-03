using codeBase.components;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class UpdateTransformPositionSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<TransformComponent>().Inc<PositionComponent>().End();
            var transformPool = world.GetPool<TransformComponent>();
            var positionPool = world.GetPool<PositionComponent>();

            foreach (var entity in filter)
            {
                transformPool.Get(entity).transform.position = positionPool.Get(entity).position;
            }
        }
    }
}