using codeBase.components;
using codeBase.configs;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class AnimationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<AnimatorComponent>().End();
            var animatorPool = world.GetPool<AnimatorComponent>();
            var movementPool = world.GetPool<MovementComponent>();

            foreach (var entity in filter)
            {
                animatorPool.Get(entity).animator.SetBool(Constants.ANIMATION_WALKING, movementPool.Has(entity));
            }
        }
    }
}