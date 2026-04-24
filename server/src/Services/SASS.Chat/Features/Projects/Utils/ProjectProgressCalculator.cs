namespace SASS.Chat.Features.Projects.Utils;

internal static class ProjectProgressCalculator
{
    public static int Calculate(int completedTaskCount, int totalTaskCount)
    {
        if (totalTaskCount <= 0)
        {
            return 0;
        }

        var percentage = (double)completedTaskCount / totalTaskCount * 100;
        return (int)Math.Round(percentage, MidpointRounding.AwayFromZero);
    }
}
