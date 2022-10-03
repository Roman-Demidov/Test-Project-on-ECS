using codeBase.components;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class UpdateTransformRotationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<TransformComponent>().Inc<RotationComponent>().End();
            var transformPool = world.GetPool<TransformComponent>();
            var rotationPool = world.GetPool<RotationComponent>();

            foreach (var entity in filter)
            {
                transformPool.Get(entity).transform.rotation = rotationPool.Get(entity).rotation;
            }
        }
    }
}