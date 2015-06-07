namespace Othello
{
    using System.Collections.Generic;

    public class Controller
    {
        public static bool TryPlayMove(Player i_Player, string i_ChosenCell, GameBoard i_Board)
        {
            string emptyMsg = string.Empty;
            return TryPlayMove(i_Player, i_ChosenCell, ref emptyMsg, i_Board);
        }

        public static bool TryPlayMove(Player i_Player, string i_ChosenCell, ref string o_Msg, GameBoard i_Board)
        {
            int row;
            bool validMove = false;

            if (!string.IsNullOrEmpty(i_ChosenCell))
            {
                char columnChar = i_ChosenCell.ToLower().ToCharArray()[0];
                string rowStr = i_ChosenCell.Substring(1);
                int column = columnChar - 'a';
                validMove = inputIsCell(rowStr, columnChar, out row, i_Board);

                if (!validMove)
                {
                    o_Msg = string.Format("{0}, This input does not represent a cell in the board!!!{1}Please play again.", i_Player.Name, System.Environment.NewLine);
                }
                else
                {
                    validMove = isMoveInValidMovesList(row, column, i_Player, i_Board);
                    if (!validMove)
                    {
                        o_Msg = string.Format("{0}, You can not choose cell {1}, Please play again.", i_Player.Name, i_ChosenCell);
                    }
                    else
                    {
                        ExecutePlayMove(row, column, i_Player, i_Board);
                    }
                }
            }

            return validMove;
        }

        public static void ExecutePlayMove(int i_Row, int i_Column, Player i_Player, GameBoard i_Board)
        {
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
        }

        public static List<string> ListAllPossibleMoves(Player i_Player, GameBoard i_Board)
        {
            List<string> validateMoves = new List<string>();

            for (int row = 0; row < i_Board.Size; row++)
            {
                for (int column = 0; column < i_Board.Size; column++)
                {
                    bool validMove = IsValidMove(row, column, i_Player, i_Board);

                    if(validMove)
                    {
                        validateMoves.Add(string.Format("{0},{1}", row, column));
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

        private static bool inputIsCell(string i_RowStr, char i_Column, out int o_Row, GameBoard i_Board)
        {
            bool canParse = int.TryParse(i_RowStr, out o_Row);

            if (canParse)
            {
                if (o_Row < 1 || o_Row > i_Board.Size)
                {
                    canParse = false;
                }
                else if (i_Column - 'a' < 0 || i_Column - 'a' >= i_Board.Size)
                {
                    canParse = false;
                }

                o_Row--;
            }

            return canParse;
        }

        private static bool isMoveInValidMovesList(int i_Row, int i_Column, Player i_Player, GameBoard i_Board)
        {
            return i_Player.GetValidateMoves(i_Board).Contains(string.Format("{0},{1}", i_Row, i_Column));
        }
    }
}
