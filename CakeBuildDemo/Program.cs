using System;

namespace CakeBuildDemo
{
   public class Program
   {
      public static void Main(string[] args)
      {
         if (args == null || args.Length == 0)
         {
            throw new ArgumentNullException(nameof(args));
         }

         foreach (var arg in args)
         {
            int result;
            if (!int.TryParse(arg, out result))
            {
               throw new ArgumentException($"Argument '{arg}' is not a valid number.");
            }
         }

         Console.WriteLine("All numbers are valid!");
      }
   }
}
