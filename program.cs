using System;
using System.Collections.Generic;
using System.Threading;

class Labirinto
{
    private const int limit = 15;

    static void mostrarLabirinto(char[,] array)
    {
        for (int i = 0; i < limit; i++)
        {
            Console.WriteLine();
            for (int j = 0; j < limit; j++)
            {
                Console.Write($" {array[i, j]} ");
            }
        }
        Console.WriteLine();
    }

    static void criaLabirinto(char[,] meuLab)
    {
        Random random = new Random();
        for (int i = 0; i < limit; i++)
        {
            for (int j = 0; j < limit; j++)
            {
                meuLab[i, j] = random.Next(4) == 1 ? '|' : '.';
            }
        }

        for (int i = 0; i < limit; i++)
        {
            meuLab[0, i] = '*';
            meuLab[limit - 1, i] = '*';
            meuLab[i, 0] = '*';
            meuLab[i, limit - 1] = '*';
        }

        int x = random.Next(limit);
        int y = random.Next(limit);
        meuLab[x, y] = 'Q';
    }

    static void resolveLabirinto(char[,] labirinto, int i, int j)
    {

        Stack<int> pilha_i = new Stack<int>();
        Stack<int> pilha_j = new Stack<int>();

        bool encontrou = false;
        while (encontrou == false)
        {
                
            labirinto[i, j] = 'v';
            if (labirinto[i, j+1] == '.' || labirinto[i, j+1] == 'Q')
            {
                pilha_i.Push(i);
                pilha_j.Push(j);

                j++;
            }
            else if(labirinto[i, j-1] == '.' || labirinto[i, j-1] == 'Q')
            {
                pilha_i.Push(i);
                pilha_j.Push(j);
                if (labirinto[i, j-1] == 'Q')
                {
                    encontrou = true;
                }
                j--;
            }
            else if (labirinto[i+1, j] == '.' || labirinto[i+1, j] == 'Q')
            {
                pilha_i.Push(i);
                pilha_j.Push(j);
                if (labirinto[i+1, j] == 'Q')
                {
                    encontrou = true;
                }
                i++;
            }
            else if (labirinto[i-1, j] == '.' || labirinto[i-1, j] == 'Q')
            {
                pilha_i.Push(i);
                pilha_j.Push(j);
                if (labirinto[i-1, j] == 'Q')
                {
                    encontrou = true;
                }
                i--;
            }

            else if(pilha_i.Count > 0 && pilha_j.Count > 0){
                labirinto[i, j] = 'x';
                i = pilha_i.Pop();
                j = pilha_j.Pop();
            }
            else
            {
                Console.WriteLine("Não houve caminho para encontrar o queijo. Game Over");
                return;
            }

            //tentar para baixo = i+1
            //tentar para traz = j-1
            //tentar para cima = i-1
            //volta (if count > 0) i = pilha_i.pop; j = pilha_j.pop
            //senao count == 0 nao tem jeito

            Thread.Sleep(300);
            Console.Clear();
            mostrarLabirinto(labirinto);

        }
        
        Console.WriteLine("Parabéns, o queijo foi encontrado");

        
        

    }

    static void Main(string[] args)
    {
        char[,] maze = new char[limit, limit];
        int x, y;
        criaLabirinto(maze);
        mostrarLabirinto(maze);
        Console.WriteLine("\nPosições iniciais (linha e coluna):");
        x = Convert.ToInt32(Console.ReadLine());
        y = Convert.ToInt32(Console.ReadLine());
        resolveLabirinto(maze, x, y);
        Console.ReadKey();
    }
}
