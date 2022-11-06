using ForDirectumConsole.Data;

namespace ForDirectumConsole
{
    public class Print
    {
        public static void Display(List<Meets> list)
        {
            Console.WriteLine("{0, -8}{1, -16}{2, -24}{3, -20}{4, -15}", "#", "Название", "Старт", "Конец", "Предупредить");
            Console.WriteLine("--------------------------------------------------------------------------------------");
            foreach (Meets meet in list)
            {
                Console.WriteLine($"{meet.Id}\t{meet.Title}\t{meet.StartMeet}\t{meet.EndMeet}\t{meet.Alert}");
            }
        }
    }
}
