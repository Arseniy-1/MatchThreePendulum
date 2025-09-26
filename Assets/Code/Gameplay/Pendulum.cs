using UnityEngine;

namespace Code.Gameplay
{
    public class Pendulum : MonoBehaviour
    {
        [SerializeField] private float _angle = 30f;
        [SerializeField] private float _frequency = 1f;
    
        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime * _frequency * Mathf.PI * 2f;
        
            float currentAngle = Mathf.Sin(_timer) * _angle;
        
            transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }
    }
}
