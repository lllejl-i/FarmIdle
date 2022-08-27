using PlantingBehaviour.Plants;
using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlantSpriteIndexChanging(int index);

namespace PlantingBehaviour
{
    public abstract class Plant
    {
        public event PlantSpriteIndexChanging OnStageChanging;
        protected PlantData data;
        protected int growStage = 0;

        #region Properties
        public static Plant CreatePlant(PlantData data, int currentStage = 0, Dictionary<string, object> parameters = null)
        {
            switch (data.Type)
            {
                case PlantType.Greenery:
                    return new GreeneryPlant(data, currentStage, parameters);
                case PlantType.Berry:
                    return new BerryPlant(data, currentStage, parameters);
                case PlantType.Mushroom:
                    return new MushroomPlant(data, currentStage, parameters);
            }

            return null;
        }

        public int GrowStage
        {
            get { return growStage; }
            set
            {
                growStage = value;
                OnStageChanging?.Invoke(growStage);
            }
        }

        public Sprite GetSprite(int index)
        {
            return data.Sprites[index];
        }

        public (FruitData data, int count) HarvestData => (data.Fruit, data.FruitsPerSquareMeter);

        public PlantData Data => data;
        #endregion
        public Plant(PlantData data, int currentStage = 0, Dictionary<string, object> parameters = null)
        {
            this.data = data;
            GrowStage = currentStage;
        }

        public int AfterHarvestIndex { get; protected set; }

        public virtual (FruitData data, int count) GetHarvest()
        {
            GrowStage = AfterHarvestIndex;
            return (data.Fruit, data.FruitsPerSquareMeter);
        }
    }
}
