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
            var movingPool = world.GetPool<MovementComponent>();

            foreach (var entity in filter)
            {
                if (movingPool.Has(entity))
                    animatorPool.Get(entity).animator.SetBool(Constants.ANIMATION_WALKING, true);
                else
                    animatorPool.Get(entity).animator.SetBool(Constants.ANIMATION_WALKING, false);
            }
        }
    }
}