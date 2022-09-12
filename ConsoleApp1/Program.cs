using ConsoleApp2;

namespace ConsoleApp1
{
    class write
    {
             public static int[] numbers = {1,2, 3, 4, 5, 6, 7, 8, 9, 10};
       public static void Main(String[] args) {
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write( $"\n{numbers[i]}");
            }
        }
    }
}