using UnityEngine;

namespace Assets.Planet.Scripts
{
    public class MaterialTranslation : MonoBehaviour
    {
        public RotationSpeedManager SpeedManager;

        private Renderer _renderer;

        // Use this for initialization
        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            _renderer.material.mainTextureOffset += new Vector2 (SpeedManager.Speed * Time.deltaTime / 360f, 0f);
        }
    }
}
