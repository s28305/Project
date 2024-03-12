int num1 = 33;
int num2 = num1*5 - 30;
Console.WriteLine("Number: " + num2);

int[] numbers = {10, 15, 7, 2, 77};
double average = CalcAverage(numbers);
Console.WriteLine("Average: " + average);

static double CalcAverage(int[] numbers) {
   int sum2 = 0;
   
   foreach (int n in numbers) {
      sum2 += n;
   }

   return (double)sum2/numbers.Length;
}

int[] numbers2 = {100, 66, 13, 74, 111, 97};
int max = CalcMaximum(numbers2);
Console.WriteLine("Maximum: " + max);


 static int CalcMaximum(int[] numbers) {
   int max = numbers[0];

   
   foreach (int num in numbers) {
      if (num > max) {
         max = num;
      }
   }

   return max;
}
