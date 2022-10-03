using codeBase.components;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class ButtonReleaseListenerSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var eventfilter = world.Filter<EventPlayerCollisionExitComponent>().End();
            var buttonfilter = world.Filter<ButtonComponent>().Inc<ColorIdComponent>().Exc<CollisionWithPlayerComponent>().End();

            var buttonReleaseEventPool = world.GetPool<EventButtonReleaseComponent>();
            var colorIdPool = world.GetPool<ColorIdComponent>();

            foreach (var eventEntity in eventfilter)
            {
                foreach (var buttonEntity in buttonfilter)
                {
                    var buttonReleaseEventEntity = world.NewEntity();
                    buttonReleaseEventPool.Add(buttonReleaseEventEntity).colorType = colorIdPool.Get(buttonEntity).colorType;
                }
            }
        }
    }
}