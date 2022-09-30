using codeBase.components;
using codeBase.configs;
using codeBase.scriptableObjects;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class RaycasrSystam : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var gameData = systems.GetShared<GameDate>();
            var filter = world.Filter<EventMouseInputComponent>().End();
            var raycastPool = world.GetPool<RaycastEventComponent>();

            foreach (var entity in filter)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if(Physics.Raycast(ray, out var hit, Constants.MAX_RAY_DISTANCE, gameData.walkingLayer))
                {
                    var raycastEntity = world.NewEntity();
                    raycastPool.Add(raycastEntity).raycastHitPosition = hit.point;
                }
            }
        }
    }
}