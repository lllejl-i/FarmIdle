using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantingBehaviour.Plants
{
    public class BerryPlant : Plant
    {

        public BerryPlant(PlantData data, int currentStage = 0, Dictionary<string, object> parameters = null)
                             : base(data, currentStage, parameters)
        {
            int maxStage = data.Sprites.Count;
            AfterHarvestIndex = 1;
        }

    }
}
