using UnityEngine;

namespace Assets.Planet.Scripts
{
    public class MaterialTranslation : MonoBehaviour
    {
        public float Speed = 2.8f;

        private Renderer _renderer;

        // Use this for initialization
        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (Mathf.Abs(Time.fixedTime % 15f) < 0.00001f) Speed += .3f;

            _renderer.material.mainTextureOffset += new Vector2(0, Speed * Time.deltaTime);
        }
    }
}
