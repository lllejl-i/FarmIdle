public class GrowStyle
{
    private int currentStage, maxStage, harvestStage;

    public GrowStyle(int endStage, int harvestStage, int startStage = 0)
    {
        startStage = currentStage;
        maxStage = endStage;
        this.harvestStage = harvestStage;
    }

    public void ChangeStage(int stage) 
    {
        currentStage = stage;
    }
    public int Grow()
    {
        if (currentStage < maxStage)
            currentStage++;
        return currentStage;
    }

    public int Harvest()
    {
        currentStage = harvestStage;
        return harvestStage;
    }
}
