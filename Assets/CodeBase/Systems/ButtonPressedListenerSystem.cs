using codeBase.components;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class CheckButtonPressedSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var collisionfilter = world.Filter<EventOnCollisionEnterComponent>().End();
            var buttonfilter = world.Filter<ButtonComponent>().Inc<TransformComponent>().Inc<ColorIdComponent>().End();

            var collisionPool = world.GetPool<EventOnCollisionEnterComponent>();
            var transformPool = world.GetPool<TransformComponent>();
            var colorIdPool = world.GetPool<ColorIdComponent>();
            var buttonPressedEventPool = world.GetPool<EventButtonPressedComponent>();

            foreach (var cllisionEntity in collisionfilter)
            {
                ref var collision = ref collisionPool.Get(cllisionEntity);
                foreach (var buttonEntity in buttonfilter)
                {
                    ref var buttonTransform = ref transformPool.Get(buttonEntity);
                    bool isChold = collision.collisionObject.transform.IsChildOf(buttonTransform.transform);
                    bool isObject = collision.collisionObject.transform == buttonTransform.transform;

                    if (isChold || isObject)
                    {
                        var buttonPressedEventEntity = world.NewEntity();
                        buttonPressedEventPool.Add(buttonPressedEventEntity).colorType = colorIdPool.Get(buttonEntity).colorType;
                    }
                }
            }
        }
    }
}