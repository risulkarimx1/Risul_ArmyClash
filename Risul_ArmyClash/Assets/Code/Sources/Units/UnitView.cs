using UnityEngine;

namespace Assets.Code.Sources.Units
{
    public class UnitView : MonoBehaviour
    {
        private readonly UnitModel _unitModel;
        private Renderer _renderer;
        private MeshFilter _meshFilter;
        private Transform _transform;
        private static readonly int Color = Shader.PropertyToID("_Color");
        private Rigidbody _rigidbody;
        
        public Transform Transform => _transform;
        public Rigidbody Rigidbody => _rigidbody;
        public void SetActive(bool state) => gameObject.SetActive(state);

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _meshFilter = GetComponent<MeshFilter>();
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Configure(UnitModel unitModel)
        {
            _meshFilter.sharedMesh = unitModel.ShapeModel.MeshFilter.sharedMesh;
            _renderer.material.SetColor(Color, unitModel.ColorModel.Color);
            _transform.localScale = Vector3.one * unitModel.SizeModel.SizeFactor;
        }

        public Vector3 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }

        public int GetID()
        {
            return gameObject.GetInstanceID();
        }
    }
}
