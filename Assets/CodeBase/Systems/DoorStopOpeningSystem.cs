using codeBase.components;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class DoorStopOpeningSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var eventfilter = world.Filter<EventButtonReleaseComponent>().End();
            var doorfilter = world.Filter<DoorComponent>().Inc<ColorIdComponent>().Inc<MovementComponent>().End();

            var eventPool = world.GetPool<EventButtonReleaseComponent>();
            var colorIdPool = world.GetPool<ColorIdComponent>();
            var movementPool = world.GetPool<MovementComponent>();


            foreach (var eventEntity in eventfilter)
            {
                foreach (var doorEntity in doorfilter)
                {
                    if (colorIdPool.Get(doorEntity).colorType == eventPool.Get(eventEntity).colorType)
                    {
                        movementPool.Del(doorEntity);
                    }
                }
            }
        }
    }
}