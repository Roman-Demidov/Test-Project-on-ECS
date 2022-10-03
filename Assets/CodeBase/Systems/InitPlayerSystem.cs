using codeBase.components;
using codeBase.scriptableObjects;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class InitPlayerSystem : IEcsInitSystem
    {
        private GameObject _player;
        private PlayerSettings _playerSettings;

        public InitPlayerSystem(GameObject player, PlayerSettings playerSettings)
        {
            _player = player;
            _playerSettings = playerSettings;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var playerEntity = world.NewEntity();
            var playerPool = world.GetPool<PlayerComponent>();
            var animatorPool = world.GetPool<AnimatorComponent>();
            var trnasformPool = world.GetPool<TransformComponent>();
            var positionPool = world.GetPool<PositionComponent>();
            var rotationPool = world.GetPool<RotationComponent>();
            var radiusPool = world.GetPool<RadiusComponent>();

            playerPool.Add(playerEntity).moveSpeed = _playerSettings.moveSpeed;
            animatorPool.Add(playerEntity).animator = _player.GetComponentInChildren<Animator>();
            trnasformPool.Add(playerEntity).transform = _player.transform;
            positionPool.Add(playerEntity).position = _player.transform.position;
            rotationPool.Add(playerEntity).rotation = _player.transform.rotation;
            radiusPool.Add(playerEntity).radius = _playerSettings.radius;
        }
    }
}