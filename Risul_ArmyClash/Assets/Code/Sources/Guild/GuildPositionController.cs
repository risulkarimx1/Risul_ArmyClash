using Assets.Code.Sources.Managers;
using Assets.Code.Sources.Units;
using UnityEngine;

namespace Assets.Code.Sources.Guild
{
    public class GuildPositionController
    {
        private readonly GameSettings _gameSettings;


        public GuildPositionController(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public Vector3 GetRandomPosition(UnitSide unitSide)
        {
            switch (unitSide)
            {
                case UnitSide.SideA:
                {
                    var positionModel = _gameSettings.GuildPositionA;
                    return RandomPosition(positionModel);
                }
                case UnitSide.SideB:
                {
                    var positionModel = _gameSettings.GuildPositionB;
                    return RandomPosition(positionModel);
                }
            }

            return Vector3.one;
        }

        public Vector3 GetRotation(UnitSide unitSide)
        {
            switch (unitSide)
            {
                case UnitSide.SideA:
                    return _gameSettings.GuildPositionA.Rotation;
                case UnitSide.SideB:
                    return _gameSettings.GuildPositionB.Rotation;
            }
            
            return Vector3.zero;
        }
        
        private Vector3 RandomPosition(GuildPositioningModel positionModel)
        {
            var x = positionModel.Center.x + Random.Range(-positionModel.Width, positionModel.Width);
            var z = positionModel.Center.z + Random.Range(-positionModel.Length, positionModel.Length);

            return new Vector3(x, positionModel.Center.y, z);
        }
    }
}
