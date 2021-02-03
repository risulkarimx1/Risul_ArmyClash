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


        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _meshFilter = GetComponent<MeshFilter>();
            _transform = GetComponent<Transform>();
        }
        
        public void Configure(UnitModel unitModel)
        {
            _meshFilter.sharedMesh = unitModel.ShapeModel.MeshFilter.sharedMesh;
            _renderer.material.SetColor(Color, unitModel.ColorModel.Color);
            _transform.localScale = Vector3.one * unitModel.SizeModel.SizeFactor;
        }

        public void SetPosition(Vector3 position) => _transform.position = position;
    }
}
