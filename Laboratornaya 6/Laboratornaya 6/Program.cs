using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Лабораторная_работа_6
  //Во входном файле записан русский текст, содержащий не более 1000 слов
 //Вывести в алфавитном порядке слова текста, все буквы которых различны
//Оставльные слова инвертировать и вывести в соответствии с убыванием длины.
{
    class Program
    {
        static void Main(string[] args)
        {
            // Нахождение пути к файлу
            FileStream file = new FileStream("D://Учеба//Программирование//Первый курс//C#//Laboratornaya 6//TextLaba6.txt", FileMode.Open);
            // Перевод для чтения на русский язык
            StreamReader reader = new StreamReader(file, Encoding.GetEncoding(1251));
            string textlab = reader.ReadToEnd();
            // Исключаем знаки знаки препинания, заменяя на пустоту
            textlab = Regex.Replace(textlab, "[-.?!<>)(,:]", "");

            //Закрыли поток и преобразуем массив в лист
            reader.Close();

            //Делаем разрыв между символами для преобразования в слова
            //Также создаем три листа(первый лист исходный текст, второй лист это текст для слов с одинаковыми буквами, третьий лист это текст для оставших слов)
            List<string> words = textlab.Split(' ').ToList<string>();
            List<string> words2 = new List<string>();
            List<string> words3 = new List<string>();

            // Проверка слов на наличе двух одинаковых букв
            //Первый цикл создан из элементов(слов), второй цикл создан из элементов(букв) для сравнения букв в третьем цикле, третьий цикл создан из элементов(букв) для сравнения со вторым циклом, чтобы найти одинаковые буквы в словах
            for (int x = 0; x < words.Count(); x++)
            {
                //Условие изменения операции
                bool condition = false;
                for (int y = 0; y < words[x].Length; y++)
                {
                    for (int z = y + 1; z < words[x].Length; z++)
                    {
                        //Сравнение буквы слова в строке второго цикла, с буквой слова в строке третьего цикла
                        if (words[x][y] == words[x][z])
                        {
                            condition = true;
                        }
                    }
                }

                // Если в слове есть одинаковые буквы, слово инвертируется(пишутся в обратном порядке - наоборот)
                if (condition == true)
                {
                    //создаем массив = делим массив слов на массив элементов(букв)
                    char[] arr = words[x].ToCharArray();
                    //Переворачиваем слово
                    Array.Reverse(arr);
                    //Добавлем в лист перевернутые слова
                    words2.Add(new string(arr));
                }
                else
                {
                    //добавляем в конец листа не перевернутые слова
                    words3.Add(words[x]);
                }
            }
            //сортировка в алфавитном порядке
            words3.Sort();

            //Цикл сортируюющий слова из второго листа по длине слова
            for (int x = 0; x < words2.Count(); x++)
            {
                string max = words[x];
                //Count считает количество слов в листе
                for (int y = x + 1; y < words2.Count(); y++)
                {
                    //Сравниваем длинну слов
                    if (words2[x].Count() < words2[y].Count())
                    {
                        max = words2[y];
                        words2[y] = words2[x];
                        words2[x] = max;
                    }
                }
            }

            Console.WriteLine("Слова, в которых были одинаковые буквы:");
            for (int x = 0; x < words2.Count(); x++)
            {
                Console.WriteLine(words2[x]);
            }

            Console.WriteLine("Слова, в которых нет одинаковых букв:");
            for (int x = 0; x < words3.Count(); x++)
            {
                Console.WriteLine(words3[x]);
            }
            Console.ReadKey();
        }
    }
}