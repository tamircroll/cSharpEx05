namespace Othello
{
    using System.Collections.Generic;

    public class Controller
    {
        public static void ExecutePlayMove(Othello i_Othello, int i_Row, int i_Column, Player i_Player, GameBoard i_Board)
        {
            i_Board.PaintGray();

            for (int rowMoveDirection = -1; rowMoveDirection <= 1; rowMoveDirection++)
            {
                for (int columnMoveDirection = -1; columnMoveDirection <= 1; columnMoveDirection++)
                {
                    if (columnMoveDirection != 0 || rowMoveDirection != 0)
                    {
                        if (canEat(i_Row, i_Column, rowMoveDirection, columnMoveDirection, i_Player, i_Board))
                        {
                            eatPiecesInDirection(i_Row, i_Column, rowMoveDirection, columnMoveDirection, i_Player, i_Board);
                        }
                    }
                }
            }

            i_Board[i_Row, i_Column] = i_Player.PlayerEnum;
            i_Othello.DoAfterTurn();

        }


        
        public static List<int[]> ListAllPossibleMoves(Player i_Player, GameBoard i_Board)
        {
            List<int[]> validateMoves = new List<int[]>();

            for (int row = 0; row < i_Board.Size; row++)
            {
                for (int column = 0; column < i_Board.Size; column++)
                {
                    bool validMove = IsValidMove(row, column, i_Player, i_Board);

                    if(validMove)
                    {
                        validateMoves.Add(new[] { row, column });
                    }
                }
            }

            return validateMoves;
        }

        private static void eatPiecesInDirection(int i_Row, int i_Column, int i_moveRow, int i_MoveColumn, Player i_Player, GameBoard i_Board)
        {
            do
            {
                i_Row += i_moveRow;
                i_Column += i_MoveColumn;
                i_Board[i_Row, i_Column] = i_Player.PlayerEnum;
            }
            while (i_Board[i_Row + i_moveRow, i_Column + i_MoveColumn] != i_Player.PlayerEnum);
        }

        private static bool canEat(int i_Row, int i_Column, int i_RowDirection, int i_ColumnDirection, Player i_Player, GameBoard i_Board)
        {
            int numOfPiecesToEat = 0;
            bool canEat = false;

            if (i_Board[i_Row, i_Column] == ePlayer.NoPlayer)
            {
                do
                {
                    i_Row += i_RowDirection;
                    i_Column += i_ColumnDirection;

                    if (i_Row < 0 || i_Column < 0 || i_Row >= i_Board.Size || i_Column >= i_Board.Size)
                    {
                        break;
                    }

                    if (i_Board[i_Row, i_Column] == i_Player.PlayerEnum)
                    {
                        canEat = numOfPiecesToEat > 0;
                        break;
                    }

                    numOfPiecesToEat++;
                }
                while (i_Board[i_Row, i_Column] != ePlayer.NoPlayer);
            }

            return canEat;
        }

        private static bool IsValidMove(int i_Row, int i_Column, Player i_Player, GameBoard i_Board)
        {
            bool validMove = false;

            for (int rowDirection = -1; rowDirection <= 1; rowDirection++)
            {
                for (int columnDirection = -1; columnDirection <= 1; columnDirection++)
                {
                    if (columnDirection != 0 || rowDirection != 0)
                    {
                        if (canEat(i_Row, i_Column, rowDirection, columnDirection, i_Player, i_Board))
                        {
                            validMove = true;
                            break;
                        }
                    }
                }

                if (validMove)
                {
                    break;
                }
            }

            return validMove;
        }
    }
}
