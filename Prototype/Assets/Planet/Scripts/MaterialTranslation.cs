using UnityEngine;

namespace Assets.Planet.Scripts
{
    public class MaterialTranslation : MonoBehaviour
    {
        public float Speed = 2.5f;

        private Renderer _renderer;

        // Use this for initialization
        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            _renderer.material.mainTextureOffset += new Vector2(0, Speed * Time.deltaTime);
        }
    }
}
