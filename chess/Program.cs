using System;
using System.Collections.Generic;

namespace chess
{
    internal class Program
    {
        class Figure
        {
            public Figure(int[] _coords)
            {
                coords = _coords;
            }
            public string model { get; set; }
            public Boolean isBlack { get; set; }
            public int[] coords { get; set; }
        }

        static void Main(string[] args)
        {
            int[] coords_1 = new int[2];
            while (true)
            {
                Console.Write("Координаты первой фигуры(k, l): ");
                string[] a = Console.ReadLine().Split(',');
                coords_1 = Array.ConvertAll(a, int.Parse);
                if ((coords_1[0] >= 1 && coords_1[0] <= 8) && (coords_1[1] >= 1 && coords_1[1] <= 8))
                {
                    Console.WriteLine();
                    break;
                }
                    
                else
                    Console.WriteLine("Координаты находятся в пределах от 1 до 8!");
            } 


            List<string> available_figures = new List<string>() { "слон", "ладья", "ферзь", "конь" };
            Figure figure_1 = new Figure(coords_1);

            while (true)
            {
                Console.Write("Какая фигура на поле k, l: ");
                string figure_model = Console.ReadLine();
                figure_1.model = figure_model.ToLower();
                if (available_figures.Contains(figure_1.model))
                {
                    Console.WriteLine();
                    break;
                }
                else
                    Console.WriteLine("Доступные фигуры: \"Слон\", \"Ладья\", \"Ферзь\", \"Конь\" ");

            }
            

            int[] coords_2 = new int[2];

            while (true)
            {
                Console.Write("Координаты второй фигуры(m, n): ");
                string[] b = Console.ReadLine().Split(',');
                coords_2 = Array.ConvertAll(b, int.Parse);
                if ((coords_1[0] >= 1 || coords_1[0] <= 8) || (coords_1[1] >= 1 || coords_1[1] <= 8))
                {
                    Console.WriteLine();
                    break;
                }
                else
                    Console.WriteLine("Координаты находятся в пределах от 1 до 8!");
            }

            Figure figure_2 = new Figure(coords_2);
            
            if ((figure_1.coords[0] % 2 != 0 && figure_2.coords[1] % 2 == 0) || (figure_1.coords[0] % 2 == 0 && figure_1.coords[1] % 2 != 0))
                figure_1.isBlack = true;
            else
                figure_1.isBlack = false;

            if ((figure_2.coords[0] % 2 != 0 && figure_2.coords[1] % 2 == 0) || (figure_2.coords[0] % 2 == 0 && figure_2.coords[1] % 2 != 0))
                figure_2.isBlack = true;
            else
                figure_2.isBlack = false;

            if (figure_1.isBlack == figure_2.isBlack)
                Console.WriteLine("Поля одинакового цвета!");
            else Console.WriteLine("Поля разного цвета! ");

            int[,] bishop_turns = new int[,] { {1, 1}, {-1, 1}, {1, -1}, {-1, -1} };
            int[,] rook_turns = new int[,] { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
            int[,] queen_turns = new int[,] { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 }, { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 } };
            int[,] knight_turns = new int[,] { { 2, 1 }, { 1, 2 }, { -1, 2 }, { -2, 1 }, {-2, -1}, {-1, -2}, {1, -2}, {2, -1} };

            Boolean danger = false;
            Boolean for_1_turn = false;

            int[] turn = new int[2] { figure_1.coords[0], figure_1.coords[1] };
            int[] first_turn = new int[2];

            switch(figure_1.model)
            {
                case "слон":
                    for (int i = 0; i < bishop_turns.GetLength(0); i++)
                    {
                        while ((turn[0] >= 1 && turn[0] <= 8) && (turn[1] >= 1 && turn[1] <= 8))
                        {
                            turn[0] += bishop_turns[i, 0];
                            turn[1] += bishop_turns[i, 1];
                            if (turn[0] == figure_2.coords[0] && turn[1] == figure_2.coords[1])
                            {
                                for_1_turn = true;
                                danger = true;
                                break;
                            }
                            if (Math.Max(figure_2.coords[0], turn[0]) - Math.Min(figure_2.coords[0], turn[0]) == Math.Max(figure_2.coords[1], turn[1]) - Math.Min(figure_2.coords[1], turn[1]))
                            {
                                if ((turn[0] >=1 && turn[0] <= 8) && (turn[1] >= 1 && turn[1] <= 8))
                                {
                                    first_turn[0] = turn[0]; first_turn[1] = turn[1];
                                }
                            }

                        }
                        turn[0] = figure_1.coords[0];
                        turn[1] = figure_1.coords[1];

                    }
                    break;

                case "ладья":
                    for (int i = 0; i < rook_turns.GetLength(0); i++)
                    {
                        while ((turn[0] >= 1 && turn[0] <= 8) && (turn[1] >= 1 && turn[1] <= 8))
                        {
                            turn[0] += rook_turns[i, 0];
                            turn[1] += rook_turns[i, 1];
                            if (turn[0] == figure_2.coords[0] && turn[1] == figure_2.coords[1])
                            {
                                for_1_turn = true;
                                danger = true;
                                break;
                            }
                            if ((turn[0] == figure_2.coords[0] && turn[1] != figure_2.coords[1]) || (turn[0] != figure_2.coords[0] && turn[1] == figure_2.coords[1]))
                            {
                                if ((turn[0] >= 1 && turn[0] <= 8) && (turn[1] >= 1 && turn[1] <= 8))
                                {
                                    first_turn[0] = turn[0]; first_turn[1] = turn[1];
                                }
                            }

                        }
                        turn[0] = figure_1.coords[0];
                        turn[1] = figure_1.coords[1];
                    }
                    break;

                case "ферзь":
                    for (int i = 0; i < queen_turns.GetLength(0); i++)
                    {
                        while ((turn[0] >= 1 && turn[0] <= 8) && (turn[1] >= 1 && turn[1] <= 8))
                        {
                            turn[0] += queen_turns[i, 0];
                            turn[1] += queen_turns[i, 1];
                            if (turn[0] == figure_2.coords[0] && turn[1] == figure_2.coords[1])
                            {
                                for_1_turn = true;
                                danger = true;
                                break;
                            }
                            if (((turn[0] == figure_2.coords[0] && turn[1] != figure_2.coords[1]) || (turn[0] != figure_2.coords[0] && turn[1] == figure_2.coords[1]))
                            || Math.Max(figure_2.coords[0], turn[0]) - Math.Min(figure_2.coords[0], turn[0]) == Math.Max(figure_2.coords[1], turn[1]) - Math.Min(figure_2.coords[1], turn[1]))
                            {
                                if ((turn[0] >= 1 && turn[0] <= 8) && (turn[1] >= 1 && turn[1] <= 8))
                                {
                                    first_turn[0] = turn[0]; first_turn[1] = turn[1];
                                }
                            }

                        }
                        turn[0] = figure_1.coords[0];
                        turn[1] = figure_1.coords[1];
                    }
                    break;

                case "конь":
                    for (int i = 0; i < knight_turns.GetLength(0); i++)
                    {
                        if (turn[0] + knight_turns[i, 0] < 1 || turn[1] + knight_turns[i, 1] < 1 || turn[0] + knight_turns[i, 0] > 8 || turn[1] + knight_turns[i, 1] > 8)
                            continue;
                        turn[0] += knight_turns[i, 0]; 
                        turn[1] += knight_turns[i, 1];
                        if (turn[0] == figure_2.coords[0] && turn[1] == figure_2.coords[1])
                        {
                            for_1_turn = true;
                            danger = true;
                            break;
                        }
                        for (int j = 0; j < knight_turns.GetLength(0); j ++)
                        {
                            if (turn[0] + knight_turns[i, 0] < 1 || turn[1] + knight_turns[i, 1] < 1 || turn[0] + knight_turns[i, 0] > 8 || turn[1] + knight_turns[i, 1] > 8)
                                continue;
                            else if (turn[0] + knight_turns[j, 0] == figure_2.coords[0] && turn[1] + knight_turns[j, 1] == figure_2.coords[1])
                            {
                                first_turn[0] = turn[0]; first_turn[1] = turn[1];
                            }
                                
                        }
                        turn[0] = figure_1.coords[0];
                        turn[1] = figure_1.coords[1];

                    }
                    break;
            }
            Console.WriteLine(danger ? "Фигура 1 угрожает фигруре 2!" : "Фигура 1 не угрожает фигуре 2!");
            if (first_turn[0] == 0 || first_turn[1] == 0)
                Console.WriteLine("Невозможно напасть за 2 хода!");
            else
                Console.WriteLine(for_1_turn ? "Можно напасть за 1 ход!" : $"Первый ход: {first_turn[0]},{first_turn[1]}");

        }
    }
}
