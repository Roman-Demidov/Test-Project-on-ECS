using UnityEngine;

namespace codeBase.factories
{
    public class ObjectFactory
    {
        private GameObject _player = null;

        public T[] getObjectsFromScene<T>() where T : Object
        {
            return GameObject.FindObjectsOfType<T>();
        }

        public GameObject getPlayer(GameObject playerPrefab, Vector3 initPos)
        {
            if (_player == null)
                return _player = Object.Instantiate(playerPrefab, initPos, Quaternion.identity);
            else
                return _player;
        }
    }
}