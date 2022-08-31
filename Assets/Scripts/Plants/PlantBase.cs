using System;
using System.Collections;
using UnityEngine;

namespace Plants
{
    public class PlantBase : MonoBehaviour, IPlant
    {
        [SerializeField] private PlantData data;
        [SerializeField] private GameObject plantObject;
        private YieldInstruction _wait;
        private Coroutine _growCor;
        
        public event Action OnGrow;
        public event Action OnPick;

        protected virtual void OnEnable()
        {
            var time = UnityEngine.Random.Range(data.growTimeRange.x, data.growTimeRange.y);
            _wait = new WaitForSeconds(time);
            plantObject.SetActive(false);
        }

        public virtual void Grow(Vector3 target)
        {
            if(_growCor != null) StopCoroutine(_growCor);
            plantObject.transform.position = target;
            _growCor = StartCoroutine(GrowCor());
        }
        
        public virtual void Pick()
        {
            plantObject.SetActive(false);
        }

        protected virtual IEnumerator GrowCor()
        {
            yield return _wait;
            plantObject.SetActive(true);
        }
    }
}