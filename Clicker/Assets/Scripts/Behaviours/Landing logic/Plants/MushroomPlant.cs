using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantingBehaviour.Plants
{
    public class MushroomPlant : Plant
    {
        private float currentHarvestMultiply = 1;
        private float maxHarvestMultiply = 2;
        private float step = 0.1f;
        public MushroomPlant(PlantData data, int currentStage = 0, Dictionary<string, object> parameters = null) : base(data, currentStage, parameters)
        {
            int maxStage = data.Sprites.Count;
            AfterHarvestIndex = 0;
        }

        public override (FruitData data, int count) GetHarvest()
        {
            var harvest = base.GetHarvest();
            harvest.count = (int)(harvest.count * currentHarvestMultiply);
            if (currentHarvestMultiply < maxHarvestMultiply)
                currentHarvestMultiply += step;
            return harvest;
        }
    }
}
