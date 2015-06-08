//using System;
//
//namespace Othello
//{
//    internal class ConsoleHandler
//    {
//
//        internal static int GetBoardSize()
//        {
//            const string k_BoardSizeSix = "1";
//            const string k_BoardSizeEight = "2";
//            int boardSizeInt;
//
//            Console.Clear();
//            while (true)
//            {
//                Console.WriteLine(
//@"Please choose board size:
//{0}) Board size 6
//{1}) Board size 8",
//                  k_BoardSizeSix, 
//                  k_BoardSizeEight);
//                string option = Console.ReadLine();
//                if (option == k_BoardSizeSix)
//                {
//                    boardSizeInt = 6;
//                    break;
//                }
//                
//                if (option == k_BoardSizeEight)
//                {
//                    boardSizeInt = 8;
//                    break;
//                }
//
//                Console.Clear();
//                Console.WriteLine("Inavlid input!");
//            }
//
//            return boardSizeInt;
//        }
//
//        public static eNumOfPlayers ChooseGameType()
//        {
//            eNumOfPlayers numOfPlayers;
//
//            Console.Clear();
//            while (true)
//            {
//                Console.WriteLine(string.Format(
//@"To Start a new Othello game please choose a game type and press Enter:
//{0}) one player game 
//{1}) two players game.",
//                       (int)eNumOfPlayers.OnePlayer,
//                       (int)eNumOfPlayers.TwoPlayers));
//                string gameTypeStr = Console.ReadLine();
//
//                if (gameTypeStr == ((int)eNumOfPlayers.OnePlayer).ToString())
//                {
//                    numOfPlayers = eNumOfPlayers.OnePlayer;
//                    break;
//                }
//                
//                if (gameTypeStr == ((int)eNumOfPlayers.TwoPlayers).ToString())
//                {
//                    numOfPlayers = eNumOfPlayers.TwoPlayers;
//                    break;
//                }
//
//                Console.Clear();
//                Console.WriteLine("Invalid input! Please try again.");
//            }
//
//            return numOfPlayers;
//        }
//
//        public static void noMovesMessage(Player i_Player, GameBoard i_Board)
//        {
//            Console.Clear();
//            View.DrawBoard(i_Board);
//            Console.WriteLine(string.Format("{0}, You don't have any possible move, Press Enter to pass the turn.", i_Player.Name));
//            Console.ReadLine();
//        }
//    }
//}