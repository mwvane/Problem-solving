using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
           
        }

        // გვაქვს მოცემული გრაფი 
        // ჩვენი მიზანია გრაფის შემოვლა DFS - ის საშუალებით 
        public static void Problem1()
		{
            List<List<int>> list = new List<List<int>>();
            Dictionary<int, bool> myDictioanry = new Dictionary<int, bool>();
            int counter = 0;

            for (int i = 0; i < 8; i++)
            {
                list.Add(new List<int>());
            }
            list[1] = new List<int>() { 2, 4, 5 };
            list[2] = new List<int>() { 3, 5 };
            list[5] = new List<int>() { 6, 7 };

            void DFS(int node)
            {
                if (myDictioanry.ContainsKey(node))
                {
                    return;
                }
                myDictioanry.Add(node, true);
                counter++;
                for (int i = 0; i < list[node].Count; i++)
                {
                    DFS(list[node][i]);
                }
            }

            DFS(1);

            Console.WriteLine(counter);
            Console.ReadKey();
        }


        // გვყავს რამოდენიმე ადამიანი 
        // ამოცანა შედგება ორი ეტაპისაგან 
        // ყოვეჯერზე ჩვენ შეგვიძლია ნებისმიერი ორი ადამიანი დავამეგობროთ 
        // ამ შემთხვევაში პირველი და მეორე ადამიანის მეგორები, ერთმანეთის მეგობრებიც ხდებიან
        // და მეორე ეტაპი: ვიკითხოთ არიან თუ არა რომელიმე ორი ადამიანი ერთმანეთის მეგობრები 
        static void Problem2()
		{
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            Dictionary<int, bool> visited = new Dictionary<int, bool>();

            for (int i = 1; i <= 8; i++)
            {
                graph.Add(i, new List<int> { });
            }

            for (int i = 0; i < 5; i++)
            {
                int operationType = Convert.ToInt32(Console.ReadLine());
                int firstPerson = Convert.ToInt32(Console.ReadLine());
                int secondPerson = Convert.ToInt32(Console.ReadLine());
                if (operationType == 1)
                {
                    MakeFriends(secondPerson, firstPerson);
                }
                else
                {
                    if (graph.ContainsKey(firstPerson) && graph[firstPerson].Contains(secondPerson))
                    {
                        Console.WriteLine(true);
                    }
                    else
                    {
                        Console.WriteLine(false);
                    }
                }
                Console.WriteLine("-------------------------------");
            }

            void MakeFriends(int personOne, int personTwo, bool IsFirstCall = true)
            {
                List<int> personTwoChildren;
                graph.TryGetValue(personTwo, out personTwoChildren);
                foreach (var personTwoChild in personTwoChildren)
                {
                    if (!graph[personTwoChild].Contains(personOne) && personOne != personTwoChild)
                        graph[personTwoChild].Add(personOne);

                    foreach (var personOneChild in graph[personOne])
                    {
                        if (!graph[personTwoChild].Contains(personOneChild) && personTwoChild != personOneChild)
                            graph[personTwoChild].Add(personOneChild);
                    }
                }
                if (graph[personTwo].Contains(personOne))
                {
                    return;
                }
                graph[personTwo].Add(personOne);
                foreach (var personOneChild in graph[personOne])
                {
                    if (!graph[personTwo].Contains(personOneChild) && personOneChild != personTwo)
                        graph[personTwo].Add(personOneChild);
                }
                if (IsFirstCall)
                {
                    MakeFriends(personTwo, personOne, false);
                }
            }

            Console.ReadKey();
        }

        /* 2. გვაქვს 1,5,10,20 და 50 თეთრიანი მონეტები. დაწერეთ ფუნქცია, რომელსაც გადაეცემა თანხა 
         * (თეთრებში) და აბრუნებს მონეტების მინიმალურ რაოდენობას, რომლითაც შეგვიძლია ეს თანხა დავახურდაოთ.*/
        public static int Problem3(int amount)
        {
            int[] monets = new int[] { 50, 20, 10, 5, 1 };
            int counter = 0;
            for (int i = 0; i < monets.Length; i++)
            {
                if (monets[i] <= amount)
                {
                    counter += amount / monets[i];
                    amount = amount % monets[i];
                }
            }
            return counter;
        }

        /* 4.     მოცემულია String რომელიც შედგება „(„ და „)“ ელემენტებისგან. დაწერეთ ფუნქცია რომელიც აბრუნებს 
        * ფრჩხილები არის თუ არა მათემატიკურად სწორად დასმული.*/

        public static bool Problem4(String sequence)
        {
            int counter = 0;
            for (int i = 0; i < sequence.Length; i++)
            {
                if (counter < 0)
                {
                    return false;
                }
                if (sequence[i] == '(')
                {
                    counter++;
                }
                if (sequence[i] == ')')
                {
                    counter--;
                }
            }
            if (counter != 0)
            {
                return false;
            }
            return true;
        }

        /* 5.     გვაქვს n სართულიანი კიბე, ერთ მოქმედებაში შეგვიძლია ავიდეთ 1 ან 2 საფეხურით. დაწერეთ ფუნქცია რომელიც
        * დაითვლის n სართულზე ასვლის ვარიანტების რაოდენობას.*/

        public static int Problem5(int stearsCount)
        {
            List<int> list = new List<int>();

            // თუ თვლას ვიწყებთ 0 საფეხურიდან, ანუ შეგვიძლია გადახტომა პირველ ან მეორე საფეხურზე
            list.Add(1); // list[0] = 1
            // პირველ საფეხურზე მხოლოდ ერთი გზა არსებობს 0-დან ერთამდე
            list.Add(1);  // list[1] = 1  
            for (int i = 2; i < stearsCount; i++)
            {
                //  list[i] =  წინა 2 რიცხვის ჯამის
                list.Add(list[i - 1] + list[i - 2]);

            }
            return list.Last();
        }

        /*
         *  მოცემულია 1-10^5 სიგრძის ორი სტრინგი და ჩვენ გვჭირდება მაგათი შეკრება 
         */
        static string Problem6(string a, string b)
        {
            string result = "";
            a = a.Reverse();
            b = b.Reverse();
            int reminder = 0;
            for (int i = 0; i < MyExtension.MaxLength(a.Length, b.Length); i++)
            {
                int letterA = 0;
                if (i < a.Length)
                {
                    letterA = (int)(a[i] - '0');
                }

                int letterB = 0;
                if (i < b.Length)
                {
                    letterB = (int)(b[i] - '0');
                }

                int tmp = letterA + letterB;
                tmp += reminder;
                reminder = 0;

                if (tmp >= 10)
                {
                    reminder = tmp / 10;
                    tmp = tmp % 10;

                }
                result += tmp.ToString();
            }
            if (reminder > 0)
            {
                result += reminder.ToString();
            }
            return result.Reverse();
        }

        /*
         * მოცემული გვაქვს მასივი 
         * ჩვენ გვაინტერესებს ამ მასივში მიყოლებით მდებარე 1 ან რამოდენიმე რიცხვის ჯამი 
         * არის თუ არა 0 - ის ტოლი 
         */
        public static bool Problem7(int[] array)
        {
            Dictionary<int, bool> sum = new Dictionary<int, bool>();
            sum.Add(0, true);
            int temp = 0;
            for (int i = 0; i < array.Length; i++)
            {
                temp += array[i];
                if (sum.ContainsKey(temp))
                {
                    return true;
                }
                sum.Add(temp, true);

            }
            return temp == 0;
        }

        /*
         *  ალისა და ბობი თამაშობენ შემდეგ თამაშს
         *  გვაქვს კიბე რომლიეც შედგება გარკვეული რაოდენობის საფეხურებისაგან
         *  პირველ საფეხურზე დგას ალისაც და ბობიც
         *  ყოველ ჯერზე თითოეულს შეუძლია 1 ან 2 საფეხურით მაღლა გადახტომა 
         *  სადაც ჩერდება ერთი, იქიდან აგრძელებს მეორე 
         *  პირველი ვინც ავა კიბის თავში ის იგებს
         *  თამაშს იწყებს ალისა
         *  გავიგოთ ვინ მოიგებს თამაშს
         */
        public static void Problem8()
		{
            int stearsCount = 12;
            int currentStear = 10;
            int[] array = new int[stearsCount];
            array[stearsCount - 1] = array[stearsCount - 2] = 1;
            currentStear = stearsCount - 3;
            while (currentStear >= 0)
            {
                int one = array[currentStear + 1];
                int two = array[currentStear + 2];
                int currentStearFlag;
                if (one == two && one == 1)
                {
                    currentStearFlag = 0;
                }
                else
                {
                    currentStearFlag = 1;
                }
                array[currentStear] = currentStearFlag;
                currentStear--;
            }
            if (array[0] == 1)
            {
                Console.WriteLine("vinc iwyebs is igebs");
            }
            else
            {
                Console.WriteLine("vinc iwyebs is agebs");
            }
        }

        /*
         *  მოცემული გვაქვს ორი მასივი 
         *  ჩვენ გვინდა გავიგოთ რა მინიმალური სხვაობის მიღება შეუძლია
         *  პირველი მასივიდან ამოღებულ და მეორე მასივიდან ამოღებულ ნებისმიერ რიცხვს
         */
        public void Problem9()
		{

            int[] arrayOne = { -3, 1, 2, 5, 7, 10 };
            int[] arrayTwo = { -5, -4, 5, 9, 11, 12 };

            //used arrySort
            Array.Sort(arrayTwo);
            int result = 151556166;
            int x = ClosestNumberToLeft(arrayTwo, 7);
            foreach (var item in arrayOne)
            {
                int leftMost = ClosestNumberToLeft(arrayTwo, item);
                int righMost = ClosestNumberToRight(arrayTwo, item);
                int tmp = Math.Abs(item - leftMost);
                result = Math.Min(result, tmp);
                tmp = Math.Abs(item - righMost);
                result = Math.Min(result, tmp);
            }
            Console.WriteLine(result);

            int ClosestNumberToRight(int[] array, int number)
            {
                int left = 0;
                int right = array.Length - 1;
                while (right - left > 1)
                {
                    int middle = (right + left) / 2;
                    if (array[middle] == number)
                    {
                        return array[middle];
                    }
                    else if (array[middle] < number)
                    {
                        left = middle;
                    }
                    else
                    {
                        right = middle;
                    }
                }
                if (array[left] >= number)
                {
                    return array[left];
                }
                return array[right];
            }

            int ClosestNumberToLeft(int[] array, int number)
            {
                int left = 0;
                int right = array.Length - 1;
                while (right - left > 1)
                {
                    int middle = (right + left) / 2;
                    if (array[middle] == number)
                    {
                        return array[middle];
                    }
                    else if (array[middle] < number)
                    {
                        left = middle;
                    }
                    else
                    {
                        right = middle;
                    }
                }

                return array[left];
            }
        }


    }
}
public static class MyExtension
{
    public static string Reverse(this string text)
    {
        string temp = "";
        for (int i = text.Length - 1; i >= 0; i--)
        {
            temp += text[i];
        }
        return temp;
    }
    public static int MaxLength(this int a, int b)
    {
        if (a > b)
        {
            return a;
        }
        return b;
    }
}