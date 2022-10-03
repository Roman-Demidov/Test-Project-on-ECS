using codeBase.components;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class DoorStartOpeningSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var eventfilter = world.Filter<EventButtonPressedComponent>().End();
            var doorfilter = world.Filter<DoorComponent>().Inc<ColorIdComponent>().End();

            var eventPool = world.GetPool<EventButtonPressedComponent>();
            var colorIdPool = world.GetPool<ColorIdComponent>();
            var doorPool = world.GetPool<DoorComponent>();
            var movementPool = world.GetPool<MovementComponent>();

            foreach (var eventEntity in eventfilter)
            {
                foreach (var doorEntity in doorfilter)
                {
                    if(colorIdPool.Get(doorEntity).colorType == eventPool.Get(eventEntity).colorType)
                    {
                        movementPool.Add(doorEntity);
                        ref var movementComponent = ref movementPool.Get(doorEntity);

                        movementComponent.newPosition = doorPool.Get(doorEntity).openPosition;
                        movementComponent.moveSpeed = doorPool.Get(doorEntity).moveSpeed;
                    }
                }
            }
        }
    }
}