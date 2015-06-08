//namespace Othello
//{
//    using System;
//
//    public class View
//    {
//        public static void DrawBoard(GameBoard i_GameBoard)
//        {
//            int boardSize = i_GameBoard.Size;
//            ePlayer[,] board = i_GameBoard.Board;
//            char column = 'A';
//            
//            Console.Clear();
//            Console.Write(" ");
//            for (int i = 0; i < boardSize; i++)
//            {
//                Console.Write("   " + (column++));
//            }
//
//            Console.WriteLine("  ");
//            for (int j = 0; j < boardSize; j++)
//            {
//                Console.Write("  ");
//                for (int i = 0; i < (boardSize * 4) + 1; i++)
//                {
//                    Console.Write("=");
//                }
//
//                Console.WriteLine();
//                if ((j + 1) <= 9)
//                {
//                    Console.Write(" ");
//                }
//
//                Console.Write(j + 1);
//                for (int i = 0; i < boardSize; i++)
//                {
//                    Console.Write(string.Format("| {0} ", getCellSign(board[j, i])));
//                }
//
//                Console.WriteLine("|");
//            }
//
//            Console.Write("  ");
//            for (int i = 0; i < (boardSize * 4) + 1; i++)
//            {
//                Console.Write("=");
//            }
//
//            Console.WriteLine();
//        }
//
//        private static string getCellSign(ePlayer i_PlayerEnum)
//        {
//            string toReturn = " ";
//            switch (i_PlayerEnum)
//            {
//                case ePlayer.NoPlayer:
//                    toReturn = " ";
//                    break;
//                case ePlayer.Player1:
//                     toReturn =  "X";
//                     break;
//                case ePlayer.Player2:
//                     toReturn =  "O";
//                     break;
//                default:
//                     throw new Exception("Couldn't find a cell sign");
//            }
//
//            return toReturn;
//            
//        }
//
//        internal static void ShowScore(Player i_FirstPlayer, Player i_SecondPlayer, GameBoard i_Board)
//        {
//            string winnerMsg = @"{0} IS THE WINNER!!!
//
//                                   .''.
//       .''.      .        *''*    :_\/_:     .
//      :_\/_:   _\(/_  .:.*_\/_*   : /\ :  .'.:.'.
//  .''.: /\ :    /)\   ':'* /\ *  : '..'.  -=:o:=-
// :_\/_:'.:::.  | ' *''*    * '.\'/.'_\(/_ '.':'.'
// : /\ : :::::  =  *_\/_*     -= o =- /)\    '  *
//  '..'  ':::' === * /\ *     .'/.\'.  ' ._____
//      *        |   *..*         :       |.   |' .----|
//        *      |     _           .--'|  ||   | _|    |
//        *      |  .-'|       __  |   |  |    ||      |
//     .-----.   |  |' |  ||  |  | |   |  |    ||      |
// ___'       ' / \ |  '-.''.    '-'   '-.'    '`      |____
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//         Press Enter to start a new game";
//
//            string tieMsg = @"YOU BOTH WINNERS!!!
//
//                ,a_a
//               {/ ''\_
//               {\ ,_oo)
//               {/  (_^_____________________
//     .=.      {/ \___)))*)----------;=====;`
//    (.=.`\   {/   /=;  ~~           |     |
//        \ `\{/(   \/\               |     |
//         \  `. `\  ) )              |     |
//          \    // /_/_              |     |
//           '==''---))))             |_____|
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//         Press Enter to start a new game";
//            
//            Console.Clear();
//            View.DrawBoard(i_Board);
//            Console.WriteLine(string.Format(
//                @"
//  ▄▀  ██   █▀▄▀█ ▄███▄       ████▄     ▄   ▄███▄   █▄▄▄▄  ▄ 
//▄▀    █ █  █ █ █ █▀   ▀      █   █      █  █▀   ▀  █  ▄▀ █  
//█ ▀▄  █▄▄█ █ ▄ █ ██▄▄        █   █ █     █ ██▄▄    █▀▀▌ █   
//█   █ █  █ █   █ █▄   ▄▀     ▀████  █    █ █▄   ▄▀ █  █ █   
// ███     █    █  ▀███▀               █  █  ▀███▀     █      
//        █    ▀                        █▐            ▀   ▀   
//       ▀                              ▐                     
//
//{0} have {1} points, {2} have {3} points.",
//                i_FirstPlayer.Name,
//                i_Board.GetScore(i_FirstPlayer.PlayerEnum),
//                i_SecondPlayer.Name,
//                i_Board.GetScore(i_SecondPlayer.PlayerEnum)));
//
//            if (i_Board.GetScore(i_FirstPlayer.PlayerEnum) > i_Board.GetScore(i_SecondPlayer.PlayerEnum))
//            {
//                Console.WriteLine(string.Format(winnerMsg, i_FirstPlayer.Name));
//                Console.ReadLine();
//            }
//            else if (i_Board.GetScore(i_SecondPlayer.PlayerEnum) > i_Board.GetScore(i_FirstPlayer.PlayerEnum))
//            {
//                Console.WriteLine(string.Format(winnerMsg, i_SecondPlayer.Name));
//                Console.ReadLine();
//            }
//            else
//            {
//                Console.WriteLine(tieMsg);
//                Console.ReadLine();
//            }
//        }
//    }
//}
