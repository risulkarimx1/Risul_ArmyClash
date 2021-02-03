using UnityEngine;

namespace Assets.Code.Sources.Units
{
    public class UnitView : MonoBehaviour, IUnitView
    {
        private readonly UnitModel _unitModel;
        private Renderer _renderer;
        private static readonly int Color = Shader.PropertyToID("_Color");


        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }
        
        public void Configure(UnitModel unitModel)
        {
            // Untilize model to make look and free
            _renderer.material.SetColor(Color, unitModel.ColorModel.Color);
        }
    }
}
