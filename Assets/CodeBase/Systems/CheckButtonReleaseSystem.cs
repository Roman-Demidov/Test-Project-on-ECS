using codeBase.components;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class CheckButtonReleaseSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var collisionfilter = world.Filter<EventOnCollisionExitComponent>().End();
            var buttonfilter = world.Filter<ButtonComponent>().Inc<TransformComponent>().Inc<ColorIdComponent>().End();

            var collisionPool = world.GetPool<EventOnCollisionExitComponent>();
            var transformPool = world.GetPool<TransformComponent>();
            var colorIdPool = world.GetPool<ColorIdComponent>();
            var buttonReleaseEventPool = world.GetPool<EventButtonReleaseComponent>();

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
                        var buttonReleaseEventEntity = world.NewEntity();
                        buttonReleaseEventPool.Add(buttonReleaseEventEntity).colorType = colorIdPool.Get(buttonEntity).colorType;
                    }
                }
            }
        }
    }
}