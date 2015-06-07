namespace Othello
{
    public class Program
    {
        public static void Main()
        {
            bool keepPlayng;

            do
            {
                Othello othello = new Othello();
                keepPlayng = othello.StartNewGame();
            }
            while (keepPlayng);
        }
    }
}
