using System;

namespace NextBiggerTask
{
    public static class NumberExtension
    {
        /// <summary>
        /// Finds the nearest largest integer consisting of the digits of the given positive integer number and null if no such number exists.
        /// </summary>
        /// <param name="number">Source number.</param>
        /// <returns>
        /// The nearest largest integer consisting of the digits  of the given positive integer and null if no such number exists.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when source number is less than 0.</exception>
        public static int? NextBiggerThan(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Number cannot be less than 0.", nameof(number));
            }

            int numberCopy = number;
            int newNumber = 0;
            int size = 0;
            if (numberCopy == 0 || number == int.MaxValue)
            {
                return null;
            }

            while (numberCopy != 0)
            {
                numberCopy /= 10;
                size++;
            }

            if (size == 1)
            {
                return null;
            }

            int[] numberDigits = new int[size];
            numberCopy = number;

            for (int i = 0; i < size; i++)
            {
                numberDigits[i] = numberCopy % 10;
                numberCopy /= 10;
            }

            int positionOfMainDigit = 0;
            bool mainWasChanged = false;
            for (int i = 0; i < size - 1; i++)
            {
                if (numberDigits[i] > numberDigits[i + 1])
                {
                    positionOfMainDigit = i;
                    mainWasChanged = true;
                    break;
                }
            }

            if (!mainWasChanged)
            {
                return null;
            }

            numberDigits[positionOfMainDigit] += numberDigits[positionOfMainDigit + 1];
            numberDigits[positionOfMainDigit + 1] =
                numberDigits[positionOfMainDigit] - numberDigits[positionOfMainDigit + 1];
            numberDigits[positionOfMainDigit] -= numberDigits[positionOfMainDigit + 1];

            MergeSort(numberDigits, 0, positionOfMainDigit + 1);

            for (int i = 0; i < positionOfMainDigit + 1; i++)
            {
                newNumber += (int)(numberDigits[i] * Math.Pow(10, positionOfMainDigit - i));
            }

            for (int i = positionOfMainDigit + 1; i < size; i++)
            {
                newNumber += (int)(numberDigits[i] * Math.Pow(10, i));
            }

            if (newNumber == number)
            {
                return null;
            }

            return newNumber;
        }

        private static void Merge(int[] array, int lowIndex, int middleIndex, int highIndex)
        {
            var left = lowIndex;
            var right = middleIndex + 1;
            var tempArray = new int[highIndex - lowIndex + 1];
            var index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }

        private static void MergeSort(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                var middleIndex = (lowIndex + highIndex) / 2;
                MergeSort(array, lowIndex, middleIndex);
                MergeSort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }
        }
    }
}
