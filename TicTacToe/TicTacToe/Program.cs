
using System.Collections.Specialized;
using static System.Runtime.InteropServices.JavaScript.JSType;
class TicTacToe
{
  private char[,] Field = new char[3, 3]; //Поле
  private bool isOver = false; //Окончание игры
  public bool IsOver
  {
    get { return isOver; }
  }
  private int numberOfEmptyCells = 9; //Количество пустых клеток (необходимо для определение ничьей)
  private char player = 'X'; //Текущий игрок
  private int N = 3; //Количество строк/столбцов
  private int rowHighlightWithColor = -1;
  private int columnHighlightWithColor = -1;
  public enum Columns //Перечисление столбцов
  {
    a = 1,
    b,
    c
  }
  public TicTacToe() //Заполнение поле пробелами в конструкторе
  {
    for (int i = 0; i < N; i++)
    {
      for (int j = 0; j < N; j++)
      {
        Field[i, j] = ' ';
      }
    }
  }
  public void DrawHorizontalSeparator()
  {
    for (int j = 0; j < N; j++)
    {
      Console.Write("-----");
    }
    Console.WriteLine();
  }

  public void DrawRow(int row)
  {
    Console.Write(" " + (row + 1) + " |");
    for (int j = 0; j < N; j++)
    {
      if (rowHighlightWithColor == row && columnHighlightWithColor == j) //Подсвечиваем
        Console.ForegroundColor = ConsoleColor.Green;
      Console.Write(" " + Field[row, j]);
      Console.ResetColor();
      Console.Write(" |");
    }
    Console.WriteLine();
  }

  public void FieldDrawing() // Отрисовка поля
  {
    Console.Write("   |"); //Шапка
    for (int j = 0; j < N; j++)
    {
      Console.Write(" " + (Columns)(j + 1) + " |");
    }
    Console.WriteLine();

    DrawHorizontalSeparator();

    for (int i = 0; i < N; i++) //поле
    {
      DrawRow(i);
      DrawHorizontalSeparator();
    }
  }
  public bool IsInputValid(string input, out int row, out Columns column) //Проверка на валидность
  {
    row = 0;
    column = Columns.a;

    string[] combinationArray = input.Split(' ');

    if (combinationArray.Length != 2)
    {
      Console.WriteLine("Неверный формат ввода. Пожалуйста, убедитесь, что введена пара значений.");
      return false;
    }

    bool parseError = !int.TryParse(combinationArray[0], out row) || !Enum.TryParse(combinationArray[1], out column);

    if (parseError)
    {
      Console.WriteLine("Неверный формат ввода. Пожалуйста, убедитесь, что первый элемент - число, а второй - символ.");
      return false;
    }

    if (!(1 <= row && row <= N && Enum.IsDefined(typeof(Columns), column)))
    {
      Console.WriteLine("Неверный формат ввода. Пожалуйста, убедитесь, что значения в пределах игрового поля.");
      return false;
    }

    int rowIndex = row - 1;
    int colIndex = (int)column - 1;

    if (Field[rowIndex, colIndex] != ' ')
    {
      Console.WriteLine("Эта ячейка уже занята. Пожалуйста, выберите другую.");
      return false;
    }

    return true;
  }

  public void SelectPosition() // Выбор позиции пользователем
  {
    bool validInput = false;
    while (!validInput)
    {
      Console.WriteLine("Выберите позицию для " + player + " в формате: 1 a");
      string combination = Console.ReadLine();

      int line;
      Columns column;

      validInput = IsInputValid(combination, out line, out column);
      if (!validInput)
      {
        continue;
      }

      int rowIndex = line - 1;
      int colIndex = (int)column - 1;

      Field[rowIndex, colIndex] = player;
      numberOfEmptyCells--;
      rowHighlightWithColor = rowIndex;
      columnHighlightWithColor = colIndex;
    }
  }
  public bool CheckingWinConditions() //Проверка на выигрыш текущего игрока
  {
    if (Field[0, 0] == player && Field[1, 1] == player && Field[2, 2] == player) return true;
    else if (Field[0, 2] == player && Field[1, 1] == player && Field[2, 0] == player) return true;
    else
    {
      // Проверка по горизонтали
      for (int i = 0; i < N; i++)
        if (Field[i, 0] == player && Field[i, 1] == player && Field[i, 2] == player) return true;

      // Проверка по вертикали
      for (int j = 0; j < N; j++)
        if (Field[0, j] == player && Field[1, j] == player && Field[2, j] == player) return true;
    }
    return false;
  }

  public void GameOverCheck() //Проверка на окончание игры
  {
    if (CheckingWinConditions())
    { 
      Console.WriteLine("Выиграли " + player + "!!"); 
      isOver = true; 
    }
    else
    {
      if (numberOfEmptyCells == 0) 
      { 
        Console.WriteLine("Ничья"); 
        isOver = true; 
      }
      else
      {
        player = (player == 'X') ? 'O' : 'X'; //смена игрока
      }
    }
  }
}


class Program
{
  static void Main()
  {
    TicTacToe game = new TicTacToe();
    game.FieldDrawing();
    while (!game.IsOver)
    {
      game.SelectPosition();
      game.FieldDrawing();
      game.GameOverCheck();
    }

  }
}
