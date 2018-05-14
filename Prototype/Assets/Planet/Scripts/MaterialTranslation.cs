using UnityEngine;

namespace Assets.Planet.Scripts
{
    /// <summary>
    /// Changing main material offset of <see cref="Renderer"/> by <see cref="SpeedManager"/> speed
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class MaterialTranslation : MonoBehaviour
    {
        [SerializeField]
        private RotationSpeedManager SpeedManager;
        
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
