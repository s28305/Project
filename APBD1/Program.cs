int num1 = 33;
int num2 = num1*5 - 30;
Console.WriteLine("Number: " + num2);

int[] numbers = {10, 15, 7, 2, 77};
double average = CalcAverage(numbers);
Console.WriteLine("Average: " + average);

static double CalcAverage(int[] numbers) {
   int sum = 0;
   
   foreach (int n in numbers) {
      sum += n;
   }

   return (double)sum/numbers.Length;
}
