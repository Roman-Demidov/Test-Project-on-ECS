using codeBase.components;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class ButtonPressedListenerSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var collisionfilter = world.Filter<EventPlayerCollisionEnterComponent>().End();
            var buttonfilter = world.Filter<ButtonComponent>().Inc<CollisionWithPlayerComponent>().Inc<ColorIdComponent>().End();

            var buttonPressedEventPool = world.GetPool<EventButtonPressedComponent>();
            var colorIdPool = world.GetPool<ColorIdComponent>();

            foreach (var cllisionEntity in collisionfilter)
            {
                foreach (var buttonEntity in buttonfilter)
                {
                    var buttonPressedEventEntity = world.NewEntity();
                    buttonPressedEventPool.Add(buttonPressedEventEntity).colorType = colorIdPool.Get(buttonEntity).colorType;
                }
            }
        }
    }
}