using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aquarium_fish_selection_program
{
    internal class Program
    {

        //нижче 3 перевантажені методи розрахунку обєму акваріуму, залежно від форми.
        //прямокутний: за висотою, шириною, глибиною, переведено в літри
        static double VolumeCalc(double height, double width, double depth)
        {
            return height * width * depth / 1000;
        }

        //круглий: за діаметром
        static double VolumeCalc(double diameter)
        {
            return (Math.PI * Math.Pow(diameter, 3)) / 6 / 1000;
        }

        // за діаметром і висотою циліндра
        static double VolumeCalc(double diameter, double height)
        {
            return Math.PI * Math.Pow(diameter, 2) * height / 4 / 1000;
        }

        //метод нижче застосовується для перетворення введеного тексту в індекси групи, назви і кількості   
        static void answerProcessing(string a, out int group, out int species, out int quantityR)
        {
            group = a[0] - '0' - 1;
            species = a[1] - '0' - 1;
            quantityR = Convert.ToInt32(a.Substring(2));
            //Console.WriteLine("Group: {0}, Specie: {1}, Quantity: {2}", group, species, quantityR);
        }

        /* завдання Програма підбору акваріумних рибок.
        У програмі творчо реалізуйте свої знання та вміння з використання умовних та циклічних конструкцій, методів та масивів мови програмування С#.
        
        В цій програмі застосовані наступні інструменти, які допоможуть підібрати рибок:
        1) розрахунок об'єму акваріуму
        2) підбір можливих варіантів виходячи з об'єму і суми
        3) підбір конкретних рибок так, щоб кількість не перевищувала рекомендовану, виходячи з обєму акваріуму і типу рибки. 
        В підсумку виводяться назва і кількість вибраних рибок, фактичне заповнення акваріуму, вартість рибок.
        */
        static void Main(string[] args)
        {
            Console.OutputEncoding= Encoding.Unicode;
            Console.WriteLine("Ця програма допоможе вам підібрати склад акваріумних рибок");
            Console.WriteLine("Перше, що повинні зробити, визначитися з розміром акваріуму");

            int volLarge = 12;
            int volMiddle = 6;
            int volSmall = 3;

            int prcLarge = 150;
            int prcMiddle = 90;
            int prcSmall = 30;
                                  
            double volume = 0;
            string input;

            do
            {
                Console.Write("Чи вам відомий об'єм акваріуму в літрах? (y/n) ");
                input = Console.ReadLine();
            }              
            while (((input == "y") || (input == "n")) != true);

            switch (input)
            {
                case "y":
                    
                    do
                    {
                        Console.Write("Введіть об'єм в літрах: ");
                        input = Console.ReadLine();
                        if (int.TryParse(input, out int num) && num > 1 && num <= 10000)
                            volume = Convert.ToDouble(num);
                    }
                    while (volume < 1 || volume > 10000);
                    break;

                case "n":
                    Console.WriteLine("Якої форми ваш акваріум? Оберіть номер відповіді(1-3): ");
                    Console.WriteLine("1: прямокутник");
                    Console.WriteLine("2: куля");
                    Console.WriteLine("3: циліндр/велика банка");
                    int choice;
                    double height;
                    double width;
                    double depth;
                    double diameter;
                    
                    do
                    {
                        Console.WriteLine("Зробіть свій вибір, введіть цифру відповіді від 1 до 3: ");
                    } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3);

                    switch (choice)
                    {
                        case 1:
                            do
                            {
                                Console.Write("Введіть висоту акваріуму в см: ");
                            } while (!double.TryParse(Console.ReadLine(), out height) || height < 1 || height > 200);
                            do
                            {
                                Console.Write("Введіть ширину акваріуму в см: ");
                            } while (!double.TryParse(Console.ReadLine(), out width) || width < 1 || width > 200);
                            do
                            {
                                Console.Write("Введіть глибину акваріуму в см: ");
                            } while (!double.TryParse(Console.ReadLine(), out depth) || depth < 1 || depth > 200);

                            volume = VolumeCalc(height, width, depth);
                            Console.WriteLine("Розрахунковий об'єм акваріуму - {0:0.0} л", volume);
                            break;
                        
                        case 2:
                            do
                            {
                                Console.Write("Введіть діаметр акваріуму в см: ");
                            } while (!double.TryParse(Console.ReadLine(), out diameter) || diameter < 10 || diameter > 100);
                           
                            volume = VolumeCalc(diameter);
                            Console.WriteLine("Розрахунковий об'єм акваріуму - {0:0.0} л", volume);
                            break;
                        
                        case 3:
                            Console.WriteLine("Ми порахуємо об'єм банки без врахування звуження зверху, не лийте воду до верху, рибки вистрибнуть!");
                            do
                            {
                                Console.Write("Введіть діаметр банки в см: ");
                            } while (!double.TryParse(Console.ReadLine(), out diameter) || diameter < 10 || diameter > 50);

                            do
                            {
                                Console.Write("Введіть висоту банки, її циліндричної частини в см: ");
                            } while (!double.TryParse(Console.ReadLine(), out height) || height < 10 || height > 50);

                            volume = VolumeCalc(diameter, height);
                            Console.WriteLine("Розрахунковий об'єм акваріуму - {0:0.0} л", volume);
                            break;
                    }
                    break;
            }
            //2 частина завдання
            do
            {
                Console.Write("Вас цікавить розрахунок оптимального заповнення акваріуму в поєднанні з сумою грошей, які ви готові витратити на рибок?(y/n) ");
                input = Console.ReadLine();
            }
            while (((input == "y") || (input == "n")) != true);

            if (input == "y") 
            {
                Console.WriteLine("Виходячи з об'єму акваріуму і суми грошей, які покупець готовий витратити на рибок, розрахуємо комбінацію рибок, яка дозволить максимально заповнити акваріум, виходячи з потреб води на одну рибку ");
                Console.Write("Введіть суму грошей, які покупець готовий витратити на купівлю рибок: ");
                int amount = Convert.ToInt32(Console.ReadLine());

                
                int maxVol = 0;
                int[] arrfish = new int[5];
                int currentCost;
                int currentVolume;


                for (int quantityLarge = 0; quantityLarge <= volume / volLarge; quantityLarge++)
                {
                    for (int quantityMiddle = 0; quantityMiddle <= (volume - quantityLarge * volLarge) / (volMiddle); quantityMiddle++)
                    {
                        for (int quantitySmall = 0; quantitySmall < (volume - quantityLarge * volLarge - quantityMiddle * volMiddle) / volSmall; quantitySmall++)
                        {
                            currentCost = quantityLarge * prcLarge + quantityMiddle * prcMiddle + quantitySmall * prcSmall;
                            currentVolume = quantityLarge * volLarge + quantityMiddle * volMiddle + quantitySmall * volSmall;

                            if (currentCost <= amount)
                            {
                                if (currentVolume > maxVol)
                                {
                                    arrfish[0] = quantityLarge;
                                    arrfish[1] = quantityMiddle;
                                    arrfish[2] = quantitySmall;
                                    arrfish[3] = currentCost;
                                    arrfish[4] = currentVolume;
                                    maxVol = currentVolume;
                                }
                            }
                        }

                    }
                }
                maxVol = arrfish[4];
                for (int quantityLarge = 0; quantityLarge <= volume / volLarge; quantityLarge++)
                {
                    for (int quantityMiddle = 0; quantityMiddle <= (volume - quantityLarge * volLarge) / (volMiddle); quantityMiddle++)
                    {
                        for (int quantitySmall = 0; quantitySmall < (volume - quantityLarge * volLarge - quantityMiddle * volMiddle) / volSmall; quantitySmall++)
                        {
                            currentCost = quantityLarge * prcLarge + quantityMiddle * prcMiddle + quantitySmall * prcSmall;
                            currentVolume = quantityLarge * volLarge + quantityMiddle * volMiddle + quantitySmall * volSmall;

                            if (currentCost <= amount)
                            {
                                if (currentVolume == maxVol)
                                {
                                    Console.WriteLine($"Великих рибок: {quantityLarge}, середніх: {quantityMiddle}, малих: {quantitySmall}, вартість: {currentCost}, літраж: {currentVolume}");
                                }
                            }
                        }

                    }
                }
            }
            //3 частина завдання
            string[][] fishSizeArray = new string[3][];
            fishSizeArray[0] = new string[] { "золота рибка", "скалярія", "цихлазома" };
            fishSizeArray[1] = new string[] { "півник", "гурамі", "ляліус" };
            fishSizeArray[2] = new string[] { "гуппі", "даніо", "меченосець", "кардинал" };

            int[][] fishNumberArray = new int[3][];
            fishNumberArray[0] = new int[] { 0, 0, 0 };
            fishNumberArray[1] = new int[] { 0, 0, 0 };
            fishNumberArray[2] = new int[] { 0, 0, 0, 0 };

            Console.WriteLine("\nПерейдемо до вибору рибок для вашого вашого акваріуму");
            Console.WriteLine("Рибки в магазині складають 3 групи залежно від розміру.\n");
            Console.WriteLine("Група 1 - великі риби:");
            for (int i = 0; i < fishSizeArray[0].Length; i++)
            {
                Console.Write(fishSizeArray[0][i] + " - " + (i + 1) + " ");
                if (i == fishSizeArray[0].Length - 1)
                    Console.WriteLine("\n");
            }

            Console.WriteLine("Група 2 - середні риби:");
            for (int i = 0; i < fishSizeArray[1].Length; i++)
            {
                Console.Write(fishSizeArray[1][i] + " - " + (i + 1) + " ");
                if (i == fishSizeArray[1].Length - 1)
                    Console.WriteLine("\n");
            }

            Console.WriteLine("Група 3 - малі риби:");
            for (int i = 0; i < fishSizeArray[2].Length; i++)
            {
                Console.Write(fishSizeArray[2][i] + " - " + (i + 1) + " ");
                if (i == fishSizeArray[2].Length - 1)
                    Console.WriteLine("\n");
            }
            
            Console.WriteLine($"Обсяг вашого акваріуму становить {volume:0.00} літрів");
            Console.WriteLine("Перш ніж обрати рибку, нагадаємо, що максимальна кількість рибок обмежується обсягом акваріуму і нормою води на одну рибку залежно від її розміру:");
            Console.WriteLine($"для великих - {volLarge}л, для середніх - {volMiddle}л, для малих - {volSmall}л.\n");
            Console.WriteLine("Щоб обрати рибку, необхідно ввести код, що складається з послідовноcті чисел:\nномер групи (1-3), номер рибки (1-9), кількість рибок (1-99)");
            
            int volumeFilled = 0;
            int castValue = 0;
            double volumeFillPercentage = 0;
            do
            {
                Console.Write("Введіть код рибки або exit для виходу: ");
                input = Console.ReadLine();

                if (int.TryParse(input, out int num) && num >= 111 && num <= 3499)
                {
                    answerProcessing(input, out int row, out int column, out int pieces);
                    if (fishNumberArray.Length > row && fishNumberArray[row].Length > column)
                    {
                        int sizePerOne = 0, pricePerOne = 0;
                        switch (row)
                        {
                            case 0: sizePerOne = volLarge; pricePerOne = prcLarge; break;
                            case 1: sizePerOne = volMiddle; pricePerOne = prcMiddle; break;
                            case 2: sizePerOne = volSmall; pricePerOne = prcSmall; break;
                        }
                        if (volumeFilled + pieces * sizePerOne <= volume)
                        {
                            volumeFilled += pieces * sizePerOne;
                            castValue += pieces * pricePerOne;
                            fishNumberArray[row][column] += pieces;
                            volumeFillPercentage = ((double)volumeFilled / volume) * 100;
                            Console.WriteLine($"Обрана рибка {fishSizeArray[row][column]} в кількості {pieces} одиниць, ціною {pricePerOne} грн за одну.");
                            Console.WriteLine($"Вартість всіх обраних рибок: {castValue} грн.");
                            Console.WriteLine($"Заповнення акваріуму: {volumeFilled:0.00} літрів, що складає {volumeFillPercentage:0.00}%\n");
                        }
                        else Console.WriteLine($"Кількість рибок перевищує обсяг акваріуму.\nПоточний доступний залишок {volume - volumeFilled:0.00} літрів." +
                            $"\nЗробіть інший вибір, враховуючи що великій рибці треба {volLarge}л, середній - {volMiddle}л, малій - {volSmall}л\nабо введіть exit для завершення вибору");
                    }
                    else Console.WriteLine("Такої рибки немає в асортименті, зробіть правильний вибір!");
                }

            }
            while (input != "exit");
            Console.WriteLine("\nПідсумок того, що ви обрали:\n");
            for (int i = 0; i < fishSizeArray.Length; i++)
            {
                for (int j = 0; j < (fishNumberArray[i]).Length; j++)
                {
                    if (fishNumberArray[i][j] != 0)
                    {
                        Console.WriteLine(fishSizeArray[i][j] + ": " + fishNumberArray[i][j] + " од");
                    }
                }
            }
            Console.WriteLine($"\nЗаповнення акваріуму: {volumeFilled} літрів, що складає {volumeFillPercentage:0.00}%\nВартість рибок: {castValue} грн.");
            Console.ReadKey();
        }
    }
}
