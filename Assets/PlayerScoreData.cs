public class PlayerScoreData
{
    private int score;

    public int GetScore()
    {
        return score;
    }

    public void IncrementScore()
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
